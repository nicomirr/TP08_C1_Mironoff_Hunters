using UnityEngine;

namespace Game.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        // Warning: campos public static mutables. Cualquier clase puede escribir BossKilled, KeyPicked o FragmentPicked y no se resetean al volver al menú.
        // Exponé properties de solo lectura con métodos para cambiarlos y reseteálos junto con GameSessionManager.EraseSavedData().
        public static bool BossKilled;

        public static bool KeyPicked;

        public static bool FragmentPicked;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

}

