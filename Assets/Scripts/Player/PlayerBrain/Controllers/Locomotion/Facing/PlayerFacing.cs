using UnityEngine;

namespace Game.Player
{
    public class PlayerFacing
    {
        //Esto se va a usar para la direccion de los objetos lanzados
        //Evaluar si se hace con esto o si se consigue de otra forma la direccion
        public bool IsFacingRight => _isFacingRight;
        private bool _isFacingRight;

        private Transform _ownerTransform;

        public PlayerFacing(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
            _isFacingRight = _ownerTransform.right == Vector3.right ? true : false;
        }

        public void Flip(float direction)
        {
            if (direction == 0) return;

            _isFacingRight = direction > 0;

            _ownerTransform.right = _isFacingRight ? Vector3.right : Vector3.left;
        }
    }
}

