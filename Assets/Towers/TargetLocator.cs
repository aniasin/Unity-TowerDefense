using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] float range = 15f;
    [SerializeField] ParticleSystem projectileParticles;
    Transform target;


    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(enemy.transform.position, transform.position);
            if (targetDistance < maxDistance)
            {
                maxDistance = targetDistance;
                closestTarget = enemy.transform;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
        if (!target) return;
        float targetDistance = Vector3.Distance(target.position, transform.position);

        transform.LookAt(target);

        Fire(targetDistance <= range);
    }

    void Fire(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;    
    }
}
