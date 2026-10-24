using UnityEngine;

[CreateAssetMenu(fileName = "TargetFollowerDataSo", menuName = "Scriptable Objects/TargetFollowerDataSo")]
public class TargetFollowerDataSo : ScriptableObject
{
    [SerializeField] private float _smoothTime;
    public float SmoothTime => _smoothTime;
}
