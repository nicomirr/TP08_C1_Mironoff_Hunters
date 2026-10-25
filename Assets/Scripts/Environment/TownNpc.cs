using UnityEngine;
using System.Collections;
using Game.Marker;
using Game.Data;

namespace Game.Environment
{
    public class TownNpc : MonoBehaviour
    {
        [SerializeField] private TownNpcDataSo _data;

        private Rigidbody2D _rb;

        private Animator _animator;

        private float _currentSpeed;

        private bool _limitReached;
        private bool _isAlive;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();

            _isAlive = true;

            StartCoroutine(WalkRoutine());
        }

        private void FixedUpdate()
        {
            _rb.linearVelocityX = this.transform.right.x * _currentSpeed;
        }

        private IEnumerator WalkRoutine()
        {
            while(_isAlive)
            {
                _currentSpeed = 0;

                _animator.SetBool(_data.WalkingAnimationBoolHash, false);

                int randomWaitTime = Random.Range(_data.MinIdleTime, _data.MaxIdleTime);

                yield return new WaitForSeconds(randomWaitTime);

                this.transform.right = this.transform.right == Vector3.right? 
                    Vector3.left : Vector3.right;

                _animator.SetBool(_data.WalkingAnimationBoolHash, true);

                _limitReached = false;

                _currentSpeed = _data.MovementSpeed;

                yield return new WaitUntil(() => _limitReached);
            }
        }        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out NpcLimitMarker _)) return;
                       
            _limitReached = true;
        }

    }
}
