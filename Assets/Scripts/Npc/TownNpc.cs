using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using TMPro;
using Game.Core;
using Game.Data;
using Game.Marker;

namespace Game.Npc
{
    public class TownNpc : MonoBehaviour, IInteractable, ICoroutineRunner
    {
        [SerializeField] private TownNpcDataSo _data;

        [SerializeField] private TMP_Text _npcTextBox;

        [SerializeField] private CanvasGroup _textBoxCanvasGroup;

        private Dialogue _dialogue;

        private Rigidbody2D _rb;

        private Animator _animator;

        private float _currentSpeed;

        private bool _limitReached;
        private bool _isAlive;

        private Coroutine _walkRoutine;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _animator = GetComponentInChildren<Animator>();

            _dialogue = new Dialogue(_data.DialogueData, _npcTextBox, GetComponentInChildren<AudioSource>(), this);
        }

        private void OnEnable()
        {
            _dialogue.OnDialogueFinished += ResetNpc;
        }

        private void Start()
        {           
            _isAlive = true;
            _canInteract = true;

            _walkRoutine = StartCoroutine(WalkRoutine());
        }

        private void FixedUpdate()
        {
            if(_dialogue.DialogueDisplaying)
            {
                _animator.SetBool(_data.WalkingAnimationBoolHash, false);
                _rb.linearVelocityX = 0;
                return;
            }

            _rb.linearVelocityX = this.transform.right.x * _currentSpeed;
        }

        private void LateUpdate()
        {
            _npcTextBox.transform.rotation = Quaternion.identity;
        }

        private void OnDisable()
        {
            _dialogue.OnDialogueFinished -= ResetNpc;
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

        public void Interact(InputAction interactAction)
        {            
            _canInteract = false;
            
            StopCoroutine(_walkRoutine);
            _walkRoutine = null;

            _textBoxCanvasGroup.alpha = 1;
            _dialogue.BeginDialogue(interactAction);
        }

        private void ResetNpc()
        {
            _textBoxCanvasGroup.alpha = 0;

            _canInteract = true;
            _walkRoutine = StartCoroutine(WalkRoutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out NpcLimitMarker _)) return;
                       
            _limitReached = true;
        }

        public Coroutine RunRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
