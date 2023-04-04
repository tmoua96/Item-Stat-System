public interface IHealable
{
    /// <summary>
    /// Heal HP based off of heal data.
    /// </summary>
    /// <param name="data">Info about heal data.</param>
    void HealDamage(HealData data);
}
