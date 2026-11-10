using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Game.Core;
using Game.Data;
using Game.Npc;
using System.Collections;
using Game.GameManagement;
using Game.SceneTransitions;

namespace Game.Environment
{
    public class GateExit : MonoBehaviour, IInteractable, ICoroutineRunner
    {
        [SerializeField] private GameObject _finalScreen;

        [SerializeField] private GameObject _player1;
        [SerializeField] private GameObject _player2;
        [SerializeField] private GameObject _globalInputs;

        [SerializeField] private SceneTransitionManager _sceneTransitionManager;

        [SerializeField] private SceneToLoadDataSo _sceneToLoad;

        [SerializeField] private DialogueDataSo _data;

        [SerializeField] private TMP_Text _textBox;

        [SerializeField] private CanvasGroup _textBoxCanvasGroup;

        private Dialogue _dialogue;

        private Coroutine _routine;

        private bool _canInteract;
        public bool CanInteract => _canInteract;

        public bool HasInteractorInRange { get; set; }

        private void Awake()
        {
            _dialogue = new Dialogue(_data, _textBox, GetComponentInChildren<AudioSource>(), this);
        }

        private void OnEnable()
        {
            _dialogue.OnDialogueFinished += Reset;
        }

        private void Start()
        {
            _canInteract = true;
        }

        private void OnDisable()
        {
            _dialogue.OnDialogueFinished -= Reset;
        }

        public void Interact(InputAction interactAction, Transform interactor, PlayerType playerType)
        {
            if(GameManager.KeyPicked && GameManager.FragmentPicked)
            {
                if (_routine != null) return;

                _player1.SetActive(false);
                _player2.SetActive(false);
                _globalInputs.SetActive(false);

                _finalScreen.SetActive(true);

                _routine = StartCoroutine(EndGameRoutine());
                return;
            }

            _canInteract = false;
            _textBoxCanvasGroup.alpha = 1;

            _dialogue.BeginDialogue(interactAction);
        }

        private void Reset()
        {
            _textBoxCanvasGroup.alpha = 0;
            _canInteract = true;
        }

        private IEnumerator EndGameRoutine()
        {
            yield return new WaitForSeconds(10);

            _sceneTransitionManager.ChangeScene(_sceneToLoad);
            
        }

        public Coroutine RunRoutine(IEnumerator routine)
        {
            return StartCoroutine(routine);
        }
    }

}
