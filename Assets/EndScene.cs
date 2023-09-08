using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI WinText;
    void Start()
    {
        WinText.text = GameManager.instance.winName;
        //GameManager.instance.LoadendScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
