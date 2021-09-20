using TMPro;
using UnityEngine;

public class UIPanel : StateBase
{
    [Header("References")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private bool isPauseState;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(titlePanel == null, "titlePanel is missing!!!");
        CustomLogs.Instance.Warning(gameplayPanel == null, "gameplayPanel is missing!!!");
        CustomLogs.Instance.Warning(pausePanel == null, "pausePanel is missing!!!");
        CustomLogs.Instance.Warning(gameOverPanel == null, "gameOverPanel is missing!!!");

        isFailedConfig = titlePanel == null || gameplayPanel == null 
            || pausePanel == null  || gameOverPanel == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        titlePanel.SetActive(true);
        gameplayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        titlePanel.SetActive(false);
        gameplayPanel.SetActive(true);

        isPauseState = false;
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGamePause()
    {
        if (isFailedConfig)
            return;

        isPauseState = !isPauseState;
        ShowPausePanel();
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        gameOverPanel.SetActive(true);
    }


    private void ShowPausePanel()
    {
        if (isPauseState)
            pausePanel.SetActive(true);
        else
            pausePanel.SetActive(false);
    }
}
