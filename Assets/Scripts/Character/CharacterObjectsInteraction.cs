using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterObjectsInteraction : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _camera;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _interactionRange;
    [Space(16.0f)]
    [SerializeField] LayerMask _interactableObjectsLayerMask;

    InputAction _interactAction;

    void Start()
    {
        _interactAction = InputSystem.actions.FindAction("Interact");
    }

    void Update()
    {
        if (_interactAction.WasPressedThisFrame())
        {
            Interact();
        }
    }

    void Interact()
    {
        RaycastHit _raycastHitInfo;

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHitInfo, _interactionRange, _interactableObjectsLayerMask))
        {
            switch (_raycastHitInfo.transform.gameObject.tag)
            {
                case "Window": _raycastHitInfo.transform.gameObject.GetComponentInParent<Window>().DecreaseDangerStage(); break;
                case "Room Door": _raycastHitInfo.transform.gameObject.GetComponentInParent<RoomDoor>().ChangeState(); break;
                case "Entrance Door": _raycastHitInfo.transform.gameObject.GetComponentInParent<EntranceDoor>().DecreaseDangerStage(); break;
                case "Light Switch": _raycastHitInfo.transform.gameObject.GetComponentInParent<LightSwitch>().SwitchState(); break;
                case "Vent Tunnel": _raycastHitInfo.transform.gameObject.GetComponentInParent<VentTunnel>().DecreaseDangerStage(); break;
            }
        }
    }
}
