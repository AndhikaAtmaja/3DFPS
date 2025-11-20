using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Character
{
    private CharacterDataSO _characterData;
    private PlayerUI _playerUI;

    [Header("Damage Overlay")]
    public Image damageOverLay;
    public float damageDuration;
    public float damageFadeSpeed;

    [Header("Events")]
    public OnUltimateActivedEventSO onUltimateActivedEvent;
    public OnUltimateDeactiveEventSO onUltimateDeactiveEvent;
    public OnPlayerDeathEventSO onPlayerDeathEvent;

    private float _durationTimer;

    private void Start()
    {
        _characterData = GetComponent<PlayerData>().CharacterData;
        _healthPoint = _maxHealthPoint = _characterData.healthCharacter;
        _playerUI = GetComponent<PlayerUI>();
        damageOverLay.color = new Color(damageOverLay.color.r, damageOverLay.color.g, damageOverLay.color.b, 0);
    }

    private void Update()
    {
        _healthPoint = Mathf.Clamp(_healthPoint, 0, _maxHealthPoint);
        _playerUI.UpdateHealthBarUI(_healthPoint, _maxHealthPoint);

        if (damageOverLay.color.a > 0)
        {
            if (_healthPoint < 30)
            {
                return;
            }

            _durationTimer += Time.deltaTime;
            if (_durationTimer > damageDuration)
            {
                float tempAlpha = damageOverLay.color.a;
                tempAlpha -= Time.deltaTime * damageFadeSpeed;
                damageOverLay.color = new Color(damageOverLay.color.r, damageOverLay.color.g, damageOverLay.color.b, tempAlpha);
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        _healthPoint -= damage;
        _durationTimer = 0;
        damageOverLay.color = new Color(damageOverLay.color.r, damageOverLay.color.g, damageOverLay.color.b, 1);
        _playerUI.ResetLerptime();

        if (_healthPoint <= 0)
        {
            onPlayerDeathEvent.OnPlayerDeath();
            SoundManager.instance.Play2DSound("Death");
            SaveSystem.Save();
        }

    }

    public override void RestoreHealth(float healAmount)
    {
        _healthPoint += healAmount;
        _playerUI.ResetLerptime();
    }

    void UpgradeHealth()
    {
        _healthPoint = _maxHealthPoint = 200;
    }

    void ResetHealth()
    {
        _maxHealthPoint = 100;

        // Clamp current health so it doesn't exceed new max
        _healthPoint = Mathf.Min(_healthPoint, _maxHealthPoint);
    }

    private void OnEnable()
    {
        onUltimateActivedEvent.OnUltimateActivedEvent   += UpgradeHealth;
        onUltimateDeactiveEvent.OnUltimateDeactiveEvent += ResetHealth;
    }

    private void OnDisable()
    {
        onUltimateActivedEvent.OnUltimateActivedEvent   -= UpgradeHealth;
        onUltimateDeactiveEvent.OnUltimateDeactiveEvent -= ResetHealth;
    }
}
