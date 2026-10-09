using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class TurretManager : MonoBehaviour
{
    #region  --Serialized Fields --

    [SerializeField] private UnityEvent<Vector3> aimListeners;
    [SerializeField] private UnityEvent fireListeners;
    [SerializeField] UnityEvent secondaryFireListeners;

    #endregion

    private void Start()
    {
        InputManager.instance.SetTurretManagerListener(InvokeFireListeners);
    }
    
    // Update is called once per frame
    void Update()
    {
        SendMousePos();
    }

    void SendMousePos()
    {
        Ray screenRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitInfo;
        
        if (Physics.Raycast(screenRay, out hitInfo, Mathf.Infinity))
            aimListeners?.Invoke(hitInfo.point);
    }

    void InvokeFireListeners()
    {
        fireListeners?.Invoke();
    }
}
