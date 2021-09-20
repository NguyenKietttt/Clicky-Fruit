using System.Linq;
using UnityEngine;

public class AudioState : StateBase
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
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {        
        if (isFailedConfig)
            return;

        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioSO.Musics.First();
            audioSource.Play();
        }
    }
}
