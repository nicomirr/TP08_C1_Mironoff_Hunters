using Game.Core;
using Game.Data;
using Game.Npc;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TextInteractable : MonoBehaviour, IInteractable, ICoroutineRunner
{
    [SerializeField] private DialogueDataSo _data;

    [SerializeField] private TMP_Text _textBox;

    [SerializeField] private CanvasGroup _textBoxCanvasGroup;

    private Dialogue _dialogue;

    private bool _canInteract;
    public bool CanInteract => _canInteract;

    public bool HasInteractorInRange { get; set ; }

    private void Awake()
    {
        _dialogue = new Dialogue(_data, _textBox, GetComponent<AudioSource>(), this);
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
        _canInteract = false;
        _textBoxCanvasGroup.alpha = 1;

        _dialogue.BeginDialogue(interactAction);
    }
       
    protected virtual void Reset()
    {
        _textBoxCanvasGroup.alpha = 0;
        _canInteract = true;
    }

    public Coroutine RunRoutine(IEnumerator routine)
    {
        return StartCoroutine(routine);
    }
}
