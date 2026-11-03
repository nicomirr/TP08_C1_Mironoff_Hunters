using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Player
{
    public class PlayerEffectsController 
    {
        private readonly Dictionary<ItemType, ItemEffect> _itemEffects = new();

        private ICoroutineRunner _coroutineRunner;
        
        public PlayerEffectsController(ICoroutineRunner coroutineRunner, PlayerHealthController healthController, PlayerCombatController combatController, Transform userTransform)
        {
            _coroutineRunner = coroutineRunner;

            HealEffect healEffect = new HealEffect(healthController, userTransform);
            InvulnerabilityEffect invulnerabilityEffect = new InvulnerabilityEffect(healthController, userTransform, 7);
            FireDamageEffect fireDamageEffect = new FireDamageEffect(combatController, userTransform, 10, 1.65f);
            PoisonDamageEffect poisonDamageEffect = new PoisonDamageEffect(combatController, userTransform, 10, 1.65f);

            _itemEffects.Add(ItemType.HealingBalmItem, healEffect);
            _itemEffects.Add(ItemType.WardingCharm, invulnerabilityEffect);
            _itemEffects.Add(ItemType.Embers, fireDamageEffect);            
            _itemEffects.Add(ItemType.PosionPowder, poisonDamageEffect);            
        }

        public void HandleItemUsed(ItemType itemType)
        {
            _coroutineRunner.RunRoutine(_itemEffects[itemType].UseItemRoutine());
        }
    }
}

