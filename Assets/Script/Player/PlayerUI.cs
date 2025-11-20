using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;

    [Header("Settings Animation")]
    [SerializeField] private float lerpTimer;
    [SerializeField] private float chipSpeed;

    [Header("Health UI")]
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI valueHealthBar;

    [Header("Ammo UI")]
    public TextMeshProUGUI ammo;

    [Header("Score UI")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;

    [Header("Abillity UI")]
    public TextMeshProUGUI skillText;
    public TextMeshProUGUI ultimateText;

    [Header("Events")]
    public OnUltimateActivedEventSO onUltimateActivedEvent;
    public OnUltimateDeactiveEventSO onUltimateDeactiveEvent;
    public OnUpdateScoreEventSO onUpdateScoreEvent;
    public OnStartUIPlayerEventSO onStartUIPlayerEvent;

    private void Update()
    {
        if (CoolDownManager.instance.GetCoolDownSkill() > 0)
        {
            skillText.text = $"Skill : {CoolDownManager.instance.GetCoolDownSkill().ToString("F2")}";
        }
        else
            skillText.text = $"Skill : READY!";


        if (CoolDownManager.instance.GetCoolDownUltimate() > 0)
        {
            ultimateText.text = $"Ultimate : {CoolDownManager.instance.GetCoolDownUltimate().ToString("F2")}";
        }
        else
            ultimateText.text = $"Ultimate : READY!";
    }

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    public void IntiliazeScoreUI(int bestScore)
    {
        scoreText.text = $"score : {0}";
        bestScoreText.text = $"bestScore : {bestScore.ToString()}";
    }

    private void UpdateScoreUI(int scoreValue)
    {
        scoreText.text = $"score : {scoreValue.ToString()}";
    }

    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        if (currentAmmo == 0 && maxAmmo == 0)
        {
            ammo.text = string.Empty;
        }
        else
        {
            ammo.text = $"{currentAmmo} / {maxAmmo}";
        }
    }

    public void UpdateHealthBarUI(float healthPoint, float maxHealthPoint)
    {
        valueHealthBar.text = healthPoint.ToString() + " / " + maxHealthPoint.ToString();
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = healthPoint / maxHealthPoint;

        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        else if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        else
        {
            lerpTimer = 0;
        }
    }

    void UpgradeHealthUI()
    {
        Debug.Log("Resize to 200HP!");
    }

    private void ResetHealthUI()
    {
        Debug.Log("Resize to 100HP!");
    }

    public void ResetLerptime()
    {
        lerpTimer = 0;
    }

    private void OnEnable()
    {
        onUltimateActivedEvent.OnUltimateActivedEvent   += UpgradeHealthUI;
        onUltimateDeactiveEvent.OnUltimateDeactiveEvent += ResetHealthUI;
        onUpdateScoreEvent.OnUpdateScoreEvent += UpdateScoreUI;
    }

    private void OnDisable()
    {
        onUltimateActivedEvent.OnUltimateActivedEvent   -= UpgradeHealthUI;
        onUltimateDeactiveEvent.OnUltimateDeactiveEvent -= ResetHealthUI;
        onUpdateScoreEvent.OnUpdateScoreEvent -= UpdateScoreUI;
    }
}
