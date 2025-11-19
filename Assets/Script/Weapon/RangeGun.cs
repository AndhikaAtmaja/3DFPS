using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeGun : Weapon
{
    [Header("Config")]
    public GameObject bulletPrefab;
    public Transform bulletContainer;
    public Transform gunBarrel;
    public float bulletVelocity;


    public override void PlayerAttack()
    {
        if (bulletContainer == null || bulletPrefab == null)
        {
            Debug.LogWarning("bulletPrefab or bulletContainer is NULL!");
            return;
        }

        Vector3 shootingDir = CalculateDirection().normalized;

        //spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity, bulletContainer);
        bullet.transform.forward =shootingDir;

        SoundManager.instance.Play2DSound("Gun");

        //Shoot the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(shootingDir * bulletVelocity, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("RangeGun: Bullet prefab has NO rigidbody!");
        }
    }

    public Vector3 CalculateDirection()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(100);
        }

        Vector3 direction = targetPoint - gunBarrel.position;

        float x = Random.Range(-inaccuracy, inaccuracy);
        float y = Random.Range(-inaccuracy, inaccuracy);

        return direction + new Vector3(x, y, 0);
    }

    public override void EnemyAttack()
    {
        if (bulletContainer == null || bulletPrefab == null)
        {
            Debug.LogWarning("bulletPrefab or bulletContainer is NULL!");
            return;
        }

        //spawn the bullet
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, Quaternion.identity, bulletContainer);

        SoundManager.instance.Play2DSound("Gun");

        //Shoot the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Quaternion spreadRotation = Quaternion.Euler(
            Random.Range(-inaccuracy, inaccuracy),
            Random.Range(-inaccuracy, inaccuracy),
            0f
            );

            Vector3 inaccuracyDirection = spreadRotation * gunBarrel.forward;

            rb.AddForce(inaccuracyDirection * bulletVelocity, ForceMode.Impulse);
        }
        else
        {
            Debug.LogWarning("RangeGun: Bullet prefab has NO rigidbody!");
        }
    }
}
