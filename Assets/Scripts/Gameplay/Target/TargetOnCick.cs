using UnityEngine;

public class TargetOnCick : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private BaseTargetSO targetSO;

    [Header("Events")]
    [SerializeField] private IntEventSO onTargetClickEvent;
    [SerializeField] private ScoreEventSO floatScoreEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    public GameObject floatScore;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(targetSO == null, "targetSO is missing!!!");
        CustomLogs.Instance.Warning(onTargetClickEvent == null, "onTargetClickEvent is missing!!!");

        isFailedConfig = targetSO == null || onTargetClickEvent == null;
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

        if (gameObject.CompareTag("Bad Target"))
        {
            (targetSO as BadTargetSO).Explode(transform.position);
            onTargetClickEvent.RaiseEvent(-1); // -1 => Bad Target does not have point
        }
        
        if (gameObject.CompareTag("Good Target"))
        {
            int point = (targetSO as GoodTargetSO).Point;

            SpawnFloatText(point);

            onTargetClickEvent.RaiseEvent(point);
        }


        ObjectPooler.Instance.ReturnGameObjectToPool(gameObject);
        SpawnExplotionVFX();
    }

    private void SpawnExplotionVFX()
    {
        GameObject explotion = Instantiate(targetSO.ExplotionVFX, transform.position, 
            targetSO.ExplotionVFX.transform.rotation);

        Destroy(explotion, targetSO.Lifetime);
    }

    private void SpawnFloatText(int point)
    {
        var temp = Instantiate(floatScore, transform.position, Quaternion.identity);
            ScoreData scoreData = new ScoreData()
            {
                Id = temp.GetInstanceID(),
                Score = point,
                Color = (targetSO as GoodTargetSO).Color
            };
            
        floatScoreEvent.RaiseEvent(scoreData);
    }
}
