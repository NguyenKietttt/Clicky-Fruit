using UnityEngine;

public class StateManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private VoidEventSO titleMenuState;
    [SerializeField] private IntEventSO gameplayState;
    [SerializeField] private VoidEventSO gameOverState;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(titleMenuState == null, "titleMenuState is missing!!!");
        CustomLogs.Instance.Warning(gameplayState == null, "gameplayState is missing!!!");
        CustomLogs.Instance.Warning(gameOverState == null, "gameOverState is missing!!!");

        isFailedConfig = titleMenuState == null || gameplayState == null || gameOverState == null;
    }

    private void OnEnable() 
    {
        if (isFailedConfig)
            enabled = false;
    }

    private void Start()
    {
        RaiseTitleMenuState();
    }

    
    /// <summary>
    /// Raise by:
    ///  <br> - New game </br>
    ///  <br> - RestartButton on GameOverPanel </br>
    /// </summary>
    public void RaiseTitleMenuState()
    {
        if (isFailedConfig)
            return;

        titleMenuState.RaiseEvent();
    }

    /// <summary>
    /// Raise by Buttons on TitlePanel
    /// </summary>
    public void RaiseGamplayState(int index)
    {
        if (isFailedConfig)
            return;

        gameplayState.RaiseEvent(index);
    }

    /// <summary>
    /// Raise by:
    /// <br> - OnBadTargetClick Event from OnTargetClick </br>
    /// <br> - LifeManager when out of life </br>
    /// </summary>
    public void RaiseGameOverState()
    {
        if (isFailedConfig)
            return;

        gameOverState.RaiseEvent();
    }
}
