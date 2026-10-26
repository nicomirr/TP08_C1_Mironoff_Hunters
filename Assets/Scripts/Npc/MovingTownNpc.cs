using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Game.Data;
using Game.Marker;

namespace Game.Npc
{
    public class MovingTownNpc : TownNpc
    {
        private MovingTownNpcDataSo _movingData;

        private Rigidbody2D _rb;

        private Animator _animator;

        private float _currentSpeed;

        private bool _limitReached;
        private bool _isAlive;

        private Coroutine _walkRoutine;


        protected override void Awake()
        {
            base.Awake();

            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();
        }


        protected override void Start()
        {         
            base.Start();

            _movingData = (MovingTownNpcDataSo)_data; 
            _isAlive = true;
            _walkRoutine = StartCoroutine(WalkRoutine());
        }

        private void FixedUpdate()
        {
            if(_dialogue.DialogueDisplaying)
            {
                _animator.SetBool(_movingData.WalkingAnimationBoolHash, false);
                _rb.linearVelocityX = 0;
                return;
            }

            _rb.linearVelocityX = this.transform.right.x * _currentSpeed;
        }              

        private IEnumerator WalkRoutine()
        {
            while(_isAlive)
            {
                _currentSpeed = 0;

                _animator.SetBool(_movingData.WalkingAnimationBoolHash, false);

                int randomWaitTime = Random.Range(_movingData.MinIdleTime, _movingData.MaxIdleTime);

                yield return new WaitForSeconds(randomWaitTime);

                this.transform.right = this.transform.right == Vector3.right? 
                    Vector3.left : Vector3.right;

                _animator.SetBool(_movingData.WalkingAnimationBoolHash, true);

                _limitReached = false;

                _currentSpeed = _movingData.MovementSpeed;

                yield return new WaitUntil(() => _limitReached);
            }
        }

        public override void Interact(InputAction interactAction)
        {            
            base.Interact(interactAction);

            StopCoroutine(_walkRoutine);
            _walkRoutine = null;
        }

        protected override void ResetNpc()
        {
            base.ResetNpc();
            _walkRoutine = StartCoroutine(WalkRoutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out NpcLimitMarker _)) return;
                       
            _limitReached = true;
        }        
    }
}
