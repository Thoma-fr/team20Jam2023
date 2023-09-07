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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BaloonColorChange());
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            //z=blue,s=rouge,q=jaune
            if (playerID == 1)
            {
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.A))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.S))
                    checkColor(GameColor.Red);
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A))
                    checkColor(GameColor.Yellow);
                //t=blue,f=vert,g=rouge
                if (Input.GetKeyDown(KeyCode.T))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.F))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(KeyCode.G))
                    checkColor(GameColor.Red);
            }
            else if (playerID == 2)
            {
                //up arros =blue,down arrow =vert,left arrow=jaune
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    checkColor(GameColor.Yellow);
                //pavé num 5 =vert,1=rouge,2=jaune
                if (Input.GetKeyDown(KeyCode.O))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(KeyCode.K))
                    checkColor(GameColor.Yellow);
                else if (Input.GetKeyDown(KeyCode.L))
                    checkColor(GameColor.Red);
            }
        }
    }
    public void checkColor(GameColor col)
    {
        Debug.Log("check");
        if (pompeSR.sprite == pompeSprite1)
            pompeSR.sprite = pompeSprite2;
        else
            pompeSR.sprite = pompeSprite1;
        if (col == colorNeeded)
        {
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
        colorNeeded = (GameColor)Random.Range(0, 3);
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
}
