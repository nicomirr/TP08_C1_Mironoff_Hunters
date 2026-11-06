using UnityEngine;
using System.Collections;

namespace Game.Thorns
{
    public class ThornMaskController : MonoBehaviour
    {
        [SerializeField] private Transform _mask;
        [SerializeField] private Vector3 _hiddenPosition;
        [SerializeField] private Vector3 _visiblePosition;

        private Collider2D _collider;

        private float _duration;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void Start()
        {
            _collider.enabled = false;
            _mask.localPosition = _hiddenPosition;
        }

        public void Initialize(float duration)
        {
            _duration = duration;
        }

        public IEnumerator RevealRoutine()
        {
            float elapsed = 0f;

            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;
                float time = Mathf.Clamp01(elapsed / _duration);

                _mask.localPosition = Vector3.Lerp(_hiddenPosition, _visiblePosition, time);

                yield return null;
            }

            _mask.localPosition = _visiblePosition;
            _collider.enabled = true;
        }

        public IEnumerator HideRoutine()
        {
            float elapsed = 0f;

            while (elapsed < _duration)
            {
                elapsed += Time.deltaTime;
                float time = Mathf.Clamp01(elapsed / _duration);

                _mask.localPosition = Vector3.Lerp(_visiblePosition, _hiddenPosition, time);

                yield return null;
            }

            _mask.localPosition = _hiddenPosition;
            _collider.enabled = false;
        }
    }

}
