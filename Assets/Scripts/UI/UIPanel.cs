using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;

public class UIPanel : StateBase
{
    [Header("References")]
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    [Header("Title")]
    [SerializeField] private List<RectTransform> titleButtons;

    [Header("Gameplay")]
    [SerializeField] private List<GameObject> borders;

    [Header("Event")]
    [SerializeField] private VoidEventSO startSpawnEvent;

    [Header("Validation")]
    [SerializeField] private bool isFailedConfig;

    private bool isPauseState;
    private RectTransform rectTitlePanel, rectGameplayPanel, rectGameoverPanel;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(titlePanel == null, "titlePanel is missing!!!");
        CustomLogs.Instance.Warning(gameplayPanel == null, "gameplayPanel is missing!!!");
        CustomLogs.Instance.Warning(pausePanel == null, "pausePanel is missing!!!");
        CustomLogs.Instance.Warning(gameOverPanel == null, "gameOverPanel is missing!!!");

        isFailedConfig = titlePanel == null || gameplayPanel == null
            || pausePanel == null || gameOverPanel == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        if (rectTitlePanel == null)
            rectTitlePanel = titlePanel.GetComponent<RectTransform>();
            
        ShowTitlePanel();
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        if (rectGameplayPanel == null)
            rectGameplayPanel = gameplayPanel.GetComponent<RectTransform>();

        StartCoroutine(HideTitlePanel());
        StartCoroutine(ShowGameplayPanel());

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

        if (rectGameoverPanel == null)
            rectGameoverPanel = gameOverPanel.GetComponent<RectTransform>();

        StartCoroutine(SlowTimeBeforeGameOver());
    }


    private void ShowTitlePanel()
    {
        titlePanel.SetActive(true);

        rectTitlePanel.DOAnchorPos(Vector2.zero, 1.0f)
            .OnComplete(() => ShowButtonTitlePanel());
    }

    private void ShowButtonTitlePanel()
    {
        Sequence buttonsShowlSeq = DOTween.Sequence();

        foreach (var item in titleButtons)
        {
            buttonsShowlSeq.Append(item.DOScale(Vector3.one, 0.1f))
                .Append(item.DOPunchScale(Vector3.one * 0.6f, 0.3f, 6, 0.7f).SetEase(Ease.OutCirc));
        }
    }

    private IEnumerator HideTitlePanel()
    {
        for (int i = titleButtons.Count - 1; i >= 0; i--)
        {
            titleButtons[i].DOScale(Vector3.zero, 0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        rectTitlePanel.DOAnchorPos(new Vector2(0.0f, 500.0f), 1.0f)
            .OnComplete(() => titlePanel.SetActive(false));
    }

    private IEnumerator ShowGameplayPanel()
    {
        gameplayPanel.SetActive(true);

        foreach (var item in borders)
        {
            item.SetActive(true);
        }

        borders[0].GetComponent<Transform>().DOMoveX(-7.5f, 0.5f);
        borders[1].GetComponent<Transform>().DOMoveX(7.5f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        rectGameplayPanel.DOAnchorPos(Vector2.zero, 1.0f)
            .OnComplete(() => startSpawnEvent.RaiseEvent());
    }

    private void ShowPausePanel()
    {
        if (isPauseState)
            pausePanel.SetActive(true);
        else
            pausePanel.SetActive(false);
    }

    IEnumerator SlowTimeBeforeGameOver()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1.0f;
        gameOverPanel.SetActive(true);
    }
}
