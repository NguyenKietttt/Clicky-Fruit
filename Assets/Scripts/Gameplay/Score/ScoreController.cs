using UnityEngine;

public class ScoreController : StateBase
{
    [Header("Events")]
    [SerializeField] private IntEventSO displayScoreEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private int totalScore;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(displayScoreEvent == null, "displayScoreEvent is missing!!!");

        isFailedConfig = displayScoreEvent == null;
    }


    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        totalScore = 0;
        displayScoreEvent.RaiseEvent(totalScore);
    }


    /// <summary>
    /// Raise by OnGoodTargetClick Event from TargetOnClick
    /// </summary>
    public void UpdateScore(int score)
    {
        if (isFailedConfig)
            return;
            
        totalScore += score;
        displayScoreEvent.RaiseEvent(totalScore);
    }
}
