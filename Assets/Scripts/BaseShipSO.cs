using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Base Ship SO", menuName = "Ships SO/Base Ship")]
public class BaseShipSO : BaseEntitySO
{
    #region Health Variables
    
    public float shieldRechargeRate;
    public int shieldDownDuration;
    
    #endregion

    #region Movement Variables

    public int accelerateForce;
    public int decelerateForce;
    public int maxAccelerateForce;
    public float turnSpeed;
    public LayerMask obstacleLayerMask;
    public LayerMask targetLayerMask;

    #endregion

    #region SFX Variables

    // public AudioClipSO smallExplosion;
    // public AudioClipSO finalExplosion;
 
    #endregion

    #region Explosion  Variables
    
    public GameObject finalExplosionPrefab;

    #endregion
}
