using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private AudioSO audioSO;

    [Header("References")]
    [SerializeField] private AudioSource audioSource;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(audioSO == null, "audioSO is missing!!!");
        CustomLogs.Instance.Warning(audioSource == null, "audioSource is missing!!!");

        isFailedConfig = audioSO == null || audioSource == null;
    }


    /// <summary>
    /// Raise by VolumeSlider on TitlePanel
    /// </summary>
    public void ChangeVolume(float vol)
    {
        if (isFailedConfig)
            return;
            
        audioSource.volume = vol;

        // Save audio volume
        audioSO.MusicVolume = vol;
    }
}
