using UnityEngine;

[CreateAssetMenu(fileName = "New Life Data", menuName = "SciptableObject/Manager/Life Data")]
public class LifeSO : ScriptableObject
{
    [Tooltip("Times player can miss click")]
    [SerializeField] private int lives;


    #region Properties

    public int Lives => lives;

    #endregion
}
