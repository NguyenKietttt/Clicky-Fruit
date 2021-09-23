using System.Collections.Generic;
using UnityEngine;

public class BaseTargetSO : ScriptableObject
{
    [Header("Toss")]
    [SerializeField] private float minTossForce;
    [SerializeField] private float maxTossForce;

    [Header("Torque")]
    [SerializeField] private float minTorqueForce;
    [SerializeField] private float maxTorqueForce;

    [Header("Position")]
    [SerializeField] [Range(-4.0f, 4.0f)] private float xRange;
    [SerializeField] [Range(-6.0f, -1.0f)] private float yPos;

    [Header("VFX")]
    [SerializeField] private GameObject explotionVFX;
    [SerializeField] private float lifetime;

    [Header("Sound")]
    [SerializeField] private List<AudioClip> sounds;


    #region Properties

    public float MinTossForce => minTossForce;
    public float MaxTossForce => maxTossForce;
    public float MinTorqueForce => minTorqueForce;
    public float MaxTorqueForce => maxTorqueForce;
    public float XRange => xRange;
    public float YPos => yPos;
    public GameObject ExplotionVFX => explotionVFX;
    public float Lifetime => lifetime;
    public List<AudioClip> Sounds => sounds;

    #endregion
}
