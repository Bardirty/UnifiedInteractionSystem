using UnityEngine;

[CreateAssetMenu(fileName = "DefaultInspectionData", menuName = "Player/Inspection Data")]
public class InspectionData : ScriptableObject
{
    [SerializeField] private float rotationSpeed = 0.2f;
    [SerializeField] private float transitionDuration = 0.5f;
    public float RotationSpeed => rotationSpeed;
    public float TransitionDuration => transitionDuration;
}
