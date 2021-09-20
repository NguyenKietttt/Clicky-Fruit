using UnityEngine;

[CreateAssetMenu(fileName = "New Good Target Data", menuName = "SciptableObject/Target/Good Target Data")]
public class GoodTargetSO : BaseTargetSO
{
    [Header("Point")]
    [SerializeField] [Range(0, 10.0f)] private int point;

    [Header("Color")]
    [SerializeField] private Color color;


    #region Properties

    public int Point => point;

    public Color Color => color;

    #endregion
}
