using UnityEngine;

public class HealthPassthrough : MonoBehaviour, IHealth
{
    public void TakeDamage(float damage)
    {
        transform.parent.TryGetComponent(out IHealth iHealth);
        if(iHealth != null)
            iHealth.TakeDamage(damage);
    }
}
