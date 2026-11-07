using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "PlayerMovementDataSo", menuName = "Scriptable Objects/PlayerMovementDataSo")]
    public class PlayerMovementDataSo : ScriptableObject
    {
        [SerializeField] private float _regularMovementSpeed;
        public float RegularMovementSpeed => _regularMovementSpeed;

        [SerializeField] private float _mudMovementSpeed;
        public float MudMovementSpeed => _mudMovementSpeed;
    }
}

