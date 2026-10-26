using UnityEngine;

public class BaseEntitySO : ScriptableObject
{
    public int maxHealth;
    public int maxShield;
    public float lowShieldPercentage;
    
    public GameObject[] explosionPrefab;
    public int numExplosions;
    public float explosionFrequency;
}
