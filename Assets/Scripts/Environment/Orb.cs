using Game.GameManagement;
using Game.Marker;
using System.Collections;
using UnityEngine;

namespace Game.Environment
{
    public class Orb : MonoBehaviour
    {
        [SerializeField] private GameObject _textObject;

        private AudioSource _audioSource;
        private Coroutine _routine;

        private SpriteRenderer _spriteRenderer;
        private Animator _animator;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            if (GameManager.FragmentPicked)
                this.gameObject.SetActive(false);
        }

        private IEnumerator PickKeyRoutine()
        {
            GameManager.FragmentPicked = true;

            _animator.SetTrigger("Empty");

            _spriteRenderer.enabled = false;
            _audioSource.Play();

            _textObject.SetActive(true);

            yield return new WaitForSeconds(0.4f);

            _textObject.SetActive(false);

            yield return new WaitForSeconds(0.4f);

            _textObject.SetActive(true);

            yield return new WaitForSeconds(0.4f);

            _textObject.SetActive(false);

            yield return new WaitForSeconds(0.4f);

            _textObject.SetActive(true);

            yield return new WaitForSeconds(0.4f);

            _textObject.SetActive(false);

            this.gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out PlayerMarker _)) return;

            if (_routine != null) return;

            _routine = StartCoroutine(PickKeyRoutine());
        }
    }

}
