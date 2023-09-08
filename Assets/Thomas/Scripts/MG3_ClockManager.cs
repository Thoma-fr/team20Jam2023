using DG.Tweening;
using System.Collections;
using UnityEngine;

public class MG3_ClockManager : MonoBehaviour
{
    public GameColor colorAvailable;
    [SerializeField] private int DurationRandomMAx, DurationRandomMin;
    private int duration;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Camera cam;
    [SerializeField] private Vector3 startCamPos;
    [SerializeField] private float camspeed;
    private bool canInput;
    public string message;
    public AudioClip ring;
    private ColorKeys _colorKeys = new();
    private bool canMove;
    void Start()
    {
        startCamPos = cam.transform.position;
        GameManager.instance.DisplayMessage(message);
        StartCoroutine(delay());
        GameManager.instance.GetComponent<AudioLowPassFilter>().enabled = true;
        DOTween.To(() => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency = x, 1000, 1);

    }
    private IEnumerator delay()
    {
        DOTween.To(() => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency = x, 1000, 1);
        yield return new WaitForSeconds(GameManager.instance.timeBeforestart);
        duration = Random.Range(DurationRandomMin, DurationRandomMAx);
        canMove = true;
        DOTween.To(() => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency = x, 10, duration);
        StartCoroutine(ticktack());
    }
    void Update()
    {
        if (!canInput&& canMove)
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, cam.transform.position.z + camspeed * Time.deltaTime);
        }
        else
            cam.transform.position = startCamPos;

        if (Input.GetKeyDown(_colorKeys.button4Red.code))
            Checkcolor(2, GameColor.Red);
        else if (Input.GetKeyDown(_colorKeys.button3Blue.code))
            Checkcolor(2, GameColor.Blue);
        else if (Input.GetKeyDown(_colorKeys.button3Yellow.code) || Input.GetKeyDown(_colorKeys.button4Yellow.code))
            Checkcolor(2, GameColor.Yellow);
        else if (Input.GetKeyDown(_colorKeys.button3Green.code) || Input.GetKeyDown(_colorKeys.button4Green.code))
            Checkcolor(2, GameColor.Green);

        if (Input.GetKeyDown(_colorKeys.button1Red.code) || Input.GetKeyDown(_colorKeys.button2Red.code))
            Checkcolor(1, GameColor.Red);
        else if (Input.GetKeyDown(_colorKeys.button1Blue.code) || Input.GetKeyDown(_colorKeys.button2Blue.code))
            Checkcolor(1,GameColor.Blue);
        else if (Input.GetKeyDown(_colorKeys.button1Yellow.code))
            Checkcolor(1,GameColor.Yellow);
        else if (Input.GetKeyDown(_colorKeys.button2Green.code))
            Checkcolor(1, GameColor.Green);
    }
    public void animCadran()
    {
        

    }
    public void Checkcolor(int ID, GameColor col)
    {
        if (!canInput)
        {
            if (ID == 1)
                EndMinigame(2);
            else
                EndMinigame(1);
            StopAllCoroutines();
        }
        else
        {
            if(col== colorAvailable)
            {
                EndMinigame(ID);
            }
            else
            {
                if (ID == 1)
                    EndMinigame(2);
                else
                    EndMinigame(1);
            }
        }
    }
    public void EndMinigame(int id)
    {
        Debug.Log("gg a p" + id);
        GameManager.instance.endMinigame(1, "p" + id);
        DOTween.To(() => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency, x => GameManager.instance.GetComponent<AudioLowPassFilter>().cutoffFrequency = x, 5000, 1).OnComplete(()=>GameManager.instance.GetComponent<AudioLowPassFilter>().enabled=false);
    }
    private IEnumerator ticktack()
    {
        for (int i = 0; i < DurationRandomMin; i++)
        {
            yield return new WaitForSeconds(1);
            //cam.orthographicSize--;
            ticktack();
            background.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            //tick sound
        }
        //cam.orthographicSize=startCamSize;
        colorAvailable= (GameColor)Random.Range(0, 3);
        canInput = true;
        ClockAnime();
        switch (colorAvailable)
        {
            case GameColor.Red:
                background.color = Color.red; break;
                case GameColor.Green:
                background.color = Color.green; break;
                case GameColor.Blue:
                background.color = Color.blue; break;
                case GameColor.Yellow:
                background.color = Color.yellow;
                break;
        }

    }
    public void ClockAnime()
    {
        GetComponent<AudioSource>().Stop();
        GetComponent<AudioSource>().PlayOneShot(ring);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, 5),0.1f,RotateMode.Fast));
        sequence.Append(transform.DOLocalRotate(new Vector3(0, 0, -5), 0.1f, RotateMode.Fast).SetLoops(-1,LoopType.Restart));
    }
}
