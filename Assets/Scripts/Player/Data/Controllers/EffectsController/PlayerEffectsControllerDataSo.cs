using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerEffectsControllerDataSo", menuName = "Scriptable Objects/PlayerEffectsControllerDataSo")]
    public class PlayerEffectsControllerDataSo : ScriptableObject
    {
        [SerializeField] private InvulnerabilityEffectDataSo _invulnerabilityData;
        public InvulnerabilityEffectDataSo InvulnerabilityEffectData => _invulnerabilityData;

        [SerializeField] private DamageEffectDataSo _fireDamageData;
        public DamageEffectDataSo FireDamageData => _fireDamageData;

        [SerializeField] private DamageEffectDataSo _poisonDamageData;
        public DamageEffectDataSo PoisonDamageData => _poisonDamageData;

        [SerializeField] private ThrowableEffectDataSo _hollyWaterData;
        public ThrowableEffectDataSo HollyWaterData => _hollyWaterData;
    }

}

