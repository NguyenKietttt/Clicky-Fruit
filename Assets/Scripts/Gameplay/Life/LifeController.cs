using UnityEngine;

public class LifeController : StateBase
{
    [Header("Configs")]
    [SerializeField] private LifeSO lifeSO;

    [Header("Events")]
    [SerializeField] private VoidEventSO gameoverState;
    [SerializeField] private IntEventSO displayLifeEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private int currentLife;
    private bool isGameover;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(lifeSO == null, "lifeSO is missing!!!");
        CustomLogs.Instance.Warning(gameoverState == null, "gameoverState is missing!!!");
        CustomLogs.Instance.Warning(displayLifeEvent == null, "displayLifeEvent is missing!!!");

        isFailedConfig = lifeSO == null || gameoverState == null || displayLifeEvent == null; 
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManger
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        currentLife = lifeSO.Lives;
        isGameover = false;

        displayLifeEvent.RaiseEvent(currentLife);
    }

    /// <summary>
    /// Raise by GameOVerState Event from StateManger
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        currentLife = 0;
        isGameover = true;

        displayLifeEvent.RaiseEvent(currentLife);
    }

    /// <summary>
    /// Raise by ReduceLife Event from SensorTrigger
    /// </summary>
    public void ReduceOneLife()
    {
        if (isFailedConfig)
            return;
            
        if (isGameover)
            return;

        currentLife--;

        displayLifeEvent.RaiseEvent(currentLife);

        if (currentLife <= 0)
        {
            gameoverState.RaiseEvent();
            isGameover = true;
        }
    }
}
