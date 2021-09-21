using UnityEngine;

public class TargetOnCick : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BaseTargetSO targetSO;

    [Header("Events")]
    [SerializeField] private IntEventSO onTargetClickEvent;
    [SerializeField] private TextFloatEventSO floatScoreEvent;
    [SerializeField] private VFXEventSO explosionVFXPosEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private Transform cachedTransform;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(targetSO == null, "targetSO is missing!!!");

        CustomLogs.Instance.Warning(onTargetClickEvent == null, "onTargetClickEvent is missing!!!");
        CustomLogs.Instance.Warning(floatScoreEvent == null, "floatScoreEvent is missing!!!");
        CustomLogs.Instance.Warning(explosionVFXPosEvent == null, "explosionVFXPosEvent is missing!!!");

        isFailedConfig = targetSO == null || onTargetClickEvent == null || floatScoreEvent == null
            || explosionVFXPosEvent == null;
    }


    /// <summary>
    ///  Raise by ClickByPlayer Event from InputManager
    /// </summary>
    public void OnClick(int instanceID) 
    {
        if (isFailedConfig)
            return;

        if (instanceID != gameObject.GetInstanceID())
            return;

        if (cachedTransform == null)
            cachedTransform = transform;

        if (gameObject.CompareTag("Bad Target"))
        {
            (targetSO as BadTargetSO).Explode(cachedTransform.position);
            onTargetClickEvent.RaiseEvent(-1); // -1 => Bad Target does not have point
        }
        
        if (gameObject.CompareTag("Good Target"))
        {
            int point = (targetSO as GoodTargetSO).Point;

            RaiseFloatScoreEvent(point);
        }

        RaiseVFXEvent();
        ObjectPooler.Instance.ReturnGameObjectToPool(gameObject);
    }

    private void RaiseVFXEvent()
    {
        var vfx = new VFXData(targetSO.ExplotionVFX, cachedTransform.position, targetSO.Lifetime);
        explosionVFXPosEvent.RaiseEvent(vfx);
    }

    private void RaiseFloatScoreEvent(int point)
    {
        var scoreData = new ScoreFloatData(point, cachedTransform.position, (targetSO as GoodTargetSO).Color);
        floatScoreEvent.RaiseEvent(scoreData);
    }
}
