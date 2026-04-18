using UnityEngine;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerLocomotion))]
[RequireComponent(typeof(PlayerRotation))]
[RequireComponent(typeof(PlayerStateMachine))]
[RequireComponent(typeof(PlayerInteraction))]
[RequireComponent(typeof(PlayerItemHolder))]
[RequireComponent(typeof(PlayerInspection))]
public class PlayerContext : MonoBehaviour {
    [Header("Player Configs")]
    [SerializeField] private LocomotionData _locomotionData;
    [SerializeField] private CameraData _cameraData;
    [SerializeField] private InteractionData _interactionData;
    [SerializeField] private InspectionData _inspectionData;

    [Header("Player Objects")]
    [SerializeField] private PlayerCamera _camera;
    [SerializeField] private Transform _itemHoldPoint;
    [SerializeField] private Transform _inspectPoint;

    [Header("Managers")]
    [SerializeField] private DialogueManager _dialogueController;

    // Data component references
    public LocomotionData LocomotionData => _locomotionData;
    public CameraData CameraData => _cameraData;
    public InteractionData InteractionData => _interactionData;
    public InspectionData InspectionData => _inspectionData;

    // Player logic components
    public PlayerInputHandler Input { get; private set; }
    public PlayerLocomotion Locomotion { get; private set; }
    public PlayerRotation Rotation { get; private set; }
    public PlayerInteraction Interaction { get; private set; }
    public PlayerItemHolder ItemHolder { get; private set; }
    public PlayerInspection Inspection { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public DialogueManager DialogueManager { get; private set; }

    // Player object references
    public PlayerCamera Camera => _camera;
    public Transform ItemHoldPoint => _itemHoldPoint;
    public Transform InspectPoint => _inspectPoint;

    // Logic Limits
    private bool _interactionLocked;
    public bool CanInteract => !_interactionLocked;
    public void LockInteraction() => _interactionLocked = true;
    public void UnlockInteraction() => _interactionLocked = false;

    private void Awake() {
        Input = GetComponent<PlayerInputHandler>();
        Locomotion = GetComponent<PlayerLocomotion>();
        Rotation = GetComponent<PlayerRotation>();
        StateMachine = GetComponent<PlayerStateMachine>();
        Interaction = GetComponent<PlayerInteraction>();
        ItemHolder = GetComponent<PlayerItemHolder>();
        Inspection = GetComponent<PlayerInspection>();
        DialogueManager = _dialogueController;

        Locomotion.Initialize(_locomotionData);
        Rotation.Initialize(Camera.transform, _cameraData);
        Interaction.Initialize(this);
        ItemHolder.Initialize(_itemHoldPoint);
        Camera.Initialize(_cameraData);
    }
}