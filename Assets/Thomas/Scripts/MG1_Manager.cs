using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MG1_Manager : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    [SerializeField]private int startTime;

    private AudioSource audioSource;
    public string message;
    public float timeBeforeStart; 
    [SerializeField] private GameObject spawner1,spawner2;
    public static MG1_Manager instance;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        GameManager.instance.DisplayMessage(message);
        StartCoroutine(delay());
        startTime -= GameManager.instance.MinigamesDones;
        audioSource = GameManager.instance.GetComponent<AudioSource>();
    }

    private void Update()
    {
        timerText.text = startTime.ToString();
        //startTime = startTime += Time.deltaTime;
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        spawner1.SetActive(true);
        spawner2.SetActive(true);
        StartCoroutine(countdown());
    }
    private IEnumerator countdown()
    {
        yield return new WaitForSeconds(1);
        startTime--;
        if (startTime <= 0)
        {
            EndMiniGame();
        }
        else
            StartCoroutine(countdown());
    }
    public void EndMiniGame()
    {
        spawner1.SetActive(false);
        spawner2.SetActive(false);
        if(spawner1.GetComponent<MG1_Sequence>().numberPoped> spawner2.GetComponent<MG1_Sequence>().numberPoped)
            GameManager.instance.endMinigame(1, "p1");
        else if(spawner1.GetComponent<MG1_Sequence>().numberPoped < spawner2.GetComponent<MG1_Sequence>().numberPoped)
            GameManager.instance.endMinigame(1, "p2");
        else
            GameManager.instance.endMinigame(0,"null");

    }
}
