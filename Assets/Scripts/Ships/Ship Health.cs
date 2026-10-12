using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class ShipHealth : MonoBehaviour, IHealth
{
    #region Serialized Fields

    [SerializeField] private BaseShipSO shipSO;
    [SerializeField] UnityEvent onShipDeath;
    [SerializeField] MMFeedbacks deathFeedbacks;
    [SerializeField] bool showGizmos;

    #endregion

    Rigidbody rigidBody;
    
    #region Variables
    
    private float currentHealth;
    private bool isHit;
    private bool isDead;
    
    #endregion

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = shipSO.maxHealth;
    }

    private void Update()
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
                onShipDeath?.Invoke();
                rigidBody.useGravity = true;

                StartCoroutine(ExplodeRoutine());
            }
        }
    }

    IEnumerator ExplodeRoutine()
    {
        Collider shipCollider = gameObject.GetComponentInChildren<Collider>();
        
        for (int x = 0; x < shipSO.numExplosions; x++)
        {
            yield return new WaitForSeconds(shipSO.explosionFrequency);
            
            Vector3 randomSpot = new Vector3(
                Random.Range(shipCollider.bounds.center.x - shipCollider.bounds.size.x / 2,
                    shipCollider.bounds.center.x + shipCollider.bounds.size.x / 2),
                Random.Range(shipCollider.bounds.center.y - shipCollider.bounds.size.y / 2,
                    shipCollider.bounds.center.y + shipCollider.bounds.size.y / 2),
                Random.Range(shipCollider.bounds.center.z - shipCollider.bounds.size.z / 2,
                    shipCollider.bounds.center.z + shipCollider.bounds.size.z / 2));
            
            if(shipSO.explosionPrefab.Length  > 0)
                Instantiate(shipSO.explosionPrefab[Random.Range(0, shipSO.explosionPrefab.Length)],  randomSpot, Quaternion.identity);
            
            // if(AudioManager.instance != null)
            //     if(shipSO.smallExplosion  != null)
            //         AudioManager.instance.PlaySound(shipSO.smallExplosion, transform.position);
            //         
            // deathFeedbacks?.PlayFeedbacks();
            //
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Terrain")
        {
            ExplodeShip(other.contacts[0].point);
        }
    }

    void ExplodeShip(Vector3 pos)
    {
        Instantiate(shipSO.finalExplosionPrefab[Random.Range(0, shipSO.finalExplosionPrefab.Length)], pos, Quaternion.identity);
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, shipSO.blastRadius);
        if (colliders.Length > 0)
        {
            foreach (var col in colliders)
            {
                col.TryGetComponent(out ShipHealth shipHealth);
                if(shipHealth != null)
                    shipHealth?.TakeDamage(currentHealth);
            }
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if(showGizmos)
            Gizmos.DrawWireSphere(transform.position, shipSO.blastRadius);
    }
}
