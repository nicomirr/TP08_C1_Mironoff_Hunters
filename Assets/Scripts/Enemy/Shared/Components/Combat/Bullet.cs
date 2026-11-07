using UnityEngine;
using Game.Data;
using Game.Marker;

namespace Game.Enemy
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private BulletDataSo _data;

        private Rigidbody2D _rb;

        private float _lifeTimer;

        public float Speed => _data.Speed;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _lifeTimer += Time.deltaTime;

            if (_lifeTimer >= _data.LifeTime)
            {
                _lifeTimer = 0;
                this.gameObject.SetActive(false);
            }
        }

        public void Move(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            _rb.linearVelocity = direction * _data.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerMarker _))
            {
                _lifeTimer = 0;
                this.gameObject.SetActive(false);
            }
        }
    }

}
