using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = ("Events/On Enemy Death Event"))]
public class OnEnemyDeathEventSO : ScriptableObject
{
    public UnityAction OnEnemyDeath;

    public void EnemyDeath() => OnEnemyDeath?.Invoke();
}
