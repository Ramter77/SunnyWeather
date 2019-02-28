using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeAndStats : MonoBehaviour
{
    public float health = 100f;
    public float defense = 20f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.CompareTag("possibleTargets") && health <= 0)
        {
            //Debug.Log("yeah im dead");
            gameObject.tag = "destroyedTarget";
        }
    }
}
