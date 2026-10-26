using UnityEngine;

public class AlignTowards : ShipMovement
{
    [SerializeField] private int minStandOffRange;
    [SerializeField] private int maxStandOffRange;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
    
    protected void FixedUpdate()
    {
        if (targetObject != null && !isDead)
        {
            CheckDistanceToTargetObject();
        }
    }
    
    void CheckDistanceToTargetObject()
    {
        if (Vector3.Distance(transform.position, targetObject.transform.position) >= minStandOffRange && Vector3.Distance(transform.position, targetObject.transform.position) <= maxStandOffRange)
        {
            RotateTowardsTargetObject();
            Decelerate();
        }
        else if(Vector3.Distance(transform.position, targetObject.transform.position) > maxStandOffRange || Vector3.Distance(transform.position, targetObject.transform.position) < minStandOffRange)
        {
            RotateTowardsTargetObject();
            MoveShip();
        }
    }
    
    void RotateTowardsTargetObject()
    {
        Vector3 targetVector = targetObject.transform.position - transform.position;

        if (Vector3.Distance(transform.position, targetObject.transform.position) <= minStandOffRange)
        {
            targetVector *= -1;
        }

        targetVector.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(targetVector);
        
        transform.rotation =
            Quaternion.SlerpUnclamped(transform.rotation, targetRotation, shipSO.turnSpeed * Time.deltaTime);
    }
    
    private void OnDrawGizmosSelected()
    {
        if (showGizmos)
        {
            Debug.DrawRay(transform.position, transform.forward * shipSO.lookAheadDistance, Color.blue);
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere(transform.position, minStandOffRange);
            Gizmos.DrawWireSphere(transform.position, maxStandOffRange);
        }
    }
}
