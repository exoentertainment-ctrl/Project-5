using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileHit : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] ProjectileSO projectileSO;
    
    #endregion

    #region Variables

    private Collider collider;
    private float damageUpgrade = 1;

    #endregion

    private void Awake()
    {
        collider  = GetComponent<Collider>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        
        other.gameObject.TryGetComponent<ShipHealth>(out var shipHealth);
        if(shipHealth != null)
            shipHealth.TakeDamage(projectileSO.damage);
        
        if(projectileSO.impactPrefab.Length > 0)
            Instantiate(projectileSO.impactPrefab[Random.Range(0, projectileSO.impactPrefab.Length)], other.contacts[0].point, transform.rotation);
        
        gameObject.SetActive(false);
    }
}
