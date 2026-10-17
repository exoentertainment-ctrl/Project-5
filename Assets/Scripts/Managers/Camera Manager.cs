using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region --Serialized Fields--

    [SerializeField] CinemachineVirtualCameraBase camera;
    [SerializeField] CinemachinePositionComposer  positionComposer;
    [SerializeField] private int scrollAmount;
    [SerializeField] int maxScrollAmount;

    #endregion
    
    public static CameraManager instance;
    
    int currentScrollAmount;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void ScrollOut()
    {
        if (currentScrollAmount < maxScrollAmount)
        {
            positionComposer.CameraDistance += scrollAmount;
            currentScrollAmount += scrollAmount;
        }
    }

    public void ScrollIn()
    {
        if (currentScrollAmount > -maxScrollAmount)
        {
            positionComposer.CameraDistance -= scrollAmount;
            currentScrollAmount -= scrollAmount;
        }
    }
}
