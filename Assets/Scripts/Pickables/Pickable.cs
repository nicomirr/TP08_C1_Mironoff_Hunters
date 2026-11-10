using UnityEngine;
using Game.Core;
using Game.Data;

namespace Game.Pickables
{
    public class Pickable : MonoBehaviour
    {
        [SerializeField] private PickableDataSo _data;

        private Rigidbody2D _rb;

        private float _lifeTimer;

        public PickableType PickableType => _data.PickableType;
        public int Amount => _data.Amount;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            Spawn();
        }

        private void Update()
        {
            _lifeTimer += Time.deltaTime;

            if(_lifeTimer >= _data.Lifetime)
            {
                _lifeTimer = 0;
                this.gameObject.SetActive(false);
            }
        }

        private void Spawn()
        {
            float randomAngle = Random.Range(_data.MinAngle, _data.MaxAngle);
            float radians = randomAngle * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));

            float randomForce = Random.Range(_data.MinForce, _data.MaxForce);

            _rb.AddForce(direction * randomForce, ForceMode2D.Impulse);
        }

    }
}

