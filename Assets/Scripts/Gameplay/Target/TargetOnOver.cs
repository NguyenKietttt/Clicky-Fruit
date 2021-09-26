using UnityEngine;

public class TargetOnOver : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private IntEventSO onTargetOver;

    [Header("Validation")]
    [SerializeField] private bool isFailedConfig;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(onTargetOver == null, "onTargetOver is missing!!!");

        isFailedConfig = onTargetOver == null;
    }


    private void OnMouseOver() 
    {
        onTargetOver.RaiseEvent(1); // 1 => hand cursor
    }

    private void OnMouseExit() 
    {
        onTargetOver.RaiseEvent(0); // 0 => pointer cursor
    }
}
