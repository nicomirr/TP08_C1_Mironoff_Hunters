using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Checkpoints
{
    public class FastTravel : MonoBehaviour
    {
        [SerializeField] private CheckpointsManager _checkpointsManager;

        [SerializeField] private Button _checkpoint1Button;
        [SerializeField] private Button _checkpoint2Button;
        [SerializeField] private Button _checkpoint3Button;
        [SerializeField] private Button _checkpoint4Button;
        [SerializeField] private Button _checkpoint5Button;
        [SerializeField] private Button _checkpoint6Button;

        [SerializeField] private Button _leaveButton;

        private CanvasGroup _canvasGroup;

        public static bool FastTravelOpen;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void OnEnable()
        {
            _checkpoint1Button.onClick.AddListener(CheckpointOneClicked);
            _checkpoint2Button.onClick.AddListener(CheckpointTwoClicked);
            _checkpoint3Button.onClick.AddListener(CheckpointThreeClicked);
            _checkpoint4Button.onClick.AddListener(CheckpointFourClicked);
            _checkpoint5Button.onClick.AddListener(CheckpointFiveClicked);
            _checkpoint6Button.onClick.AddListener(CheckpointSixClicked);

            _leaveButton.onClick.AddListener(HideFastTravel);
        }

        private void Start()
        {
            _checkpoint1Button.interactable = false;
            _checkpoint2Button.interactable = false;
            _checkpoint3Button.interactable = false;
            _checkpoint4Button.interactable = false;
            _checkpoint5Button.interactable = false;
            _checkpoint6Button.interactable = false;
        }        

        private void Update()
        {
            foreach (int checkpointNumber in _checkpointsManager.CheckpointsReached)
            {
                if (checkpointNumber == 1)
                    _checkpoint1Button.interactable = true;

                if (checkpointNumber == 2)
                    _checkpoint2Button.interactable = true;

                if (checkpointNumber == 3)
                    _checkpoint3Button.interactable = true;

                if (checkpointNumber == 4)
                    _checkpoint4Button.interactable = true;

                if (checkpointNumber == 5)
                    _checkpoint5Button.interactable = true;

                if (checkpointNumber == 6)
                    _checkpoint6Button.interactable = true;
            }
            
        }

        private void OnDisable()
        {
            _checkpoint1Button.onClick.RemoveAllListeners();
            _checkpoint2Button.onClick.RemoveAllListeners();
            _checkpoint3Button.onClick.RemoveAllListeners();
            _checkpoint4Button.onClick.RemoveAllListeners();
            _checkpoint5Button.onClick.RemoveAllListeners();
            _checkpoint6Button.onClick.RemoveAllListeners();
        }

        public void DisplayFastTravel()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            FastTravelOpen = true;

            EventSystem.current.SetSelectedGameObject(_leaveButton.gameObject);
        }

        private void HideFastTravel()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            FastTravelOpen = false;
        }

        private void CheckpointOneClicked()
        {
            _checkpointsManager.FastTravel(1);
            HideFastTravel();
        }

        private void CheckpointTwoClicked()
        {
            _checkpointsManager.FastTravel(2);
            HideFastTravel();
        }

        private void CheckpointThreeClicked()
        {
            _checkpointsManager.FastTravel(3);
            HideFastTravel();
        }

        private void CheckpointFourClicked()
        {
            _checkpointsManager.FastTravel(4);
            HideFastTravel();
        }

        private void CheckpointFiveClicked()
        {
            _checkpointsManager.FastTravel(5);
            HideFastTravel();
        }

        private void CheckpointSixClicked()
        {
            _checkpointsManager.FastTravel(6);
            HideFastTravel();
        }
    }

}
