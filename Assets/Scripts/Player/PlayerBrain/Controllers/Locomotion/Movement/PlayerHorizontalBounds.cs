using UnityEngine;
using Game.Data;

namespace Game.Player
{
    public class PlayerHorizontalBounds
    {
        private readonly PlayerHorizontalBoundsDataSo _data;

        private readonly Camera _camera;

        public PlayerHorizontalBounds(PlayerHorizontalBoundsDataSo data)
        {
            _data = data;
            _camera = Camera.main;
        }

        public Vector2 GetHorizontalBounds()
        {
            float minX = _camera.ViewportToWorldPoint(new Vector3(_data.MinXViewportPoint, 0f)).x;

            float maxX = _camera.ViewportToWorldPoint(new Vector3(_data.MaxXViewportPoint, 0f)).x;

            return new Vector2(minX, maxX);
        }
    }

}
