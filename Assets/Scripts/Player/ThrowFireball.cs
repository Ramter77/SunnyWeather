using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireball : MonoBehaviour
{
    //! ONLY TO TEST
    //Todo: convert this to player attack script?

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
    private float attackCD = 0.1f;
    private float attackSpeed;

    [SerializeField]
    private float speed = 10;

    [SerializeField]
    private float destroyTime = 10;



    private Animator playerAnim;
    #endregion

    void Start() {
        playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        shootProjectile();
    }

    #region shootProjectile
    private void shootProjectile()
    {
        if (Input.GetKeyDown(hotkey)) {
            if (Time.time > attackSpeed) {
                attackSpeed = Time.time + attackCD;

                playerAnim.SetTrigger("Attack");

                Rigidbody projectileRB = Instantiate(projectile, projectileOrigin.position, projectileOrigin.rotation);
                
                
                //TODO: DO these in projectile script
                projectileRB.velocity = transform.TransformDirection(new Vector3(0, 0, speed));

                Destroy(projectileRB.gameObject, destroyTime);
            }
        }
    }
    #endregion
}
