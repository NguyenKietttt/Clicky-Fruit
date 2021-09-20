using UnityEngine;
using UnityEngine.InputSystem;

public class InputCilck : StateBase
{
    [Header("References")]
    [SerializeField] private PlayerInput playerInput;

    [Header("Events")]
    [SerializeField] private IntEventSO playerClickEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private Camera mainCamera;
    private InputAction mousePos;
    private bool isAllowClick;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(playerInput == null, "playerInput is missing!!!");
        CustomLogs.Instance.Warning(playerClickEvent == null, "playerClickEvent is missing!!!");

        isFailedConfig = playerInput == null || playerClickEvent == null;
    }

    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        isAllowClick = false;
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        isAllowClick = true;
    }

    /// <summary>
    /// Raise by GamePauseState Event from InputPause
    /// </summary>
    public override void OnGamePause()
    {
        if (isFailedConfig)
            return;

        isAllowClick = !isAllowClick;
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        isAllowClick = false;
    }


    /// <summary>
    /// Raise by PlayerInput Component from InputManager
    /// </summary>
    public void Click(InputAction.CallbackContext ctx)
    {
        if (isFailedConfig)
            return;

        if (!isAllowClick)
            return;

        if (mousePos == null)
            mousePos = playerInput.actions["MousePosition"];

        if (ctx.started)
            DetectObject(mousePos.ReadValue<Vector2>());
    }

    private void DetectObject(Vector2 mousePos)
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
        
        var ray = mainCamera.ScreenPointToRay(mousePos);

        if (!Physics.Raycast(ray, out var hit, Mathf.Infinity))
            return;

        if (hit.collider != null)
            playerClickEvent.RaiseEvent(hit.collider.gameObject.GetInstanceID());
    }
}
