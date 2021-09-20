using UnityEngine;

public class ClearAllTarget : MonoBehaviour
{
    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public void DestroyAllTargetOnLayer()
    {
        GameObject[] objects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        if (objects.Length == 0)
            return;

        foreach (var item in objects)
        {
            if (item.layer == 6)
                Destroy(item);
        }
    }
}
