using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTower : MonoBehaviour
{
    public GameObject closestEnemy = null;
    private GameObject[] gos;
    private GameObject shooter;
    private GameObject target;
    public GameObject bulletPrefab;
    public float shotSpeed = 2f;
    public float attackRange = 20f;
    //locations
    public Vector3 interceptPoint;
    private Vector3 shooterPosition;
    private Vector3 targetPosition;
    //velocities
    private Vector3 shooterVelocity;
    private Vector3 targetVelocity;
    // Start is called before the first frame update
    void Start()
    {
        FindClosestTarget();
        target = closestEnemy;
        shooter = gameObject;
        shooterPosition = shooter.transform.position;
        targetPosition = target.transform.position;
        shooterVelocity = shooter.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;
        targetVelocity = target.GetComponent<Rigidbody>() ? target.GetComponent<Rigidbody>().velocity : Vector3.zero;
        StartCoroutine(startAiming());
        
    }
   

// Update is called once per frame
void Update()
    {
        
    }

    private void aimAtTarget()
    {
        FindClosestTarget();
        target = closestEnemy;
        if(target != null)
        {
            shooterPosition = shooter.transform.position;
            targetPosition = target.transform.position;
            shooterVelocity = shooter.GetComponent<Rigidbody>() ? shooter.GetComponent<Rigidbody>().velocity : Vector3.zero;
            targetVelocity = target.GetComponent<BasicEnemy>().agent.velocity;
            interceptPoint = FirstOrderIntercept
            (
                shooterPosition,
                shooterVelocity,
                shotSpeed,
                targetPosition,
                targetVelocity
            );


            Debug.Log(shooterVelocity);
            Debug.Log(targetVelocity);
            shootProjectile();

            //Debug.Log(interceptPoint);
        }


    }

    IEnumerator startAiming()
    {
        yield return new WaitForSeconds(3);
        aimAtTarget();
    }

    private void shootProjectile()
    {
        Vector3 spawnPoint = gameObject.transform.position;
        Vector3 targetPoint = interceptPoint;
        Vector3 toTarget = targetPoint - spawnPoint;
        if(Vector3.Distance(spawnPoint, targetPoint) <= attackRange)
        {
            bulletPrefab.GetComponent<projectileVelocity>().speed = shotSpeed;
            Instantiate(bulletPrefab, spawnPoint, Quaternion.LookRotation(toTarget));
            StartCoroutine(shootCd());
        }
        else
        {
            Debug.Log("Turret: Target not in range");
            StartCoroutine(shootCd());
        }
       
    }

    IEnumerator shootCd()
    {
        yield return new WaitForSeconds(2);
        aimAtTarget();
    }


    public static Vector3 FirstOrderIntercept
        (
            Vector3 shooterPosition,
            Vector3 shooterVelocity,
            float shotSpeed,
            Vector3 targetPosition,
            Vector3 targetVelocity
        )

    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }

    public void FindClosestTarget()
    {
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closestEnemy = go;
                distance = curDistance;
            }
        }
    }

    
}
