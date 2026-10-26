using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using Game.Core;
using TMPro;

namespace Game.Npc
{
    public class TownNpc : MonoBehaviour, IInteractable, ICoroutineRunner
    {
        [SerializeField] protected TownNpcDataSo _data;

        [SerializeField] protected TMP_Text _npcTextBox;

        [SerializeField] protected CanvasGroup _textBoxCanvasGroup;

        protected Dialogue _dialogue;               

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        protected virtual void Awake()
        {           
            _dialogue = new Dialogue(_data.DialogueData, _npcTextBox, GetComponentInChildren<AudioSource>(), this);
        }

        private void OnEnable()
        {
            _dialogue.OnDialogueFinished += ResetNpc;
        }

        protected virtual void Start()
        {
            _canInteract = true;
        }

        private void LateUpdate()
        {
            _npcTextBox.transform.rotation = Quaternion.identity;
        }

        private void OnDisable()
        {
            _dialogue.OnDialogueFinished -= ResetNpc;
        }

        public virtual void Interact(InputAction interactAction)
        {
            _canInteract = false;
            _textBoxCanvasGroup.alpha = 1;

            _dialogue.BeginDialogue(interactAction);
        }

        protected virtual void ResetNpc()
        {
            _textBoxCanvasGroup.alpha = 0;
            _canInteract = true;
        }

        public Coroutine RunRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }
}
