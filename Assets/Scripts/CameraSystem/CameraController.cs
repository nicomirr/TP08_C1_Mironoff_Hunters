using UnityEngine;
using Game.CameraSystem;
using Game.Core;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _singlePlayerCamera;
    [SerializeField] private TargetFollower _singlePlayerTargetFollower;

    [SerializeField] private Transform _playerOneCamera;
    [SerializeField] private TargetFollower _playerOneTargetFollower;

    [SerializeField] private Transform _playerTwoCamera;
    [SerializeField] private TargetFollower _playerTwoTargetFollower;

    private void OnEnable()
    {
        Door.OnDoorUsed += SetCameraZone;
    }

    private void OnDisable()
    {
        Door.OnDoorUsed -= SetCameraZone;
    }

    private void SetCameraZone(PlayerType playerType, Transform transform, float newLeftLimit)
    {
        switch (playerType)
        {            
            case PlayerType.Player1:

                if(GameMode.CurrentGameModeType == GameModeType.SinglePlayer)
                {
                    _singlePlayerCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
                    _singlePlayerTargetFollower.ChangeLeftLimit(newLeftLimit);
                    _singlePlayerTargetFollower.ResetSmoothVelocity();

                    return;
                }

                _playerOneCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
                _playerOneTargetFollower.ChangeLeftLimit(newLeftLimit);
                _playerOneTargetFollower.ResetSmoothVelocity();

                break;

            case PlayerType.Player2:

                _playerTwoCamera.position = new Vector3(transform.position.x, transform.position.y, -10);
                _playerTwoTargetFollower.ChangeLeftLimit(newLeftLimit);
                _playerTwoTargetFollower.ResetSmoothVelocity();

                break;
        }
    }
}
