using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG8_ScrewDriver : MonoBehaviour
{
    public List<GameColor> sequence = new List<GameColor>();
    public List<Sprite> ScrewDriverSprite= new List<Sprite>();
    public List<Sprite> ScrewSprite = new List<Sprite>();
    public SpriteRenderer screw;
    public int index;
    public int buttonNumber, playerID;
    private bool canInput=true;
    public int numberOfturnNeeded;
    private int numberOfturn;
    private int numberOfScrew;
    // Start is called before the first frame update
    void Start()
    {
        
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
                if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.A))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.S))
                    checkColor(GameColor.Red);
                else if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.Q))
                    checkColor(GameColor.Yellow);
            }
            if (buttonNumber == 1)
            {
                //t=blue,f=vert,g=rouge
                if (Input.GetKeyDown(KeyCode.T))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.F))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(KeyCode.G))
                    checkColor(GameColor.Red);
            }
            if (buttonNumber == 2)
            {
                //up arros =blue,down arrow =vert,left arrow=jaune
                if (Input.GetKeyDown(KeyCode.UpArrow))
                    checkColor(GameColor.Blue);
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                    checkColor(GameColor.Green);
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                    checkColor(GameColor.Yellow);
            }
            if (buttonNumber == 3)
            {
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
        if (col == sequence[index])
        {
            Debug.Log("oui");
            index++;

            if (index == sequence.Count)
            {
                index = 0;
                numberOfturn++;
                if (numberOfturn >= numberOfturnNeeded)
                {
                    if(playerID == 1) 
                    {
                        MG8_Manager.instance.screwp1++;
                    }
                    else
                    {
                        MG8_Manager.instance.screwp2++;
                    }
                    canInput = false;
                }
                    
            }
            GetComponent<SpriteRenderer>().sprite = ScrewDriverSprite[index];
            screw.GetComponent<SpriteRenderer>().sprite = ScrewSprite[index];
            transform.position = new Vector2(transform.position.x, transform.position.y+0.4f);
        }
        else
            Debug.Log("non");
    }
}
