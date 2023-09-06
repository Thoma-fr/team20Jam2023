using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG8_Manager : MonoBehaviour
{
    public int screwp1, screwp2;
    public static MG8_Manager instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (screwp1 <= 2)
        {
            GameManager.instance.endMinigame(1, "p1");
            Debug.Log("p1 gagne");
        }
        else if (screwp2 <= 2)
        {
            Debug.Log("p2 gagne");
            GameManager.instance.endMinigame(1, "p2");
        }
    }

}
