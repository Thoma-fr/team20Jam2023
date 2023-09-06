using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MG2_GameManager : MonoBehaviour
{
    [Header("Base Settings")]
    [SerializeField] private GameObject[] _padPrefab;
    [SerializeField] private int _nbPad;

    [Header("Game Status")]
    [SerializeField] private int _p1CurrentPad = 0;
    [SerializeField] private bool _p1CanPlay;
    [SerializeField] private int _p2CurrentPad = 0;
    [SerializeField] private bool _p2CanPlay;

    [SerializeField] private List<GameObject> _p1PadList = new();
    [SerializeField] private List<GameObject> _p2PadList = new();

    private KeyCode[] _colorKeys;

    private enum Color { Red, Blue, Green, Yellow}

    private void Start()
    {
        GeneratePadsP1();
        GeneratePadsP2();
        // spawn case verte 
    }

    private void Update()
    {
        if (CheckWin())
            return;

        // check bonne touche
    }

    private void GeneratePadsP1()
    {
        Color previousColor = Color.Green;
        bool canRepeat = true;

        float posY = 0;
        for (int i = 0; i < _nbPad; i++)
        {
            Color currentColor = GetRandomColor();
            
            if (!canRepeat && previousColor == currentColor)
            {
                while (previousColor != currentColor)
                    currentColor = GetRandomColor();
                canRepeat = true;
            }
            else if (canRepeat && previousColor == currentColor)
            {
                canRepeat = false;
            }
            SpawnPad(currentColor, new Vector3(-3, posY, 0), 1);
            previousColor = currentColor;
            posY += 3;
        }
    }

    private void GeneratePadsP2()
    {
        Color previousColor = Color.Green;
        Color currentColor = GetRandomColor();
        bool canRepeat = true;

        float posY = 0;
        for (int i = 0; i < _nbPad; i++)
        {
            if (!canRepeat & previousColor == currentColor)
            {
                currentColor = GetRandomColor();
                canRepeat = true;
            }
            else if (canRepeat & previousColor == currentColor)
            {
                canRepeat = false;
            }
            SpawnPad(GetRandomColor(), new Vector3(3, posY, 0), 2);
            posY += 3;
        }
    }

    private void SpawnPad(Color color, Vector3 pos, int player)
    {
        GameObject pad = Instantiate(GetRandomPrefab(color), pos, Quaternion.identity);
        if (player == 1)
            _p1PadList.Add(pad);
        else if (player == 2)
            _p2PadList.Add(pad);
    }

    private GameObject GetRandomPrefab(Color color)
    {
        int prefabIndex = (int)color * 2 + Random.Range(0, 2);
        return _padPrefab[prefabIndex];
    }

    private Color GetRandomColor()
    {
        return (Color)Random.Range(0, 4);
    }

    private bool CheckWin()
    {
        if (_p1CurrentPad >= _nbPad)
        {
            Debug.Log("p1 win");
            return true;
        }
        else if (_p2CurrentPad >= _nbPad)
        {
            Debug.Log("p2 win");
            return true;
        }
        else return false;
    }

}
