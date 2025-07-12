using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterHeightControl : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _cameraHolder;
    [Space(16.0f)]
    [SerializeField] CharacterController _characterController;
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16.0f)]
    public float normalHeight;
    public float crouchHeight;
    [SerializeField] float _heightChangeSpeed;
    [Space(16.0f)]
    [SerializeField] float _cameraHolderHeightRatio;

    InputAction _crouchAction;

    void Start()
    {
        _crouchAction = InputSystem.actions.FindAction("Crouch");

        _characterController.height = normalHeight;
        _characterController.center = Vector3.up * normalHeight / 2.0f;

        _cameraHolder.transform.localPosition = Vector3.up * _characterController.height * _cameraHolderHeightRatio;
    }

    void Update()
    {
        ControlCharacterHeight();
    }

    void ControlCharacterHeight()
    {
        if (_crouchAction.IsPressed() && _characterController.height != crouchHeight && _characterMovementControl.isGrounded)
        {
            _characterController.height = Mathf.Lerp(_characterController.height, crouchHeight, _heightChangeSpeed * Time.deltaTime);

            if (_characterController.height < crouchHeight + 0.01f)
            {
                _characterController.height = crouchHeight;
            }

            UpdatePositions();
        }

        if (!_crouchAction.IsPressed() && _characterController.height != normalHeight && !_characterMovementControl.touchesCeiling)
        {
            _characterController.height = Mathf.Lerp(_characterController.height, normalHeight, _heightChangeSpeed * Time.deltaTime);

            if (_characterController.height > normalHeight - 0.01f)
            {
                _characterController.height = normalHeight;
            }

            UpdatePositions();
        }
    }

    void UpdatePositions()
    {
        _characterController.center = Vector3.up * _characterController.height / 2.0f;

        _cameraHolder.transform.localPosition = Vector3.up * _characterController.height * _cameraHolderHeightRatio;
    }
}
