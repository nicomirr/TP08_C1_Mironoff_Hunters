using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using TMPro;
using Game.Data;
using Game.Core;

namespace Game.Npc
{
    public class Dialogue 
    {
        private readonly DialogueDataSo _data;

        private readonly TMP_Text _textBox;
        private readonly AudioSource _audioSource;

        private readonly ICoroutineRunner _coroutineRunner;

        public event Action OnDialogueFinished;

        private bool _dialogueDisplaying;
        public bool DialogueDisplaying => _dialogueDisplaying;

        private bool _skipRequested;

        public Dialogue(DialogueDataSo data, TMP_Text textBox, AudioSource audioSource, ICoroutineRunner coroutineRunner)
        {
            _data = data;

            _textBox = textBox;
            _audioSource = audioSource;
            _coroutineRunner = coroutineRunner;
        }

        public void BeginDialogue(InputAction interactAction)
        {
            _coroutineRunner.RunRoutine(DialogueRoutine(interactAction));
        }

        private IEnumerator DialogueRoutine(InputAction interactAction)
        {
            _dialogueDisplaying = true;
            _skipRequested = false;

            interactAction.performed += OnInteract;

            foreach (string dialogue in _data.Dialogue)
            {
                _textBox.text = dialogue;
                _textBox.ForceMeshUpdate();
                _textBox.maxVisibleCharacters = 0;

                int characterCount = _textBox.textInfo.characterCount;

                for (int i = 0; i < characterCount; i++)
                {
                    if (_skipRequested)
                    {
                        _skipRequested = false;
                        _textBox.maxVisibleCharacters = characterCount;
                        break;
                    }

                    _textBox.maxVisibleCharacters = i + 1;

                    if (!_audioSource.isPlaying)
                        _audioSource.Play();

                    yield return new WaitForSeconds(_data.LetterDelayTime);
                }

                yield return new WaitUntil(() => !interactAction.IsPressed());

                _skipRequested = false;

                yield return new WaitUntil(() => _skipRequested);

                _skipRequested = false;
                _textBox.text = string.Empty;
            }

            interactAction.performed -= OnInteract;

            _textBox.maxVisibleCharacters = int.MaxValue;

            _dialogueDisplaying = false;
            OnDialogueFinished?.Invoke();
        }

        private void OnInteract(InputAction.CallbackContext context)
        {
            _skipRequested = true;
        }
    }
}

