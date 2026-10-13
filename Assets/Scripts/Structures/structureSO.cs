using UnityEngine;

[CreateAssetMenu(fileName = "Structure SO", menuName = "Structure SO/Structure")]
public class structureSO : ScriptableObject
{
    public int maxHealth;
    public GameObject[] explosionPrefabs;
    
    public int blastRadius;
    public int blastDamage;
    public int blastForce;
}
