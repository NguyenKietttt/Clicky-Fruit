using UnityEngine;

public class SensorTrigger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEventSO reduceLifeEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(reduceLifeEvent == null, "reduceLifeEvent is missing!!!");

        isFailedConfig = reduceLifeEvent == null;
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (isFailedConfig)
            return;

        if (other.CompareTag("Good Target"))
            reduceLifeEvent.RaiseEvent();

        Destroy(other.gameObject);
    }
}
