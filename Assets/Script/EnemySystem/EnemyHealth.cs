using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Character
{
    private CharacterDataSO _characterData;
    private EnemyUI _enemyUI;

    [Header("Events")]
    public OnEnemyDeathEventSO OnEnemyDeath;

    private void Start()
    {
        _characterData = GetComponent<EnemyData>().CharacterData;
        _healthPoint = _maxHealthPoint = _characterData.healthCharacter;
        _enemyUI = GetComponent<EnemyUI>();
    }

    private void Update()
    {
        _healthPoint = Mathf.Clamp(_healthPoint, 0, _maxHealthPoint);
        _enemyUI.UpdateHealthUI(_healthPoint, _maxHealthPoint);
    }

    public override void TakeDamage(float damage)
    {
        _healthPoint -= damage;
    }

    public override void RestoreHealth(float healAmount)
    {
        _healthPoint += healAmount;
    }

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }
}
