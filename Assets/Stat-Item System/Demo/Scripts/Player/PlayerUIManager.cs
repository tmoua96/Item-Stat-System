using ScriptableObjectArchitecture;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI playerHealthText;
    [SerializeField]
    private Image playerHealthImage;

    [SerializeField]
    private TextMeshProUGUI statusEffectsText;

    [SerializeField]
    private TextMeshProUGUI equipmentText;

    [SerializeField]
    private EquipmentEventDataGameEvent EquipmentChangedEvent;
    [SerializeField]
    private StatusEffectEventDataGameEvent StatusChangedEvent;
    [SerializeField]
    private StatEventDataGameEvent StatsChangedEvent;
    [SerializeField]
    private CharacterHealthDataGameEvent CharacterHealthChangedEvent;

    private StatusEffect[] statusEffects;

    private void Update()
    {
        UpdateStatus();
    }

    private void UpdateStatus()
    {
        if (statusEffects == null || statusEffects.Length == 0)
        {
            return;
        }

        string statusText = "";

        foreach (var status in statusEffects)
        {
            statusText += $"{status.Data.Name}: {(int)status.TimeRemaining}\n";
        }

        statusEffectsText.text = statusText;
    }

    private void OnEnable()
    {
        EquipmentChangedEvent?.AddListener(OnEquipmentChanged);
        StatusChangedEvent?.AddListener(OnStatusChanged);
        StatsChangedEvent?.AddListener(OnStatsChanged);
        CharacterHealthChangedEvent?.AddListener(OnHealthChanged);
    }

    private void OnEquipmentChanged(EquipmentEventData data)
    {
        string action = data.WasEquipmentAdded ? "Equipped" : "Unequipped";
        string message = $"{action} {data.ModifiedEquipment.Item.Name}";

        equipmentText.text = message;
    }

    private void OnStatusChanged(StatusEffectEventData data)
    {
        statusEffects = data.StatusEffects;

        if (statusEffects.Length == 0)
            statusEffectsText.text = "None";
    }

    private void OnStatsChanged(StatEventData data)
    {
        // TODO: Implement elements to display Stats UI
    }

    private void OnHealthChanged(CharacterHealthData data)
    {
        string healthText = $"{Mathf.Round(data.CurrentHealth)} / {Mathf.Round(data.MaxHealth)}";

        playerHealthImage.fillAmount = Mathf.Round(data.CurrentHealth) / Mathf.Round(data.MaxHealth);
        playerHealthText.text = healthText;
    }

    private void OnDisable()
    {
        EquipmentChangedEvent?.RemoveListener(OnEquipmentChanged);
        StatusChangedEvent?.RemoveListener(OnStatusChanged);
        StatsChangedEvent?.RemoveListener(OnStatsChanged);
        CharacterHealthChangedEvent?.RemoveListener(OnHealthChanged);
    }
}
