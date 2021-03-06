using System.Collections;
using UnityEngine;

public class TargetSpawn : StateBase
{
    [Header("Configs")]
    [SerializeField] private SpawnSO spawnSO;

    [Header("Events")]
    [SerializeField] private IntEventSO targetSpawnEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private bool isAllowSpawn;
    private float spawnRate;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(spawnSO == null, "Spawn Data is missing!!!");

        CustomLogs.Instance.Warning(targetSpawnEvent == null, "targetSpawnEvent Data is missing!!!");

        isFailedConfig = spawnSO == null || targetSpawnEvent == null;
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
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public void GetSpawnTime(int index)
    {
        if (isFailedConfig)
            return;

        spawnRate = spawnSO.Difficulties.Find(p => p.Index == index).SpawnTime;
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public void StartSpawn()
    {
        if (isFailedConfig)
            return;

        isAllowSpawn = true;
        
        StartCoroutine(Spawn(spawnRate));
    }

    private IEnumerator Spawn(float time)
    {
        while (isAllowSpawn)
        {
            yield return new WaitForSeconds(time);

            var index = Random.Range(0, spawnSO.SpawnTargets.Count);
            var target = Instantiate(spawnSO.SpawnTargets[index]);
            
            targetSpawnEvent.RaiseEvent(target.gameObject.GetInstanceID());
        }
    }
}
