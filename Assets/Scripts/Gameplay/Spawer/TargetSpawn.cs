using System.Collections;
using UnityEngine;

public class TargetSpawn : StateBase
{
    [Header("Configs")]
    [SerializeField] private SpawnSO spawnSO;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private bool isAllowSpawn;
    private float spawnRate;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(spawnSO == null, "Spawn Data is missing!!!");

        isFailedConfig = spawnSO == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        isAllowSpawn = false;
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        isAllowSpawn = true;
        
        StartCoroutine(Spawn(spawnRate));
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        isAllowSpawn = false;

        StopCoroutine(Spawn(spawnRate));
    }


    /// <summary>
    /// 0 - Easy 2s
    /// 1 - Normal 1s
    /// 2 - Hard 0.5s
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    /// <param name="cursorIndex"></param>
    public void GetSpawnTime(int index)
    {
        if (isFailedConfig)
            return;

        spawnRate = spawnSO.Difficulties.Find(p => p.Index == index).SpawnTime;
    }

    private IEnumerator Spawn(float time)
    {
        while (isAllowSpawn)
        {
            yield return new WaitForSeconds(time);

            var index = Random.Range(0, spawnSO.SpawnTargets.Count);
            ObjectPooler.Instance.GetObjectFromPool(spawnSO.SpawnTargets[index]);
        }
    }
}
