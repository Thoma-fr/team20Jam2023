using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG3_ClockManager : MonoBehaviour
{
    public GameColor colorAvailable;
    [SerializeField] private int DurationRandomMAx, DurationRandomMin;
    private int duration;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private Camera cam;
    [SerializeField] private float startCamSize;
    [SerializeField] private float camspeed;
    private bool canInput;

    void Start()
    {
        cam.orthographicSize = startCamSize;
        duration=Random.Range(DurationRandomMAx, DurationRandomMin);
        StartCoroutine(ticktack());
    }

    void Update()
    {
        if (!canInput)
        {
            cam.orthographicSize -= camspeed * Time.deltaTime;
        }
        else
            cam.orthographicSize = startCamSize;

        if (Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.W))
            Checkcolor(1, GameColor.Red);
        else if (Input.GetKeyUp(KeyCode.S))
            Checkcolor(1, GameColor.Blue);
        else if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.A))
            Checkcolor(1, GameColor.Yellow);
        else if (Input.GetKeyUp(KeyCode.D))
            Checkcolor(1, GameColor.Green);

        if (Input.GetKeyUp(KeyCode.UpArrow))
            Checkcolor(2, GameColor.Red);
        else if (Input.GetKeyUp(KeyCode.DownArrow))
            Checkcolor(2,GameColor.Blue);
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
            Checkcolor(2,GameColor.Yellow);
        else if (Input.GetKeyUp(KeyCode.RightArrow))
            Checkcolor(2, GameColor.Green);
    }
    public void animCadran()
    {
        Sequence sequence = DOTween.Sequence();

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
}
