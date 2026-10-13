using UnityEngine;

public class StructureHealth : MonoBehaviour, IHealth
{
    #region --Serialize Field--

    [SerializeField] structureSO structureSO;
    [SerializeField] Rigidbody[] rigidBodies;
    
    #endregion
    
    float currentHealth;
    private bool isHit;
    private bool isDead;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = structureSO.maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        isHit = false;
    }

    public void TakeDamage(float damage)
    {
        if(!isHit)
        {
            isHit = true;
            currentHealth -= damage;

            if (currentHealth <= 0 && !isDead)
            {
                isDead = true;

            }
        }
    }
}
