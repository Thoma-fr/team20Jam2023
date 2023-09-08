using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MG8_ScrewDriver : MonoBehaviour
{
    public List<GameColor> sequence = new List<GameColor>();
    public List<Sprite> ScrewDriverSprite = new List<Sprite>();
    public List<Sprite> ScrewSprite = new List<Sprite>();
    public SpriteRenderer screw;
    public int index;
    public int buttonNumber, playerID;
    private bool canInput = true;
    public int numberOfturnNeeded;
    private int numberOfturn;
    private int numberOfScrew;

    public AudioClip unscrew;
    private ColorKeys _colorKeys = new();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(delay());
        numberOfturnNeeded = MG8_Manager.instance.NumberpfTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInput)
        {
            if (buttonNumber == 0)
            {
                Debug.Log(index);
                Debug.Log(sequence[index]);

                //z=blue,s=rouge,q=jaune
                if (Input.GetKeyDown(_colorKeys.button1Blue.code))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(_colorKeys.button1Red.code))
                    checkColor(GameColor.Red);
                else if (Input.GetKeyDown(_colorKeys.button1Yellow.code))
                    checkColor(GameColor.Yellow);
            }
            if (buttonNumber == 1)
            {
                //t=blue,f=vert,g=rouge
                if (Input.GetKeyDown(_colorKeys.button2Blue.code))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(_colorKeys.button2Green.code))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(_colorKeys.button2Red.code))
                    checkColor(GameColor.Red);
            }
            if (buttonNumber == 2)
            {
                //up arros =blue,down arrow =vert,left arrow=jaune
                if (Input.GetKeyDown(_colorKeys.button3Blue.code))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(_colorKeys.button3Green.code))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(_colorKeys.button3Yellow.code))
                    checkColor(GameColor.Yellow);
            }
            if (buttonNumber == 3)
            {
                //pav� num 5 =vert,1=rouge,2=jaune
                if (Input.GetKeyDown(_colorKeys.button4Green.code))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(_colorKeys.button4Yellow.code))
                    checkColor(GameColor.Yellow);
                else if (Input.GetKeyDown(_colorKeys.button4Red.code))
                    checkColor(GameColor.Red);
            }
        }
    }

    public void checkColor(GameColor col)
    {
        if (col == sequence[index])
        {
            Debug.Log("oui");
            index++;
            GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1f);
            GetComponent<AudioSource>().PlayOneShot(unscrew);
            if (index == sequence.Count)
            {
                index = 0;
                numberOfturn++;
                if (numberOfturn >= numberOfturnNeeded)
                {
                    if (playerID == 1)
                    {
                        MG8_Manager.instance.screwp1++;
                        screw.GetComponent<Rigidbody2D>().isKinematic=false;
                    }
                    else
                    {
                        MG8_Manager.instance.screwp2++;
                        screw.GetComponent<Rigidbody2D>().isKinematic = false;
                    }
                    canInput = false;
                }

            }
            GetComponent<SpriteRenderer>().sprite = ScrewDriverSprite[index];
            screw.GetComponent<SpriteRenderer>().sprite = ScrewSprite[index];
            transform.position = new Vector2(transform.position.x, transform.position.y + MG8_Manager.instance.screUpMove / numberOfturnNeeded);
        }
        else
            Debug.Log("non");
    }
    private IEnumerator delay()
    {
        canInput = false;
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        canInput= true;
    }
}
