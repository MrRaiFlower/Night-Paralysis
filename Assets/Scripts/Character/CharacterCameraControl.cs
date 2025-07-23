using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterCameraControl : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _character;
    [SerializeField] GameObject _cameraHolder;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _sensitivity;
    [Space(16.0f)]
    [SerializeField] float _smoothness;

    [HideInInspector] public Vector2 cursorVelocity;

    Vector3 _cameraRotation;

   [HideInInspector] public  Vector3 cameraRotationVelocity;

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
        cameraRotationVelocity = Vector3.Lerp(cameraRotationVelocity, Vector3.right * (cursorVelocity.x * _sensitivity * Time.deltaTime) + Vector3.up * (-cursorVelocity.y * _sensitivity * Time.deltaTime), _smoothness * Time.deltaTime);

        _cameraRotation.y += cameraRotationVelocity.x;

        _cameraRotation.x += cameraRotationVelocity.y;
        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -90f, 90f);

        _character.transform.rotation = Quaternion.Euler(0f, _cameraRotation.y, 0f);
        _cameraHolder.transform.localRotation = Quaternion.Euler(_cameraRotation.x, 0f, 0f);
    }
}
