using UnityEngine;

public class ShipManager : MonoBehaviour
{
    private GameObject playerShip;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerShip = GameObject.FindGameObjectWithTag("Player");
    }
}
