using UnityEngine;

[CreateAssetMenu(fileName = "New RippleVFX Data", menuName = "SciptableObject/VFX/RippleVFX Data")]
public class RippleSO : ScriptableObject
{
    [Header("Ripple")]
    [SerializeField] private Material rippleMaterial;
    [SerializeField] private float maxAmount;
    [SerializeField] [Range(0, 1)] private float friction;

    [Header("SLow time")]
    [SerializeField] [Range(0, 1)] private float durable;

    #region Properties

    public Material RippleMaterial => rippleMaterial;
    public float MaxAmount => maxAmount;
    public float Friction => friction;
    public float Durable => durable;

    #endregion
}
