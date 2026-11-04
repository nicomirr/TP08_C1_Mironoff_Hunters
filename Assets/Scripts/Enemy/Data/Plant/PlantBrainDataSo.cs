using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlantBrainDataSo", menuName = "Scriptable Objects/PlantBrainDataSo")]
    public class PlantBrainDataSo : EnemyBrainDataSo
    {
        [SerializeField] private PlantCombatControllerDataSo _combatData;
        public PlantCombatControllerDataSo CombatData => _combatData;
    }
}


