namespace Game.Core
{
    public interface IDamageProvider
    {
        public float Damage { get; }
        public DamageType DamageType { get; }
    }

}
