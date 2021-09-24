using UnityEngine;

public class LifeController : StateBase
{
    [Header("Configs")]
    [SerializeField] private LifeSO lifeSO;
    [SerializeField] private AudioClip clip;

    [Header("Events")]
    [SerializeField] private VoidEventSO gameoverState;
    [SerializeField] private IntEventSO displayLifeEvent;
    [SerializeField] private SFXEventSO chewSFXEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private int currentLife;
    private bool isGameover, isNoSFXPlayed;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(lifeSO == null, "lifeSO is missing!!!");

        CustomLogs.Instance.Warning(gameoverState == null, "gameoverState is missing!!!");
        CustomLogs.Instance.Warning(displayLifeEvent == null, "displayLifeEvent is missing!!!");
        CustomLogs.Instance.Warning(chewSFXEvent == null, "chewSFXEvent is missing!!!");

        isFailedConfig = lifeSO == null || gameoverState == null || displayLifeEvent == null
            || chewSFXEvent == null; 
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManger
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        currentLife = lifeSO.Lives;
        isGameover = isNoSFXPlayed = false;

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

        if (!isNoSFXPlayed)
        {
            chewSFXEvent.RaiseEvent(clip);
            isNoSFXPlayed = true;
        }

        displayLifeEvent.RaiseEvent(currentLife);
    }

    /// <summary>
    /// Raise by ReduceLife Event from FloatTextSpawn
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
