using System;
using UnityEngine;
using System.Collections;

public class ProjectileMove : MonoBehaviour
{
    #region Serialized Fields

    [SerializeField] int move;

    #endregion

    private void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.forward * (move * Time.fixedDeltaTime);
    }
}
