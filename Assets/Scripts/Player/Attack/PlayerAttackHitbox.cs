using UnityEngine;

namespace Game.Player
{
    public class PlayerAttackHitbox : MonoBehaviour
    {
        [SerializeField] private Collider2D _hitbox;

        public void EnableHitbox()
        {
            _hitbox.enabled = true;
        }

        public void DisableHitbox()
        {
            _hitbox.enabled = false;
        }
    }

}
