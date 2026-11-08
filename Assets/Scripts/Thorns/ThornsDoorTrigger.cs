using UnityEngine;

namespace Game.Thorns
{
    public class ThornsDoorTrigger : MonoBehaviour
    {
        [SerializeField] private ThornsDoor _thornsDoor;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_thornsDoor.IsOpen) return;

            _thornsDoor.CloseDoor();
        }
    }

}

