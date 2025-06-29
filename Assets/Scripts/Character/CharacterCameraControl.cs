using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCameraControl : MonoBehaviour
{
    [Header("References")]
    [Space(16f)]
    [SerializeField] GameObject _character;
    [SerializeField] GameObject _cameraHolder;

    [Header("Parameters")]
    [Space(16f)]
    [SerializeField] float _sensitivity;

    [HideInInspector] public Vector2 cursorVelocity;

    Vector3 _cameraRotation;

    InputAction _lookAction;

    void Start()
    {
        _lookAction = InputSystem.actions.FindAction("Look");

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        ProcessLookInput();

        RotateCamera();
    }

    void ProcessLookInput()
    {
        cursorVelocity = _lookAction.ReadValue<Vector2>() / Time.unscaledDeltaTime;
    }

    void RotateCamera()
    {
        _cameraRotation.y += cursorVelocity.x * _sensitivity * Time.deltaTime;

        _cameraRotation.x -= cursorVelocity.y * _sensitivity * Time.deltaTime;
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -90f, 90f);

        _character.transform.rotation = Quaternion.Euler(0f, _cameraRotation.y, 0f);
        _cameraHolder.transform.localRotation = Quaternion.Euler(_cameraRotation.x, 0f, 0f);
    }
}
