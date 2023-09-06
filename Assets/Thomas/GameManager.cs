using UnityEngine;
using TMPro;
using DG.Tweening;

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
    public static GameManager instance;
    public TextMeshProUGUI timerText;
    public GameObject messageObject;
    public Transform messagepos1, messagepos2;
    public float currentTime;

    public int minigamePlayed;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        DisplayMessage("dfdvrqjyrhstegqrfe");
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
                break;
            case "p2":
                p2Score += score;
                break;
        }
    }
    public void DisplayMessage(string message)
    {
        TextMeshProUGUI messageText= messageObject.GetComponent<TextMeshProUGUI>();
        Sequence mySequence = DOTween.Sequence();
        messageText.text = message;
        mySequence.Append(messageObject.transform.DOMove(messagepos2.transform.position,0.5f).SetEase(Ease.OutBounce));
        mySequence.AppendInterval(2f);
        mySequence.Append(messageObject.transform.DOMove(messagepos1.transform.position, 0.5f));
    }
}
