using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Data", menuName = "SciptableObject/Manager/Audio Data")]
public class AudioSO : ScriptableObject
{
    [Header("Music")]
    [SerializeField] private List<AudioClip> tracks;
    [SerializeField] [Range(0.0f, 1.0f)] private float musicVolume;

    [Header("Sound")]
    [SerializeField] [Range(0.0f, 1.0f)] private float soundVolume;


    #region Properties

    public List<AudioClip> Tracks => tracks;
    public float MusicVolume 
    { 
        get => musicVolume; 
        set => musicVolume = value; 
    }
    public float SoundVolume 
    { 
        get => soundVolume; 
        set => soundVolume = value; 
    }

    #endregion
}
