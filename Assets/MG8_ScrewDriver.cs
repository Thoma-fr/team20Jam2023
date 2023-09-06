using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG8_ScrewDriver : MonoBehaviour
{
    public List<GameColor> sequence = new List<GameColor>();
    public List<Sprite> ScrewDriverSprite= new List<Sprite>();
    public int index;
    public int buttonNumber, playerID;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(buttonNumber == 0)
        {
            //z=blue,s=rouge,q=jaune
        }
        if (buttonNumber == 1)
        {
            //z=blue,s=rouge,q=jaune
        }
    }

    public void checkColor(GameColor col)
    {
        if(col == sequence[index])
        {
            index++;
            GetComponent<SpriteRenderer>().sprite = ScrewDriverSprite[index];
            if(index == sequence.Count)
                index = 0;
        }
    }
}
