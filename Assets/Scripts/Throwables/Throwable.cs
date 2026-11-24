using UnityEngine;
using Game.Data;
using Game.ObjectPooling;

namespace Game.Throwables
{
    public class Throwable : MonoBehaviour
    {
        [SerializeField] private ThrowableDataSo _data;

        // Suggestion: el campo es [SerializeField] pero Awake lo pisa con GetComponent. Elegí una de las dos formas.
        [SerializeField] private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void Throw(float direction, Vector2 throwerVelocity)
        {
            _rb.linearVelocity = new Vector2(throwerVelocity.x + (Mathf.Sign(direction) * _data.HorizontalSpeed), _data.VerticalSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Warning: explota con cualquier trigger que toque (zonas de detección de enemigos, pickables, otros triggers del mapa) porque no filtra nada.
            // Filtrá por capa o por componente (por ejemplo BodyColliderMarker o suelo) antes de explotar.
            GameObject explosionObject = ObjectPool.Instance.GetPoolObject(_data.ExplosionType);

            explosionObject.transform.position = this.transform.position;
            explosionObject.SetActive(true);

            explosionObject.GetComponent<ParticleSystem>().Play();

            this.gameObject.SetActive(false);
        }
    }

}
