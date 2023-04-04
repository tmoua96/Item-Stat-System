public interface IDamageable
{
    /// <summary>
    /// Take damage based off of damage data.
    /// </summary>
    /// <param name="data">Info about damage data.</param>
    void TakeDamage(DamageData data);
}