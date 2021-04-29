public class Cargo_Bottles : AbstractCargo
{
    public override void ApplyEffect()
    {
    }
    
    public override void SpecifyDamage(float damagePercentage, DamageType damageType)
    {
        damagePercentage *= 2;
        base.SpecifyDamage(damagePercentage, damageType);
    }
}