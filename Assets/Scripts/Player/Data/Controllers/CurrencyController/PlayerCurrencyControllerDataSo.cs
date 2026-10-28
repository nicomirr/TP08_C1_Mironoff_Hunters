using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerCurrencyControllerDataSo", menuName = "Scriptable Objects/PlayerCurrencyControllerDataSo")]
    public class PlayerCurrencyControllerDataSo : ScriptableObject
    {
        [SerializeField] private int _initialCoins;
        public int InitialCoins => _initialCoins;
    }

}

