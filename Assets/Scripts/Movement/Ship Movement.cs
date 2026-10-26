using System;
using MoreMountains.Tools;
using UnityEngine;
using Random = UnityEngine.Random;
//using Sirenix.OdinInspector;

public class ShipMovement : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] private bool isPlayer;
    [SerializeField] protected BaseShipSO shipSO;
    [SerializeField] private GameObject engines;

    [Tooltip("This is the step size for the raycast that looks for detour routes")] 
    [SerializeField] private int radarStepAmount;

    [SerializeField] protected bool showGizmos;
    
    #endregion

    #region Variables

    Rigidbody rb;
    
    protected GameObject targetObject;

    private int radarAngleX = -45;
    private int radarAngleY = -45;
    
    private Vector3 currentSpeed;

    protected bool isDead;
    
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    protected void Start()
    {
        
    }

    protected void Update()
    {
        if (targetObject == null)
        {
            DeactivateEngines();
            SearchForTarget();
        }
    }

    protected void FixedUpdate()
    {
        if (targetObject != null)
        {
            CheckPathAhead();
            MoveShip();
        }
    }

    public void SetDead()
    {
        isDead = true;
    }
    
    //If either a standard move or move to target is set
    protected void MoveShip()
    {
        if (!isDead)
        {
            if (currentSpeed.z < shipSO.maxAccelerateForce)
                currentSpeed.z += shipSO.accelerateForce * Time.fixedDeltaTime;

            //transform.position += transform.forward * ((shipSO.accelerateForce * movementModifier)* Time.deltaTime);
            rb.linearVelocity = transform.forward * currentSpeed.z;
        }
    }

    protected void Decelerate()
    {
        if (currentSpeed.z > 0)
        {
            currentSpeed.z -= shipSO.decelerateForce * Time.fixedDeltaTime;
                
            if(currentSpeed.z < 0)
                currentSpeed.z = 0;
        }
            
        rb.linearVelocity = transform.forward * currentSpeed.z;
    }
    
    //Receive target from ship controller, set new position around target
    protected virtual void SetTarget(GameObject target)
    {
        if (target != null)
        {
            targetObject = target;

            ActivateEngines();
        }
    }

    protected virtual void CheckPathAhead()
    {
    }

    void SearchForTarget()
    {
        Collider[] possibleTargets =
            Physics.OverlapSphere(transform.position, Mathf.Infinity, shipSO.targetLayerMask);
        GameObject potentialTarget = null;

        if (possibleTargets.Length > 0)
        {
            float closestEnemy = Mathf.Infinity;

            for (int x = 0; x < possibleTargets.Length; x++)
            {
                float distanceToEnemy =
                    Vector3.Distance(possibleTargets[x].transform.position, transform.position);

                if (distanceToEnemy < closestEnemy)
                {
                    closestEnemy = distanceToEnemy;

                    potentialTarget = possibleTargets[x].transform.root.gameObject;
                }
            }

            if (potentialTarget != null)
                SetTarget(potentialTarget);
        }
    }
    
    protected void ActivateEngines()
    {
        engines?.SetActive(true);
    }
    
    void DeactivateEngines()
    {
        engines?.SetActive(false);
    }
}
