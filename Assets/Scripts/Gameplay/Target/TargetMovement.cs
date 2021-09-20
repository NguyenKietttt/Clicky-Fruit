using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BaseTargetSO targetSO;

    [Header("References")]
    [SerializeField] private Rigidbody targetRb;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private Transform cachedTransform;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(targetSO == null, "targetSO is missing!!!");
        CustomLogs.Instance.Warning(targetRb == null, "targetRb is missing!!!");

        isFailedConfig = targetSO == null || targetRb == null;
    }

    private void OnEnable() 
    {
        if (isFailedConfig)
            enabled = false;

        SpawnRandomOnXRange();

        TossTarget();
        TorqueTarget();
    }


    private void SpawnRandomOnXRange()
    {
        targetRb.velocity = Vector3.zero;
        
        var randomX = Random.Range(-targetSO.XRange, targetSO.XRange);
        var vectorPos = new Vector3(randomX, targetSO.YPos, 0.0f);

        if (cachedTransform == null)
            cachedTransform = transform;

        cachedTransform.position = vectorPos;
    }

    private void TossTarget()
    {
        var randomTossForce = Random.Range(targetSO.MinTossForce, targetSO.MaxTossForce);
        var vectorToss = Vector3.up * randomTossForce;

        targetRb.AddForce(vectorToss, ForceMode.Impulse);
    }

    private void TorqueTarget()
    {
        var x = Random.Range(targetSO.MinTorqueForce, targetSO.MaxTorqueForce);
        var y = Random.Range(targetSO.MinTorqueForce, targetSO.MaxTorqueForce);
        var z = Random.Range(targetSO.MinTorqueForce, targetSO.MaxTorqueForce);

        var vectorToss = new Vector3(x, y, z);
        
        targetRb.AddTorque(vectorToss, ForceMode.Impulse);
    }
}
