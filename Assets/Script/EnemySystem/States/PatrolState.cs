using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    public int WaypointIndex;
    public float stopTimer;
    public float shotTimer;

    public override void Enter()
    {
        stopTimer = 0;
        shotTimer = 0;
    }

    public override void Perform()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit()
    {

    }

    public void PatrolCycle()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            stopTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;

            if (stopTimer > 3f)
            {
                if (WaypointIndex < enemy.path.waypoints.Count - 1)
                {
                    WaypointIndex++;
                }
                else
                {
                    WaypointIndex = 0;
                }
                enemy.Agent.SetDestination(enemy.path.waypoints[WaypointIndex].position);
                stopTimer = 0;
            }
        }
    }

    public void ShootLastKnownPosition()
    {
        enemy.transform.LookAt(enemy.lastKnowPos);
        Debug.Log("Shoot to lastKnowPos coordinate :" + enemy.lastKnowPos);
        if (enemy.currentAmmo > 0)
        {
            enemy.enemyWeapon.EnemyAttack();
            enemy.currentAmmo--;
            shotTimer = 0;
        }
        else
        {
            enemy.ReloadWeapon();
        }
    }
}
