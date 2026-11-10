using UnityEngine;
using System.Collections;
using Game.GameManagement;
using Game.Marker;

namespace Game.Environment
{
    public class Key : MonoBehaviour
    {
        private AudioSource _audioSource;
        private Coroutine _routine;

        private SpriteRenderer _spriteRenderer; 

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            if (GameManager.KeyPicked)
                this.gameObject.SetActive(false);
        }

        private IEnumerator PickKeyRoutine()
        {
            GameManager.KeyPicked = true;

            _spriteRenderer.enabled = false;
            _audioSource.Play();

            yield return new WaitForSeconds(0.3f);

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
