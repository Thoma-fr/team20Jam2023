using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public enum GameColor
{
    Red,
    Green,
    Blue,
    Yellow
};
public class GameManager : MonoBehaviour
{
    public int p1Score {  get; set; }
    public int p2Score { get; set; }
    public TextMeshProUGUI scoretext1, scoretext2;
    public static GameManager instance;
    public TextMeshProUGUI timerText;
    public GameObject messageObject;
    public Transform messagepos1, messagepos2;
    public float currentTime;

    public int minigamePlayed;

    public List<string> scenesNames = new List<string>();
    public int curentIndex;
    public int MinigamesDones;

    public float timeBeforestart=3;

    public GameObject rideauGauche, rideauDroit;
    public string winName;
    public int numberToWin = 8;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        //DisplayMessage("dfdvrqjyrhstegqrfe");
        
    }
    private void Update()
    {
        timerText.text = currentTime.ToString("0:00");
        currentTime = currentTime += Time.deltaTime;
    }
    public void endMinigame(int score, string name)
    {
        switch(name)
        {
            case "null":
                break;
            case "p1":
                p1Score += score;
                scoretext1.text=p1Score.ToString();
                Debug.Log(p1Score);
                break;
            case "p2":
                p2Score += score;
                scoretext2.text = p2Score.ToString();
                Debug.Log(p2Score);
                break;
        }
        MinigamesDones++;
        if (p1Score >= numberToWin)
        {
            endScene("Player 1 WIN");
        }
        else if(p2Score >= numberToWin)
        {
            endScene("Player 2 WIN");
        }
        else
            transiAnim();

    }
    public void endScene(string  name) 
    {
        winName=name;
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rideauGauche.transform.DOLocalMove(new Vector3(-436, -102, 0), 1));
        sequence.Insert(0.2f, rideauDroit.transform.DOLocalMove(new Vector3(456, -102, 0), 1).OnComplete(() => LoadendScene()));


        sequence.Append(rideauGauche.transform.DOLocalMove(new Vector3(-921, -102, 0), 1));
        sequence.Insert(1.2f, rideauDroit.transform.DOLocalMove(new Vector3(924, -102, 0), 1));
    }
    public void LoadendScene()
    {
        SceneManager.LoadScene("EndScene");
        StartCoroutine(LoadFirstScene());
    }
    private IEnumerator LoadFirstScene() 
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("StartScene");
        LoadendScene();
    }
    public void transiAnim()
    { 
        Sequence sequence = DOTween.Sequence();
        sequence.Append(rideauGauche.transform.DOLocalMove(new Vector3(-436, -102, 0), 1));
        sequence.Insert(0.2f,rideauDroit.transform.DOLocalMove(new Vector3(456, -102, 0), 1).OnComplete(()=> ChangeScene()));
        

        sequence.Append(rideauGauche.transform.DOLocalMove(new Vector3(-921, -102, 0), 1));
        sequence.Insert(1.2f, rideauDroit.transform.DOLocalMove(new Vector3(924, -102, 0), 1));
    }

    public void ChangeScene()
    {
        //string nextScene = scenesNames[Random.Range(0, scenesNames.Count)];
        //while (curentScene == nextScene)
            //curentScene = scenesNames[Random.Range(0, scenesNames.Count)];
        //SceneManager.LoadScene(nextScene);

        int nextIndex = Random.Range(0, scenesNames.Count);
        while (curentIndex == nextIndex)
            nextIndex = Random.Range(0, scenesNames.Count);
        curentIndex = nextIndex;
        SceneManager.LoadScene(scenesNames[nextIndex]);
    }

    public void DisplayMessage(string message)
    {
        TextMeshProUGUI messageText= messageObject.GetComponent<TextMeshProUGUI>();
        Sequence mySequence = DOTween.Sequence();
        messageText.text = message;
        mySequence.Append(messageObject.transform.parent.DOMove(messagepos2.transform.position,0.5f).SetEase(Ease.OutBounce));
        mySequence.AppendInterval(2f);
        mySequence.Append(messageObject.transform.parent.DOMove(messagepos1.transform.position, 0.5f));
    }

}
