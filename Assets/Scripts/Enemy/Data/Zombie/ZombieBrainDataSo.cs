using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "ZombieBrainDataSo", menuName = "Scriptable Objects/ZombieBrainDataSo")]
    public class ZombieBrainDataSo : EnemyBrainDataSo
    {      
        [SerializeField] private ZombieLocomotionControllerDataSo _locomotionData;
        public ZombieLocomotionControllerDataSo LocomotionData => _locomotionData;

        [SerializeField] private ZombieCombatControllerDataSo _combatData;
        public ZombieCombatControllerDataSo CombatData => _combatData;

        [SerializeField] private ZombieBurrowControllerDataSo _burrowData;
        public ZombieBurrowControllerDataSo BurrowData => _burrowData;
    }
}
