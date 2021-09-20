using UnityEngine;
using UnityEngine.UI;

public class UIAudio : StateBase
{
    [Header("Configs")]
    [SerializeField] private AudioSO audioSO;

    [Header("References")]
    [SerializeField] private Slider musicSlider;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(audioSO == null, "audioSO is missing!!!");
        CustomLogs.Instance.Warning(musicSlider == null, "musicSlider is missing!!!");

        isFailedConfig = audioSO == null || musicSlider == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        musicSlider.value = audioSO.MusicVolume;
    }
}
