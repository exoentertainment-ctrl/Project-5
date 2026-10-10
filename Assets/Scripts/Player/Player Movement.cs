using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region --Serialize Field--

    [SerializeField] private BaseShipSO shipSO;

    #endregion

    #region --Components

    Rigidbody rb;

    #endregion


    private bool isAccelerating;
    bool isRotatingLeft;
    bool isRotatingRight;
    
    private Vector3 currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        currentSpeed = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    public void ChangeAccelerateForce(bool value)
    {
        isAccelerating = value;
    }

    public void RotateLeft(bool value)
    {
        isRotatingLeft = value;
    }

    public void RotateRight(bool value)
    {   
        isRotatingRight = value;
    }

    void Move()
    {
        //If player is pressing move button, accelerate ship until it hits the max speed
        if (isAccelerating)
        {
            if (currentSpeed.z < shipSO.maxAccelerateForce)
                currentSpeed.z += shipSO.accelerateForce * Time.fixedDeltaTime;
            
            rb.linearVelocity = transform.forward * currentSpeed.z;
        }
        //Otherwise, slowly decelerate ship until it stops moving
        else
        {
            if (currentSpeed.z > 0)
            {
                currentSpeed.z -= shipSO.decelerateForce * Time.fixedDeltaTime;
                
                if(currentSpeed.z < 0)
                    currentSpeed.z = 0;
            }
            
            rb.linearVelocity = transform.forward * currentSpeed.z;
        }
    }

    void Rotate()
    {
        if(isRotatingLeft)
            transform.Rotate(Vector3.up * (-shipSO.turnSpeed * Time.fixedDeltaTime));
        else if(isRotatingRight)
            transform.Rotate(Vector3.up * (shipSO.turnSpeed * Time.fixedDeltaTime));
    }
}
