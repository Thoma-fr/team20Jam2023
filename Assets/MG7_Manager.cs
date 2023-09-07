using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class MG7_Manager : MonoBehaviour
{
    public float RandomTimeMin, RandomTimeMax;
    public static MG7_Manager Instance;
    public int ScoreNeeded;
    public int scorep1,scorep2;
    public string message;
    public TextMeshProUGUI timerText;
    [SerializeField] private int startTime;
    bool isfinished;
    void Awake()
    {
        timerText.text = startTime.ToString();
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        if (GameManager.instance.MinigamesDones < 8)
            startTime -= GameManager.instance.MinigamesDones / 2;
        GameManager.instance.DisplayMessage(message);
        StartCoroutine(delay());
    }
    private IEnumerator delay()
    {
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        StartCoroutine(countdown());
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
    public void Update()
    {
        if (!isfinished)
        {
            if (scorep1 >= ScoreNeeded)
            {
                GameManager.instance.endMinigame(1, "p1");
                isfinished = true;
            }
            else if (scorep2 >= ScoreNeeded)
            {
                isfinished = true;
                GameManager.instance.endMinigame(1, "p2");
            }
        }
    }
    public void EndMiniGame()
    {
        GameManager.instance.endMinigame(0, "null");
    }
}
