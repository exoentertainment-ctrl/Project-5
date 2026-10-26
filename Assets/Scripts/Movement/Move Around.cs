using UnityEngine;

public class MoveAround : ShipMovement
{
    //[BoxGroup("Move Around Variables")]
    //[ShowIfGroup("Move Around Variables/movementBehavior", Value = MovementBehavior.MoveAround)]
    [SerializeField] private int minOrbitRadius;
    //[ShowIfGroup("Move Around Variables/movementBehavior", Value = MovementBehavior.MoveAround)]
    [SerializeField] private int maxOrbitRadius;
    
    private Vector3 targetPos;

    // Update is called once per frame
    void Update()
    {
        base.Update();

        if (targetObject != null && !isDead)
        {
            RotateTowardsTargetPos();
            CheckDistanceToTargetPos();
        }
    }
    
    private void FixedUpdate()
    {
        base.FixedUpdate();
    }
    
    //Receive target from ship controller, set new position around target
    protected override void SetTarget(GameObject target)
    {
        if (target != null)
        {
            targetObject = target;
            SetNewTargetPos();

            ActivateEngines();
        }
    }
    
    void RotateTowardsTargetPos()
    {
        Vector3 targetVector = targetPos - transform.position;
        targetVector.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);

        transform.rotation =
            Quaternion.SlerpUnclamped(transform.rotation, targetRotation, shipSO.turnSpeed * Time.deltaTime);
    }
    
    void CheckDistanceToTargetPos()
    {
        if (Vector3.Distance(transform.position, targetPos) <= 1)
            SetNewTargetPos();
    }
    
    //Assign a new random spot around the target
    void SetNewTargetPos()
    {
        Vector3 randomSpot = (Random.insideUnitSphere * Random.Range(minOrbitRadius, maxOrbitRadius));
        
        if(randomSpot.y < -10)
            randomSpot.y = -10;
        
        targetPos = randomSpot + targetObject.transform.position;
    }
    
    protected override void CheckPathAhead()
    {
        Physics.Raycast(transform.position, transform.forward * shipSO.lookAheadDistance, out RaycastHit hitInfo, shipSO.lookAheadDistance, shipSO.obstacleLayerMask);
        
        if(hitInfo.collider != null)
        {
           SetNewTargetPos();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            Debug.DrawRay(transform.position, transform.forward * shipSO.lookAheadDistance, Color.blue);
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(transform.position, minOrbitRadius);
            Gizmos.DrawWireSphere(transform.position, maxOrbitRadius);
        }
    }
}
