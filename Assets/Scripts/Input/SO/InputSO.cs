using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct CursorDict
{
    [SerializeField] private int index;
    [SerializeField] private Texture2D texture;


    #region Properties

    public int Index => index;
    public Texture2D Texture => texture;

    #endregion
}


[CreateAssetMenu(fileName = "New Input Data", menuName = "SciptableObject/Manager/Input Data")]
public class InputSO : ScriptableObject
{
    [Header("Cursor Images")]
    [SerializeField] private List<CursorDict> cursors;


    #region Properties

    public List<CursorDict> Cursors => cursors;

    #endregion
}
