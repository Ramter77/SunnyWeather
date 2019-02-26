using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAndDamage : MonoBehaviour
{
    [Header("Attack Stats")]
    private float damage = 20f; // flat damage amount
    private float attackSpeed = 2f; // 1f = 1 attack per second ; 2f = 1 attack every 2 seconds ; ...
    private float penetrationFactor = 3f; // factor that recudes the amount of damage reduction the target recives through its defense stat
    

    [Header("Damage Calculation")]
    private float targetDefense = 0f;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void cooldownAttack()
    {
        Debug.Log("Im attacking this target");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "possibleTargets" || other.gameObject.tag == "Player")
        {
            cooldownAttack();
            targetDefense = other.GetComponent<LifeAndStats>().defense;
            float applyingDamage = damage - targetDefense/penetrationFactor;
            Debug.Log(applyingDamage);
            other.GetComponent<LifeAndStats>().health -= applyingDamage;
            Debug.Log(other.GetComponent<LifeAndStats>().health);
        }
     
    }

}
