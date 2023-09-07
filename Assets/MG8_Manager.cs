using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;

public class MG8_Manager : MonoBehaviour
{
    public int screwp1, screwp2;
    public static MG8_Manager instance;

    public string message;

    public TextMeshProUGUI timerText;
    [SerializeField] private int startTime;
    public int NumberpfTurn;

    public float screUpMove = 0.8f;
    private bool isfinished;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        timerText.text = startTime.ToString();

        GameManager.instance.DisplayMessage(message);
        StartCoroutine(delay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!isfinished)
        {
            if (screwp1 >= 2)
            {
                GameManager.instance.endMinigame(1, "p1");
                Debug.Log("p1 gagne");
                isfinished=true;
            }
            else if (screwp2 >= 2)
            {
                Debug.Log("p2 gagne");
                GameManager.instance.endMinigame(1, "p2");
                isfinished = true;
            }
        }

    }
    private IEnumerator countdown()
    {
        yield return new WaitForSeconds(1);
        startTime--;
        timerText.text = startTime.ToString();
        if (startTime <= 0)
        {
            GameManager.instance.endMinigame(0, "null");
        }
        else
            StartCoroutine(countdown());
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        StartCoroutine(countdown());
        if (GameManager.instance.MinigamesDones < 8)
            startTime -= GameManager.instance.MinigamesDones / 2;
        else
            startTime -= 5;

        if (startTime <= 5)
        {
            NumberpfTurn--;
        }
        else if (startTime <= 3)
        {
            NumberpfTurn--;
        }
    }
}
