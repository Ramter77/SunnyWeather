using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireball : MonoBehaviour
{
    //! ONLY TO TEST

    #region Variables
    [Tooltip("Projectile to throw")]
    [SerializeField]
    private Rigidbody projectile;

    [Tooltip("Origin of thrown projectile")]
    [SerializeField]    
    private Transform projectileOrigin;

    [Tooltip("HotKey to throw Fireball")]
    [SerializeField]
    private KeyCode hotkey = KeyCode.Mouse0;

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float destroyTime = 10;
    #endregion

    void Update()
    {
        shootProjectile();
    }

    #region shootProjectile
    private void shootProjectile()
    {
        if (Input.GetKeyDown(hotkey)) {
            Rigidbody projectileRB = Instantiate(projectile, projectileOrigin.position, projectileOrigin.rotation);
            projectileRB.velocity = transform.TransformDirection(new Vector3(0, 0, speed));

            Destroy(projectileRB.gameObject, destroyTime);
        }
    }
    #endregion
}
