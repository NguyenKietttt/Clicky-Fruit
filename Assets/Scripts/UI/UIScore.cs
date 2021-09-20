using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(scoreText == null, "scoreText is missing!!!");

        isFailedConfig = scoreText == null;
    }


    /// <summary>
    /// Raise by DisplayScore Event from ScoreManager
    /// </summary>
    public void DisplayScore(int score)
    {
        if (isFailedConfig)
            return;

        scoreText.text = "Score\n" + score;
    }
}
