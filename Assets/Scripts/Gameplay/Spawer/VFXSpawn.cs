using UnityEngine;

public class VFXSpawn : MonoBehaviour
{
    /// <summary>
    /// Raise by explosionVFXPosEvent Event from TargetOnClick
    /// </summary>
    public void SpawnExplotionVFX(VFXData vFXData)
    {
        var explotion = Instantiate(vFXData.Vfx, vFXData.Position, Quaternion.identity);
        Destroy(explotion, vFXData.LifeTime);
    }
}