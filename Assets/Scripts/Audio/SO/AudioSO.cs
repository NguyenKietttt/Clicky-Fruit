using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Audio Data", menuName = "SciptableObject/Manager/Audio Data")]
public class AudioSO : ScriptableObject
{
    [Header("List music")]
    [SerializeField] private List<AudioClip> musics;

    [Header("Volume")]
    [SerializeField] [Range(0.0f, 1.0f)] private float musicVolume;


    #region Properties

    public List<AudioClip> Musics => musics;
    public float MusicVolume 
    { 
        get => musicVolume; 
        set => musicVolume = value; 
    }

    #endregion
}
