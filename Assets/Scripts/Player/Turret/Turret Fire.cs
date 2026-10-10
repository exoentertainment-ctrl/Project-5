using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class TurretFire : MonoBehaviour
{
    #region --Serialized Fields--

    [SerializeField] TurretSO turretSO;
    [SerializeField] private Transform[] spawnPoints;

    #endregion
    
    float lastFireTime;
    private bool isFiring;

    ObjectPool projectilePool;
    
    private void Awake()
    {
        projectilePool = GetComponent<ObjectPool>();
    }
    
    private void Start()
    {
        lastFireTime = Time.time;
    }

    private void Update()
    {
        Fire();
    }

    public void ChangeFireState()
    {
        isFiring = !isFiring;
    }

    void Fire()
    {
        if (isFiring)
        {
            if (Time.time - lastFireTime > turretSO.fireRate)
            {
                StartCoroutine(FireRoutine());
            }
        }
    }
    
    protected virtual IEnumerator FireRoutine()
    {
        lastFireTime = Time.time;

        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject projectile = projectilePool.GetPooledObject(); 
            if (projectile != null) {
                projectile.transform.position = spawnPoint.position;
                projectile.transform.rotation = spawnPoint.rotation;
                projectile.SetActive(true);
            }

            // if (turretSO.dischargePrefab != null)
            // {
            //     GameObject muzzle = Instantiate(turretSO.dischargePrefab, spawnPoint.position,
            //         spawnPoint.rotation);
            //     Destroy(muzzle, .1f);
            // }
            //
            // if(turretSO.fireSFX != null)
            //     if(AudioManager.instance != null)
            //         AudioManager.instance.PlaySound(turretSO.fireSFX[Random.Range(0, turretSO.fireSFX.Length)], transform.position);
            //
            // fireFeedbacks?.PlayFeedbacks();
            
            yield return new WaitForSeconds(turretSO.barrelFireDelay);
        }
    }
}
