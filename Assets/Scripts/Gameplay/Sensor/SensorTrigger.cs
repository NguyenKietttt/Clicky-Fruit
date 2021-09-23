using UnityEngine;

public class SensorTrigger : StateBase
{
    [Header("Configs")]
    [SerializeField] private AudioClip clip;

    [Header("Events")]
    [SerializeField] private TextFloatEventSO lifeScoreEvent;
    [SerializeField] private SFXEventSO sensorSFXEvent;

    [Header("Validation")]
    [SerializeField] private bool isFailedConfig;

    private bool isGameOver;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(clip == null, "clip is missing!!!");

        CustomLogs.Instance.Warning(lifeScoreEvent == null, "lifeScoreEvent is missing!!!");
        CustomLogs.Instance.Warning(sensorSFXEvent == null, "sensorSFXEvent is missing!!!");

        isFailedConfig = clip == null || lifeScoreEvent == null || sensorSFXEvent == null;
    }


    public override void OnGameplay()
    {
        isGameOver = false;
    }

    public override void OnGameOver()
    {
        isGameOver = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isFailedConfig)
            return;

        if (other.CompareTag("Good Target"))
        {
            if (!isGameOver)
                sensorSFXEvent.RaiseEvent(clip);

            RaiseFloatLifeEvent(other.gameObject);
        }

        ObjectPooler.Instance.ReturnGameObjectToPool(other.gameObject);
    }

    private void RaiseFloatLifeEvent(GameObject target)
    {
        var scoreData = new ScoreFloatData(-1, target.transform.position, Color.black);
        lifeScoreEvent.RaiseEvent(scoreData);
    }
}
