using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private UnityEvent turretManagerListener;
    
    PlayerMovement playerMovement;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public void SetTurretManagerListener(UnityAction turretManagerAction)
    {
        turretManagerListener.AddListener(turretManagerAction);
    }
    
    #region --Movement--

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

    #endregion

    #region --Mouse--

    public void LeftClick(InputAction.CallbackContext context)
    {
        if(context.performed)
            turretManagerListener.Invoke();
        else if(context.canceled)
            turretManagerListener.Invoke();
    }

    public void RightClick(InputAction.CallbackContext context)
    {
        
    }
    
    #endregion
}
