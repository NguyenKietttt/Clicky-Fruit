using System.Linq;
using UnityEngine;

public class AudioState : StateBase
{
    [Header("Configs")]
    [SerializeField] private AudioSO audioSO;

    [Header("References")]
    [SerializeField] private AudioSource musicSource;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    
    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(audioSO == null, "audioSO is missing!!!");
        CustomLogs.Instance.Warning(musicSource == null, "audioSource is missing!!!");

        isFailedConfig = audioSO == null || musicSource == null;
    }


    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {        
        if (isFailedConfig)
            return;

        if (!musicSource.isPlaying)
        {
            musicSource.clip = audioSO.Tracks.First();
            musicSource.Play();
        }
    }
}
