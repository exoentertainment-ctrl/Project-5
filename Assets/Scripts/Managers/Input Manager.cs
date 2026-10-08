using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    
    public void Accelerate(InputAction.CallbackContext context)
    {
        playerMovement.ChangeAccelerateForce(context.ReadValueAsButton());
    }

    public void RotateLeft(InputAction.CallbackContext context)
    {
        playerMovement.RotateLeft(context.ReadValueAsButton());
    }

    public void RotateRight(InputAction.CallbackContext context)
    {
        playerMovement.RotateRight(context.ReadValueAsButton());
    }
}
