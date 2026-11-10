using UnityEngine;

namespace Game.GameManagement
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

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

