using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MG7_baloon : MonoBehaviour
{

    public int playerID;
    private bool canInput=true;
    private GameColor colorNeeded;
    public Sprite spriteblue, spritegreen, spritered,spriteyellow;
    
    public SpriteRenderer pompeSR;
    public Sprite pompeSprite1, pompeSprite2;
    
    public float scalepercentage=1.10f;
    public GameObject baloon;
    public AudioClip sound;
    private ColorKeys _colorKeys = new();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BaloonColorChange());
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            //z=blue,s=rouge,q=jaune
            if (playerID == 1)
            {
                if (Input.GetKeyDown(_colorKeys.button1Red.code) || Input.GetKeyDown(_colorKeys.button2Red.code))
                    Checkcolor(1, GameColor.Red);
                else if (Input.GetKeyDown(_colorKeys.button1Blue.code) || Input.GetKeyDown(_colorKeys.button2Blue.code))
                    Checkcolor(1, GameColor.Blue);
                else if (Input.GetKeyDown(_colorKeys.button1Yellow.code))
                    Checkcolor(1, GameColor.Yellow);
                else if (Input.GetKeyDown(_colorKeys.button2Green.code))
                    Checkcolor(1, GameColor.Green);
            }
            else if (playerID == 2)
            {
                //up arros =blue,down arrow =vert,left arrow=jaune
                if (Input.GetKeyDown(_colorKeys.button4Red.code))
                    Checkcolor(2, GameColor.Red);
                else if (Input.GetKeyDown(_colorKeys.button3Blue.code))
                    Checkcolor(2, GameColor.Blue);
                else if (Input.GetKeyDown(_colorKeys.button3Yellow.code) || Input.GetKeyDown(_colorKeys.button4Yellow.code))
                    Checkcolor(2, GameColor.Yellow);
                else if (Input.GetKeyDown(_colorKeys.button3Green.code) || Input.GetKeyDown(_colorKeys.button4Green.code))
                    Checkcolor(2, GameColor.Green);
            }
        }
    }
    public void Checkcolor(int p,GameColor col)
    {
        Debug.Log("check");
        if (pompeSR.sprite == pompeSprite1)
            pompeSR.sprite = pompeSprite2;
        else
            pompeSR.sprite = pompeSprite1;
        if (col == colorNeeded)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1f);

            GetComponent<AudioSource>().PlayOneShot(sound);
            transform.localScale = new Vector3(transform.localScale.x+scalepercentage, transform.localScale.y+scalepercentage,transform.localScale.z);
            if (playerID == 1)
                MG7_Manager.Instance.scorep1++;
            else
                MG7_Manager.Instance.scorep2++;
        }
        else
        {
            if (playerID == 1)
                MG7_Manager.Instance.scorep1--;
            else
                MG7_Manager.Instance.scorep2--;
            if(transform.localScale.x > 0.5f)
            transform.localScale = new Vector3(transform.localScale.x - scalepercentage, transform.localScale.y - scalepercentage, transform.localScale.z);
        }
        
        
    }
    private IEnumerator BaloonColorChange()
    {
        yield return new WaitForSeconds(Random.Range(MG7_Manager.Instance.RandomTimeMin, MG7_Manager.Instance.RandomTimeMax));
        colorNeeded = (GameColor)Random.Range(0, 4);
        switch (colorNeeded)
        {
            case GameColor.Red:
                baloon.GetComponent<SpriteRenderer>().sprite = spritered; break;
            case GameColor.Green:
                baloon.GetComponent<SpriteRenderer>().sprite = spritegreen; break;
            case GameColor.Blue:
                baloon.GetComponent<SpriteRenderer>().sprite = spriteblue; break;
            case GameColor.Yellow:
                baloon.GetComponent<SpriteRenderer>().sprite = spriteyellow;
                break;
        }
        StartCoroutine(BaloonColorChange());
    }
    private IEnumerator delay()
    {
        canInput = false;
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        canInput = true;
    }
}
