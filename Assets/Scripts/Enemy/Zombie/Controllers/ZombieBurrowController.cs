using UnityEngine;
using System.Collections;
using Game.Data;

namespace Game.Enemy
{
    public class ZombieBurrowController
    {
        private readonly ZombieBurrowControllerDataSo _controllerData;
        private readonly BurrowLimitsDataSo _limitsData;

        private readonly Rigidbody2D _rb;
        private readonly Collider2D _bodyCollider;
        private readonly Transform _ownerTransform;

        private bool _isBurrowed;
        public bool IsBurrowed => _isBurrowed;

        private bool _isBurrowing;
        public bool IsBurrowing => _isBurrowing;

        private bool _isUnburrowing;
        public bool IsUnburrowing => _isUnburrowing;

        private bool _canBurrow;
        public bool CanBurrow => _canBurrow;

        private float _burrowTimer;
        private float _burrowCooldown;
        private bool _burrowCooldownCalculated;

        private bool _canUnburrow;
        public bool CanUnburrow => _canUnburrow;

        private float _reenableUnburrowTimer;
        private float _unburrowCooldown;
        private bool _unburrowCooldownCalculated;

        private float _offsetX;
        private bool _hasOffset;

        public ZombieBurrowController(ZombieBurrowControllerDataSo controllersData, BurrowLimitsDataSo limitsData, Rigidbody2D rb, Collider2D bodyCollider, Transform ownerTransform)
        {
            _controllerData = controllersData;
            _limitsData = limitsData;   

            _rb = rb;

            _bodyCollider = bodyCollider;

            _ownerTransform = ownerTransform;

            _isBurrowed = true;

            _canUnburrow = true;
        }

        public void UpdateBurrowTimer()
        {
            if (_isBurrowed || _canBurrow) return;

            if(!_burrowCooldownCalculated)
            {
                _burrowCooldown = Random.Range(_controllerData.MinBurrowCooldown, _controllerData.MaxBurrowCooldown);
                _burrowCooldownCalculated = true;
            }

            _burrowTimer += Time.deltaTime;

            if(_burrowTimer >= _burrowCooldown)
            {
                _canBurrow = true;
                _burrowCooldownCalculated = false;
                _burrowTimer = 0;
            }
        }

        public void UpdateUnburrowTimer()
        {
            if (_canUnburrow || !_isBurrowed) return;

            if(!_unburrowCooldownCalculated)
            {
                _unburrowCooldown = Random.Range(_controllerData.MinUnburrowCooldown, _controllerData.MaxUnburrowCooldown);
                _unburrowCooldownCalculated = true;
            }

            _reenableUnburrowTimer += Time.deltaTime;

            if(_reenableUnburrowTimer >= _unburrowCooldown)
            {
                _canUnburrow = true;
                _reenableUnburrowTimer = 0;
                _unburrowCooldownCalculated = false;
            }
        }

        public void FollowTarget(Transform target)
        {
            if (!_isBurrowed || _isBurrowing || _isUnburrowing)
                return;

            if (!_hasOffset)
            {
                float distance = Random.Range(_controllerData.MinOffsetDistance, _controllerData.MaxOffsetDistance);
                float direction = Random.value < 0.5f ? -1f : 1f;

                _offsetX = distance * direction;
                _hasOffset = true;
            }

            Vector3 position = _ownerTransform.position;

            float targetX = target.position.x + _offsetX;
            position.x = Mathf.Clamp(targetX, _limitsData.MinX, _limitsData.MaxX);

            _ownerTransform.position = position;
        }

        public IEnumerator UnburrowRoutine()
        {
            _isUnburrowing = true;
            _hasOffset = false;

            Vector3 startPosition = _ownerTransform.position;
            Vector3 endPosition = startPosition + Vector3.up * _controllerData.Distance;

            float elapsed = 0f;

            while (elapsed < _controllerData.Duration)
            {
                elapsed += Time.deltaTime;

                float time = Mathf.Clamp01(elapsed / _controllerData.Duration);

                _ownerTransform.position = Vector3.Lerp(startPosition, endPosition, time);

                yield return null;
            }

            _ownerTransform.position = endPosition;

            _rb.simulated = true;
            _bodyCollider.enabled = true;

            _isBurrowed = false;
            _isUnburrowing = false;

            _canUnburrow = false;
        }

        public IEnumerator BurrowRoutine()
        {
            _isBurrowing = true;
            _canBurrow = false;

            _rb.simulated = false;
            _bodyCollider.enabled = false;

            Vector3 startPosition = _ownerTransform.position;
            Vector3 endPosition = startPosition + Vector3.down * _controllerData.Distance;

            float elapsed = 0f;

            while (elapsed < _controllerData.Duration)
            {
                elapsed += Time.deltaTime;

                float time = Mathf.Clamp01(elapsed / _controllerData.Duration);

                _ownerTransform.position = Vector3.Lerp(startPosition, endPosition, time);

                yield return null;
            }

            _ownerTransform.position = endPosition;

            _isBurrowing = false;
            _isBurrowed = true;
        }

        public void ForceBurrow()
        {
            if (_isBurrowed || _isBurrowing || _isUnburrowing) return;

            _burrowTimer = 0f;
            _burrowCooldown = 0f;
            _burrowCooldownCalculated = false;

            _canBurrow = true;
        }

        public void Reset()
        {
            _rb.linearVelocity = Vector2.zero;

            _rb.simulated = false;
            _bodyCollider.enabled = false;

            _canBurrow = false;
            _isBurrowing = false;
            _isUnburrowing = false;

            _hasOffset = false;
            _burrowCooldownCalculated = false;
            _unburrowCooldownCalculated = false;

            _burrowTimer = 0;
            _reenableUnburrowTimer = 0;

            _isBurrowed = true;
            _canUnburrow = true;
        }
    }
}
