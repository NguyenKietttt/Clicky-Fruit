using UnityEngine;

[CreateAssetMenu(fileName = "New Float Score Data", menuName = "SciptableObject/Score/Float Score Data")]
public class ScoreFloatSO : ScriptableObject
{
    [Header("Score float prefab")]
    [SerializeField] private GameObject prefab;

    [Header("Alpha fade")]
    [SerializeField] [Range(0.0f, 1.0f)] private float intensity;

    [Header("Time")]
    [SerializeField] private float moveUpTime;
    [SerializeField] private float moveToMainScoreTime;
    [SerializeField] private float fadeTime;

    [Header("Main score position")]
    [SerializeField] private Vector3 mainScorePos;


    #region Properties

    public GameObject Prefab => prefab;
    public float Intensity => intensity;
    public float MoveUpTime => moveUpTime;
    public float FadeTime => fadeTime;
    public float MoveToMainScoreTime => moveToMainScoreTime;
    public Vector3 MainScorePos => mainScorePos;

    #endregion
}
