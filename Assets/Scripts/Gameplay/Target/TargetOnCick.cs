using UnityEngine;

public class TargetOnCick : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BaseTargetSO targetSO;

    [Header("Events")]
    [SerializeField] private IntEventSO onTargetClickEvent;
    [SerializeField] private TextFloatEventSO floatScoreEvent;
    [SerializeField] private VFXEventSO rippleVFXEvent;
    [SerializeField] private VFXEventSO explosionVFXPosEvent;
    [SerializeField] private SFXEventSO chewSFXEvent;

    [Header("Validation")]
    [SerializeField] private bool isFailedConfig;

    private Transform cachedTransform;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(targetSO == null, "targetSO is missing!!!");

        CustomLogs.Instance.Warning(onTargetClickEvent == null, "onTargetClickEvent is missing!!!");
        CustomLogs.Instance.Warning(floatScoreEvent == null, "floatScoreEvent is missing!!!");
        CustomLogs.Instance.Warning(explosionVFXPosEvent == null, "explosionVFXPosEvent is missing!!!");
        CustomLogs.Instance.Warning(explosionVFXPosEvent == null, "explosionVFXPosEvent is missing!!!");
        CustomLogs.Instance.Warning(chewSFXEvent == null, "chewSFXEvent is missing!!!");

        isFailedConfig = targetSO == null || onTargetClickEvent == null || floatScoreEvent == null
            || explosionVFXPosEvent == null || explosionVFXPosEvent == null || chewSFXEvent == null;
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
            var badTarget = (targetSO as BadTargetSO);

            badTarget.Explode(cachedTransform.position);

            RaiseVFXEvent(null, cachedTransform.position, 0.0f, rippleVFXEvent);
            onTargetClickEvent.RaiseEvent(-1); // -1 => Bad Target does not have point
        }

        if (gameObject.CompareTag("Good Target"))
        {
            var goodTarget = (targetSO as GoodTargetSO);

            RaiseSFXEvent(goodTarget.Sounds[0]);
            RaiseFloatScoreEvent(goodTarget.Point);
        }

        RaiseVFXEvent(targetSO.ExplotionVFX, cachedTransform.position, targetSO.Lifetime, explosionVFXPosEvent);
        ObjectPooler.Instance.ReturnGameObjectToPool(gameObject);
    }

    private void RaiseSFXEvent(AudioClip clip)
    {
        chewSFXEvent.RaiseEvent(clip);
    }

    private void RaiseVFXEvent(GameObject vfxObject, Vector3 position, float lifetime, VFXEventSO e)
    {
        var vfx = new VFXData(vfxObject, position, lifetime);
        
        e.RaiseEvent(vfx);
    }

    private void RaiseFloatScoreEvent(int point)
    {
        var scoreData = new ScoreFloatData(point, cachedTransform.position, (targetSO as GoodTargetSO).Color);

        floatScoreEvent.RaiseEvent(scoreData);
    }
}
