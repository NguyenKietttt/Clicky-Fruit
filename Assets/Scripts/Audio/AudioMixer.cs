using UnityEngine;

public class AudioMixer : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private AudioSO audioSO;

    [Header("References")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(audioSO == null, "audioSO is missing!!!");

        CustomLogs.Instance.Warning(musicSource == null, "musicSource is missing!!!");
        CustomLogs.Instance.Warning(soundSource == null, "soundSource is missing!!!");

        isFailedConfig = audioSO == null || musicSource == null || soundSource == null;
    }


    /// <summary>
    /// Raise by VolumeSlider on TitlePanel
    /// </summary>
    public void ChangeVolume(float vol)
    {
        if (isFailedConfig)
            return;
            
        musicSource.volume = vol;

        // Save audio volume
        audioSO.MusicVolume = vol;
    }

    /// <summary>
    /// Raise by ChewSFX Event from TargetOnClick
    /// </summary>
    public void PlaySFX(AudioClip sound)
    {
        if (isFailedConfig)
            return;
            
        soundSource.PlayOneShot(sound);
    }
}
