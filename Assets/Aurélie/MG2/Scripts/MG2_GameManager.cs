using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_GameManager : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] private GameObject[] _padPrefab;
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private int _nbPad;

    [Header("Player 1 Status")]
    [SerializeField] private bool _p1CanPlay = true;
    [SerializeField] private int _p1CurrentPad = 0;
    [SerializeField] private List<GameObject> _p1PadList = new();

    [Header("Player 2 Status")]
    [SerializeField] private bool _p2CanPlay = true;
    [SerializeField] private int _p2CurrentPad = 0;
    [SerializeField] private List<GameObject> _p2PadList = new();

    private ColorKeys _colorKeys = new();
    private Animator _p1Anim;
    private Animator _p2Anim;

    private void Awake()
    {
        _p1Anim = GameObject.Find("Player 1").GetComponent<Animator>();
        _p2Anim = GameObject.Find("Player 2").GetComponent<Animator>();
    }

    private void Start()
    {
        GeneratePadsP1();
        GeneratePadsP2();
    }

    private void Update()
    {
        if (CheckWin())
            return;

        if (_p1CanPlay)
        {
            if (Input.GetKeyDown(_colorKeys.button1Blue.code) || Input.GetKeyDown(_colorKeys.button2Blue.code))
            {
                HandleColorInput(CustomColor.Blue, 1);
                //Debug.Log("p1 press blue");
            }
            else if (Input.GetKeyDown(_colorKeys.button1Red.code) || Input.GetKeyDown(_colorKeys.button2Red.code))
            {
                HandleColorInput(CustomColor.Red, 1);
                //Debug.Log("p1 press red");
            }
            else if (Input.GetKeyDown(_colorKeys.button1Yellow.code))
            {
                HandleColorInput(CustomColor.Yellow, 1);
                //Debug.Log("p1 press yellow");
            }
            else if (Input.GetKeyDown(_colorKeys.button2Green.code))
            {
                HandleColorInput(CustomColor.Green, 1);
                //Debug.Log("p1 press green");
            }
        }

        if (_p2CanPlay)
        {
            if (Input.GetKeyDown(_colorKeys.button3Blue.code))
                HandleColorInput(CustomColor.Blue, 2);
            else if (Input.GetKeyDown(_colorKeys.button3Green.code) || Input.GetKeyDown(_colorKeys.button4Green.code))
                HandleColorInput(CustomColor.Green, 2);
            else if (Input.GetKeyDown(_colorKeys.button3Yellow.code) || Input.GetKeyDown(_colorKeys.button4Yellow.code))
                HandleColorInput(CustomColor.Yellow, 2);
            else if (Input.GetKeyDown(_colorKeys.button4Red.code))
                HandleColorInput(CustomColor.Red, 2);
        }
    }

    private void GeneratePadsP1()
    {
        CustomColor previousColor = CustomColor.Green;
        bool canRepeat = true;
        SpawnRock(new Vector3(-3, -3, 0), 1);
        SpawnPad(CustomColor.Green, new Vector3(-3, 0, 0), 1);

        float posY = 3;
        for (int i = 1; i < _nbPad; i++)
        {
            CustomColor currentColor = GetRandomColor();
            //Debug.Log(currentColor);
            //Debug.Log(canRepeat);

            if (!canRepeat && previousColor == currentColor)
            {
                while (previousColor == currentColor)
                {
                    currentColor = GetRandomColor();
                    //Debug.Log("!canrepeat   " + currentColor);
                }
                canRepeat = true;
            }
            else if (canRepeat && previousColor == currentColor)
                canRepeat = false;
            SpawnPad(currentColor, new Vector3(-3, posY, 0), 1);
            previousColor = currentColor;
            posY += 3;
        }
    }

    private void GeneratePadsP2()
    {
        CustomColor previousColor = CustomColor.Green;
        bool canRepeat = true;
        SpawnRock(new Vector3(3, -3, 0), 2);
        SpawnPad(CustomColor.Green, new Vector3(3, 0, 0), 2);

        float posY = 3;
        for (int i = 1; i < _nbPad; i++)
        {
            CustomColor currentColor = GetRandomColor();

            if (!canRepeat && previousColor == currentColor)
            {
                while (previousColor == currentColor)
                    currentColor = GetRandomColor();
                canRepeat = true;
            }
            else if (canRepeat && previousColor == currentColor)
                canRepeat = false;
            SpawnPad(currentColor, new Vector3(3, posY, 0), 2);
            previousColor = currentColor;
            posY += 3;
        }
    }

    private void SpawnPad(CustomColor color, Vector3 pos, int player)
    {
        GameObject pad = Instantiate(GetRandomPrefab(color), pos, Quaternion.identity);
        if (player == 1)
            _p1PadList.Add(pad);
        else if (player == 2)
            _p2PadList.Add(pad);
    }
    private void SpawnRock(Vector3 pos, int player)
    {
        GameObject rock = Instantiate(_rockPrefab, pos, Quaternion.identity);
        if (player == 1)
            _p1PadList.Add(rock);
        else if (player == 2)
            _p2PadList.Add(rock);
    }

    private GameObject GetRandomPrefab(CustomColor color)
    {
        int prefabIndex = (int)color * 2 + Random.Range(0, 2);
        return _padPrefab[prefabIndex];
    }

    private CustomColor GetRandomColor()
    {
        return (CustomColor)Random.Range(0, 4);
    }

    private void HandleColorInput(CustomColor color, int player)
    {
        int currentPad = (player == 1) ? _p1CurrentPad: _p2CurrentPad;
        List<GameObject> padList = (player == 1) ? _p1PadList : _p2PadList;

        currentPad++;

        //Debug.Log("current pad : "+ currentPad);
        //Debug.Log("current pad color : " + padList[currentPad].GetComponent<MG2_Pad>().color);

        if (padList[currentPad].GetComponent<MG2_Pad>().color == color && currentPad < padList.Count)
        {
            StartCoroutine(MovePads(padList));
            //Debug.Log("good input");

            if (player == 1)
                _p1CurrentPad = currentPad;
            else if (player == 2)
                _p2CurrentPad = currentPad;
        }
        else
        {
            ResetPad(padList);
            
            if (player == 1)
            {
                _p1PadList = padList;
                StartCoroutine(WrongInput(1));
                //Debug.Log("p1 reset");

            }
            else if (player == 2)
            {
                _p2PadList = padList;
                StartCoroutine(WrongInput(2));
                //Debug.Log("p2 reset");
            }
        }
    }

    private IEnumerator MovePads(List<GameObject> padList)
    {
        foreach (GameObject pad in padList)
            pad.transform.DOLocalMoveY(pad.transform.position.y - 3, 0.2f);
        yield return null;
    }

    private IEnumerator WrongInput(int player)
    {
        if (player == 1)
        {
            _p1CanPlay = false;
            _p1Anim.SetBool("Can Play", false);
            _p1CurrentPad = 0;
        }
        else if (player == 2)
        {
            _p2CanPlay = false;
            _p2Anim.SetBool("Can Play", false);
            _p2CurrentPad = 0;
        }

        yield return new WaitForSeconds(0.5f);

        if (player == 1)
        {
            _p1CanPlay = true;
            GeneratePadsP1();
            _p1Anim.SetBool("Can Play", true);
        }
        else if (player == 2)
        {
            _p2CanPlay = true;
            GeneratePadsP2();
            _p2Anim.SetBool("Can Play", true);
        }
    }

    private bool CheckWin()
    {
        if (_p1CurrentPad >= _nbPad)
        {
            //Debug.Log("p1 win");
            return true;
        }
        else if (_p2CurrentPad >= _nbPad)
        {
            //Debug.Log("p2 win");
            return true;
        }
        else return false;
    }

    private void ResetPad(List<GameObject> padList)
    {
        foreach (GameObject pad in padList)
            Destroy(pad);
        padList.Clear();
    }

}
