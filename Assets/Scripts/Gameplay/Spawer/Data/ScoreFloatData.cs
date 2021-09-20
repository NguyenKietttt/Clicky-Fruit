using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreFloatData
{
    private int score;
    private Vector3 position;
    private Color color;


    #region Properties

    public int Score { get => score; set => score = value; }
    public Color Color { get => color; set => color = value; }
    public Vector3 Position { get => position; set => position = value; }

    #endregion


    public ScoreFloatData(int score, Vector3 position, Color color)
    {
        this.score = score;
        this.position = position;
        this.color = color;
    }
}
