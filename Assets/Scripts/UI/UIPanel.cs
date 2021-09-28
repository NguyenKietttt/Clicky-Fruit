using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class UIPanel : StateBase
{

    [Header("Title HUD")]
    [SerializeField] private GameObject titleCanvas;
    [SerializeField] private GameObject titlePanel;
    [SerializeField] private List<RectTransform> titleButtons;

    [Header("Gameplay HUD")]
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private List<GameObject> borders;

    [Header("Gameover HUD")]
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameoverBtn;

    [Header("Countdown HUD")]
    [SerializeField] private GameObject countdownCanvas;
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("Event")]
    [SerializeField] private VoidEventSO startSpawnEvent;

    [Header("Validation")]
    [SerializeField] private bool isFailedConfig;

    private int countdown;
    private RectTransform rectTitlePanel, rectGameplayPanel, rectGameoverPanel, rectGameOverbtn, rectCountDown;
    private GraphicRaycaster raycasterTitle, raycasterGameover;


    private void OnValidate()
    {
        CustomLogs.Instance.Warning(titlePanel == null, "titlePanel is missing!!!");
        CustomLogs.Instance.Warning(gameplayPanel == null, "gameplayPanel is missing!!!");
        CustomLogs.Instance.Warning(gameOverPanel == null, "gameOverPanel is missing!!!");

        isFailedConfig = titlePanel == null || gameplayPanel == null
            || gameOverPanel == null;
    }

    private void Awake()
    {
        rectTitlePanel = titlePanel.GetComponent<RectTransform>();
        rectGameplayPanel = gameplayPanel.GetComponent<RectTransform>();
        rectGameoverPanel = gameOverPanel.GetComponent<RectTransform>();
        rectGameOverbtn = gameoverBtn.GetComponent<RectTransform>();
        rectCountDown = countdownText.GetComponent<RectTransform>();

        raycasterTitle = titleCanvas.GetComponent<GraphicRaycaster>();
        raycasterGameover = gameOverCanvas.GetComponent<GraphicRaycaster>();
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        StartCoroutine(HideGameplayPanel());
        StartCoroutine(HideGameoverPanel());
        ShowTitlePanel();
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        countdown = 3;

        if (isFailedConfig)
            return;

        StartCoroutine(HideTitlePanel());
        StartCoroutine(ShowGameplayPanel());
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        StartCoroutine(SlowTimeBeforeGameOver());
    }


    #region Title Panel
    private void ShowTitlePanel()
    {
        rectTitlePanel.DOAnchorPos(Vector2.zero, 1.0f)
            .OnComplete(() => StartCoroutine(ShowButtonTitlePanel()));
    }

    private IEnumerator ShowButtonTitlePanel()
    {
        Sequence buttonsShowlSeq = DOTween.Sequence();

        foreach (var item in titleButtons)
        {
            buttonsShowlSeq.Append(item.DOScale(Vector3.one, 0.1f))
                .Append(item.DOPunchScale(Vector3.one * 0.6f, 0.3f, 6, 0.7f).SetEase(Ease.OutCirc));
        }

        yield return new WaitForSeconds(1.2f);

        raycasterTitle.enabled = true;
    }

    private IEnumerator HideTitlePanel()
    {
        raycasterTitle.enabled = false;

        for (int i = titleButtons.Count - 1; i >= 0; i--)
        {
            titleButtons[i].DOScale(Vector3.zero, 0.1f);
        }

        yield return new WaitForSeconds(0.3f);

        rectTitlePanel.DOAnchorPos(new Vector2(0.0f, 500.0f), 1.0f);
    }

    #endregion

    #region Gameplay Panel
    private IEnumerator ShowGameplayPanel()
    {
        borders[0].GetComponent<Transform>().DOMoveX(-7.5f, 0.5f);
        borders[1].GetComponent<Transform>().DOMoveX(7.5f, 0.5f);

        yield return new WaitForSeconds(0.3f);

        rectGameplayPanel.DOAnchorPos(Vector2.zero, 1.0f)
            .OnComplete(() => StartCoroutine(CountDown()));
    }

    private IEnumerator HideGameplayPanel()
    {
        rectGameplayPanel.DOAnchorPos(new Vector2(0.0f, 500.0f), 1.0f);

        yield return new WaitForSeconds(0.3f);

        borders[0].GetComponent<Transform>().DOMoveX(-11.5f, 0.5f);
        borders[1].GetComponent<Transform>().DOMoveX(11.5f, 0.5f);
    }

    #endregion

    #region Gameover Panel

    IEnumerator SlowTimeBeforeGameOver()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1.0f;

        ShowGameoverPanel();
    }

    private void ShowGameoverPanel()
    {
        rectGameoverPanel.DOAnchorPos(Vector2.zero, 1.0f)
            .OnComplete(() => StartCoroutine(ShowButtonGameoverPanel()));
    }

    private IEnumerator ShowButtonGameoverPanel()
    {
        Sequence buttonsShowlSeq = DOTween.Sequence();

        buttonsShowlSeq.Append(rectGameOverbtn.DOScale(Vector3.one, 0.1f))
            .Append(rectGameOverbtn.DOPunchScale(Vector3.one * 0.6f, 0.3f, 6, 0.7f).SetEase(Ease.OutCirc));

        yield return new WaitForSeconds(0.5f);

        raycasterGameover.enabled = true;
    }

    private IEnumerator HideGameoverPanel()
    {
        raycasterGameover.enabled = false;
        rectGameOverbtn.DOScale(Vector3.zero, 0.1f);

        yield return new WaitForSeconds(0.3f);

        rectGameoverPanel.DOAnchorPos(new Vector2(0.0f, 500.0f), 1.0f);
    }

    #endregion

    #region CountDown

    private IEnumerator CountDown()
    {
        countdownCanvas.SetActive(true);
         countdownText.SetText(countdown.ToString());

        while (countdown > 0)
        {
            Sequence countdownSeq = DOTween.Sequence();
            countdownSeq.Append(rectCountDown.DOScale(Vector2.one * 2.0f, 0.5f))
                .Append(countdownText.DOFade(0.0f, 0.5f))
                .OnComplete(() => countdown--);

            yield return new WaitForSeconds(1.0f);

            countdownText.SetText(countdown.ToString());

            rectCountDown.localScale = Vector3.one;
            countdownText.color = new Color(1, 1, 1, 1);
        }

        countdownText.SetText("START!");

        yield return new WaitForSeconds(1.0f);

        countdownCanvas.SetActive(false);
        startSpawnEvent.RaiseEvent();
    }

    #endregion
}
