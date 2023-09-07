using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameColor { Red, Blue, Green, Yellow, Rock };

public class ColorKey
{
    public int player;
    public int button;
    public GameColor color;
    public KeyCode code;

    public ColorKey(int p, int b, GameColor c, KeyCode k)
    {
        player = p;
        button = b;
        color = c;
        code = k;
    }
}

public class ColorKeys
{
    public ColorKey button1Blue = new(1, 1, GameColor.Blue, KeyCode.E);
    public ColorKey button1Red = new(1, 1, GameColor.Red, KeyCode.Z);
    public ColorKey button1Yellow = new(1, 1, GameColor.Yellow, KeyCode.A);

    public ColorKey button2Blue = new(1, 2, GameColor.Blue, KeyCode.R);
    public ColorKey button2Green = new(1, 2, GameColor.Green, KeyCode.Y);
    public ColorKey button2Red = new(1, 2, GameColor.Red, KeyCode.T);

    public ColorKey button3Blue = new(1, 3, GameColor.Blue, KeyCode.U);
    public ColorKey button3Green = new(1, 3, GameColor.Green, KeyCode.I);
    public ColorKey button3Yellow = new(1, 3, GameColor.Yellow, KeyCode.O);

    public ColorKey button4Green = new(1, 4, GameColor.Green, KeyCode.P);
    public ColorKey button4Red = new(1, 4, GameColor.Red, KeyCode.Q);
    public ColorKey button4Yellow = new(1, 4, GameColor.Yellow, KeyCode.S);
}
