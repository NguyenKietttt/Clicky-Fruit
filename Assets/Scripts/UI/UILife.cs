using TMPro;
using UnityEngine;

public class UILife : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI lifeText;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(lifeText == null, "lifeText is missing!!!");

        isFailedConfig = lifeText == null;
    }


    /// <summary>
    /// Raise by DisplayLife Event from LifeManager
    /// </summary>
    public void DisplayLife(int life)
    {
        if (isFailedConfig)
            return;

        lifeText.text = "Life\n" + life;
    }
}
