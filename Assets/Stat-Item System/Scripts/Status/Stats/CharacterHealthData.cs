using UnityEngine;

[System.Serializable]
public struct CharacterHealthData
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float maxHealth;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;

    public CharacterHealthData(float currentHealth, float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
}
