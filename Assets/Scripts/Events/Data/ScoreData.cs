using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData
{
    private int id;
    private int score;
    private Color color;
    

    #region Properties

    public int Id { get => id; set => id = value; }
    public int Score { get => score; set => score = value; }
    public Color Color { get => color; set => color = value; }

    #endregion
}
