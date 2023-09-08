using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

            if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.T))
                Checkcolor(1, GameColor.Red);
            else if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.R))
                Checkcolor(1, GameColor.Blue);
            else if (Input.GetKeyUp(KeyCode.A))
                Checkcolor(1, GameColor.Yellow);
            else if (Input.GetKeyUp(KeyCode.Y))
                Checkcolor(1, GameColor.Green);

            //up arros =blue,down arrow =vert,left arrow=jaune
            if (Input.GetKeyUp(KeyCode.Q))
                Checkcolor(2, GameColor.Red);
            else if (Input.GetKeyUp(KeyCode.U))
                Checkcolor(2, GameColor.Blue);
            else if (Input.GetKeyUp(KeyCode.S) && Input.GetKeyUp(KeyCode.O))
                Checkcolor(2, GameColor.Yellow);
            else if (Input.GetKeyUp(KeyCode.I) && Input.GetKeyUp(KeyCode.P))
                Checkcolor(2, GameColor.Green);
    }
    public void Checkcolor(int playerID,GameColor col)
    {
        GameManager.instance.endMinigame(0,"null");
    }
}
