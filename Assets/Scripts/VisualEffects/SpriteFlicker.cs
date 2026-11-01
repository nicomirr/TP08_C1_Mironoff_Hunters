using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.VisualEffects
{
    public class SpriteFlicker
    {
        private readonly SpriteFlickerDataSo _data;

        private readonly SpriteRenderer _spriteRenderer;
        private readonly Color32 _playerOriginalColor;

        public SpriteFlicker(SpriteFlickerDataSo data, SpriteRenderer spriteRenderer)
        {
            _data = data;
            _spriteRenderer = spriteRenderer;

            _playerOriginalColor = _spriteRenderer.color;
        }

        public IEnumerator FlickerRoutine()
        {
            int totalChanges = _data.TotalBlinks * 2;
            float flickInterval = _data.FlickerTime / totalChanges;

            for (int i = 0; i < totalChanges; i++)
            {
                if (_spriteRenderer.color == _playerOriginalColor)
                    _spriteRenderer.color = _data.FlickerColor;
                else
                    _spriteRenderer.color = _playerOriginalColor;

                yield return new WaitForSeconds(flickInterval);
            }

            _spriteRenderer.color = _playerOriginalColor;

        }
    }

}
