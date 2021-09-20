using UnityEngine;

[CreateAssetMenu(fileName = "New Bad Target Data", menuName = "SciptableObject/Target/Bad Target Data")]
public class BadTargetSO : BaseTargetSO
{
    [Header("Exlotion")]
    [SerializeField] private float radius;
    [SerializeField] private float power;


    public void Explode(Vector3 pos)
    {
        Vector3 explosionPos = pos;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
        
            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F, ForceMode.Impulse);
        }
    }
}
