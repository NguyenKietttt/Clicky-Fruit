using UnityEngine;

public class TargetSFX : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BaseTargetSO targetSO;

    [Header("Events")]
    [SerializeField] private SFXEventSO chewSFXEvent;
    [SerializeField] private SFXEventSO spawnSFXEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(targetSO == null, "targetSO is missing!!!");

        CustomLogs.Instance.Warning(chewSFXEvent == null, "chewSFXEvent is missing!!!");
        CustomLogs.Instance.Warning(spawnSFXEvent == null, "spawnSFXEvent is missing!!!");

        isFailedConfig = targetSO == null || chewSFXEvent == null || spawnSFXEvent == null;
    }


    public void PlayClickSFX(int id)
    {
        if (id != gameObject.GetInstanceID())
            return;

        if ((gameObject.CompareTag("Bad Target")))
        {
            var badTarget = (BadTargetSO)targetSO;
            chewSFXEvent.RaiseEvent(badTarget.Sounds[0]);
        }
        else
        {
            var goodTarget = (GoodTargetSO)targetSO;
            chewSFXEvent.RaiseEvent(goodTarget.Sounds[0]);
        }
    }

    public void PlaySpawnSFX(int id)
    {
        if (id != gameObject.GetInstanceID())
            return;

        if ((gameObject.CompareTag("Bad Target")))
        {
            var badTarget = (BadTargetSO)targetSO;
            spawnSFXEvent.RaiseEvent(badTarget.Sounds[1]);
        }
        else
        {
            var goodTarget = (GoodTargetSO)targetSO;
            spawnSFXEvent.RaiseEvent(goodTarget.Sounds[1]);
        }
    }
}
