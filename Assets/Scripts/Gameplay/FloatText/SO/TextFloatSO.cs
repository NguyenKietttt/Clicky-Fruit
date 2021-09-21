using UnityEngine;

[CreateAssetMenu(fileName = "New Float Text Data", menuName = "SciptableObject/Float Text/Float Text Data")]
public class TextFloatSO : ScriptableObject
{
    [Header("Score float prefab")]
    [SerializeField] private GameObject prefab;

    [Header("Alpha fade")]
    [SerializeField] [Range(0.0f, 1.0f)] private float intensity;

    [Header("Time")]
    [SerializeField] private float moveUpTime;
    [SerializeField] private float moveToMainScoreTime;
    [SerializeField] private float fadeTime;

    [Header("Position")]
    [SerializeField] private Vector3 endPos;


    #region Properties

    public GameObject Prefab => prefab;
    public float Intensity => intensity;
    public float MoveUpTime => moveUpTime;
    public float FadeTime => fadeTime;
    public float MoveToMainScoreTime => moveToMainScoreTime;
    public Vector3 EndPos => endPos;

    #endregion
}
