using MoreMountains.Feedbacks;
using UnityEngine;

public class ExplosiveStructureHealth : MonoBehaviour, IHealth
{
    #region --Serialize Field--

    [SerializeField] structureSO structureSO;
    [SerializeField] MMFeedbacks feedbacks;
    
    [SerializeField] bool showGizmos;
    
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
                Explode();
            }
        }
    }
    

    void Explode()
    {
        feedbacks?.PlayFeedbacks();

        if (structureSO.explosionPrefabs.Length > 0)
        {
            Instantiate(structureSO.explosionPrefabs[Random.Range(0, structureSO.explosionPrefabs.Length)], transform.position, transform.rotation);
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, structureSO.blastRadius);
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                col.TryGetComponent(out IHealth iHealth);
                if(iHealth != null)
                    iHealth?.TakeDamage(structureSO.blastDamage);
            }
        }
        
        feedbacks?.PlayFeedbacks();
        
        Destroy(gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        if(showGizmos)
            Gizmos.DrawWireSphere(transform.position, structureSO.blastRadius);
    }
}
