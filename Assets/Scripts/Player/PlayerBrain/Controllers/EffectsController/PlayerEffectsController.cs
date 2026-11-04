using UnityEngine;
using System.Collections.Generic;
using Game.Core;
using Game.Data;
using Game.Throwables;

namespace Game.Player
{
    public class PlayerEffectsController 
    {
        private readonly Dictionary<ItemType, ItemEffect> _itemEffects = new();

        private ICoroutineRunner _coroutineRunner;

        public PlayerEffectsController(PlayerEffectsControllerDataSo data, ICoroutineRunner coroutineRunner, PlayerHealthController healthController,
            PlayerCombatController combatController, Transform userTransform, Rigidbody2D userRb)
        {
            _coroutineRunner = coroutineRunner;

            HealEffect healEffect = new HealEffect(healthController, userTransform);
            InvulnerabilityEffect invulnerabilityEffect = new InvulnerabilityEffect(data.InvulnerabilityEffectData, healthController, userTransform);
            DamageEffect fireDamageEffect = new DamageEffect(data.FireDamageData, combatController, userTransform);
            DamageEffect poisonDamageEffect = new DamageEffect(data.PoisonDamageData, combatController, userTransform);
            ThrowableEffect hollyWaterEffect = new ThrowableEffect(data.HollyWaterData, userRb, userTransform);

            _itemEffects.Add(ItemType.HealingBalmItem, healEffect);
            _itemEffects.Add(ItemType.WardingCharm, invulnerabilityEffect);
            _itemEffects.Add(ItemType.Embers, fireDamageEffect);            
            _itemEffects.Add(ItemType.PosionPowder, poisonDamageEffect);
            _itemEffects.Add(ItemType.HollyWater, hollyWaterEffect);
        }

        public void HandleItemUsed(ItemType itemType)
        {
            _coroutineRunner.RunRoutine(_itemEffects[itemType].UseItemRoutine());
        }
    }
}

