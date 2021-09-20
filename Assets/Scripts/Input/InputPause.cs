using UnityEngine;
using UnityEngine.InputSystem;

public class InputPause : StateBase
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Events")]
    [SerializeField] private VoidEventSO pauseGameState;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private bool isPause, isAllowPause;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(playerInput == null, "playerInput is missing!!!");
        CustomLogs.Instance.Warning(pauseGameState == null, "pauseGameEvent is missing!!!");

        isFailedConfig = playerInput == null ||pauseGameState == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        isAllowPause = false;
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        isAllowPause = true;
        isPause = false;
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        isAllowPause = false;
    }


    /// <summary>
    /// Raise by PlayerInput Component from InputManager
    /// </summary>
    public void Pause(InputAction.CallbackContext ctx)
    {
        if (!isAllowPause)
            return;

        if (ctx.started)
        {
            pauseGameState.RaiseEvent();
            isPause = !isPause;

            if (isPause)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
}
