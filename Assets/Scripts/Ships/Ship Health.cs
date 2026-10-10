using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class ShipHealth : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private BaseShipSO shipSO;
    [SerializeField] UnityEvent onShipDeath;
    [SerializeField] MMFeedbacks deathFeedbacks;

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

                //StartCoroutine(ExplodeRoutine());
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
            
            if(shipSO.explosionPrefab  != null)
                Instantiate(shipSO.explosionPrefab,  randomSpot, Quaternion.identity);
            
            // if(AudioManager.instance != null)
            //     if(shipSO.smallExplosion  != null)
            //         AudioManager.instance.PlaySound(shipSO.smallExplosion, transform.position);
            //         
            // deathFeedbacks?.PlayFeedbacks();
            //
            if (x == (shipSO.numExplosions - 1))
            {
                yield return new WaitForSeconds(shipSO.explosionFrequency);
                Instantiate(shipSO.finalExplosionPrefab, transform.position, Quaternion.identity);
                
                // if(AudioManager.instance != null)
                //     if(shipSO.finalExplosion  != null)
                //         AudioManager.instance.PlaySound(shipSO.finalExplosion, transform.position);
            }
        }
        
        Destroy(gameObject);
    }
}
