using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    private float _moveTimer;
    private float _losePlayerTimer;
    private float _shotTimer;

    public override void Enter()
    {
  
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            _losePlayerTimer = 0; 
            _moveTimer += Time.deltaTime;
            _shotTimer += Time.deltaTime;

            Vector3 dir = (enemy.player.transform.position - enemy.transform.position).normalized;
            Quaternion lookRot = Quaternion.LookRotation(dir);
            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRot, Time.deltaTime * 5f);


            if (_shotTimer > enemy.fireRate)
            {
                Shoot();
                _shotTimer = 0;
            }

            if (_moveTimer > Random.Range(3, 7))
            {
                enemy.enemyAnimator.PlayAttackAnimation();
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                _moveTimer = 0;
            }
            enemy.lastKnowPos = enemy.player.transform.position;
            enemy.hasLastKnowPos = true;
        }
        else // lost sight of player
        {
            enemy.enemyAnimator.PlayIdleAnimation();
            _losePlayerTimer += Time.deltaTime;
            if (_losePlayerTimer > 0)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    void Shoot()
    {
        if (enemy.currentAmmo > 0)
        {
            enemy.enemyAnimator.PlayAttackAnimation();
            enemy.enemyWeapon.EnemyAttack();
            enemy.currentAmmo--;
        }
        else
        {
            enemy.ReloadWeapon();
        }
    }
}
