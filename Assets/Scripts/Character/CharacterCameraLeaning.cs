using UnityEngine;

public class CharacterCameraLeaning : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _camera;
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;
    [SerializeField] CharacterCameraControl _characterCameraControl;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _movementLeanAngle;
    [SerializeField] float _movementLeanAngleProportion;
    [SerializeField] float _movementLeanSpeed;
    [Space(16.0f)]
    [SerializeField] float _rotationLeanAngle;
    [SerializeField] float _rotationLeanAngleProportion;
    [SerializeField] float _rotationLeanSpeed;
    [Space(16.0f)]
    [SerializeField] float _leanCorrectionSpeed;

    float _currentMovementLeanAngle;
    float _currentRotationLeanAngle;

    float currentLeanAngle;

    void Start()
    {

    }

    void Update()
    {
        CalculateCameraLean();

        UpdateCameraLean();
    }

    void CalculateCameraLean()
    {
        _currentMovementLeanAngle = Mathf.Lerp(currentLeanAngle, -_movementLeanAngle * transform.InverseTransformDirection(_characterMovementControl.velocity).x / _movementLeanAngleProportion, _movementLeanSpeed * Time.deltaTime);

        _currentRotationLeanAngle = Mathf.Lerp(_currentRotationLeanAngle, -_rotationLeanAngle * _characterCameraControl.cameraRotationVelocity.x / _rotationLeanAngleProportion, _rotationLeanSpeed * Time.deltaTime);

        if (_currentRotationLeanAngle != 0 && _currentMovementLeanAngle != 0)
        {
            if (_currentRotationLeanAngle > 0 != _currentMovementLeanAngle > 0)
            {
                _currentRotationLeanAngle = Mathf.Lerp(_currentRotationLeanAngle, -_currentRotationLeanAngle, _leanCorrectionSpeed * Time.deltaTime);
            }
        }

        currentLeanAngle = _currentMovementLeanAngle + _currentRotationLeanAngle;
    }

    void UpdateCameraLean()
    {
        _camera.transform.localRotation = Quaternion.Euler(0f, 0f, currentLeanAngle);
    }
}
