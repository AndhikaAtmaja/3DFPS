using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public LayerMask hitMask;
    private Animator _animator;
    private bool _doneAnimation;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public override void PlayerAttack()
    {
        _animator.SetTrigger("attack");
        if (!_doneAnimation)
        {
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            Debug.DrawRay(ray.origin, ray.direction * weaponData.rangeMelee, Color.red, 0.2f);

            if (Physics.Raycast(ray, out RaycastHit hit, weaponData.rangeMelee, hitMask))
            {
                SoundManager.instance.Play2DSound("Sword");
                IDamageable dmg = hit.collider.GetComponent<IDamageable>();
                if (dmg != null)
                {
                    dmg.TakeDamage(weaponData.meleeDamage);
                }
                else
                {
                    Debug.Log("Sword hit something but no IDamageable");
                }
            }
            else
            {
                Debug.Log("Sword missed");
            }
        }
  
    }
}
