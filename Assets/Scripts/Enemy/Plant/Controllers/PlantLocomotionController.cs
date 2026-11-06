using UnityEngine;

namespace Game.Enemy
{
    public class PlantLocomotionController
    {
        private readonly Transform _ownerTransform;

        public PlantLocomotionController(Transform ownerTransform)
        {
            _ownerTransform = ownerTransform;
        }

        public void FaceTarget(Transform target)
        {
            if (target == null) return;

            _ownerTransform.right = target.position.x < _ownerTransform.position.x ? Vector2.left :
                Vector2.right;
        }
    }

}
