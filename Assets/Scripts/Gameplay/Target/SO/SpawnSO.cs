using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct DifficultyDict
{
    [SerializeField] private int index;
    [SerializeField] private string name;
    [SerializeField] [Tooltip("Seconds")] private float spawnTime;


    #region Properties

    public int Index => index;
    public string Name => name;
    public float SpawnTime => spawnTime;

    #endregion
}


[CreateAssetMenu(fileName = "New Spawn Data", menuName = "SciptableObject/Manager/Spawn Data")]
public class SpawnSO : ScriptableObject
{
    [Header("List targets")]
    [SerializeField] private List<GameObject> spawnTargets;

    [Header("List difficulty")]
    [SerializeField] private List<DifficultyDict> difficulties;


    #region Properties

    public List<GameObject> SpawnTargets => spawnTargets;
    public List<DifficultyDict> Difficulties => difficulties;

    #endregion
}
