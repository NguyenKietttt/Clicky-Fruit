using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private TextFloatEventSO lifeScoreEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(lifeScoreEvent == null, "lifeScoreEvent is missing!!!");

        isFailedConfig = lifeScoreEvent == null;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (isFailedConfig)
            return;

        if (other.CompareTag("Good Target"))
            RaiseFloatLifeEvent(other.gameObject);

        ObjectPooler.Instance.ReturnGameObjectToPool(other.gameObject);
    }

    private void RaiseFloatLifeEvent(GameObject target)
    {
        var scoreData = new ScoreFloatData(-1, target.transform.position, Color.black);
        lifeScoreEvent.RaiseEvent(scoreData);
    }
}
