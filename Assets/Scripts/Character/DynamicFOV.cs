using Unity.VisualScripting;
using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Camera _camera;
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _normalFOV;
    [SerializeField] float _crouchedFOV;
    [SerializeField] float _sprintingFOV;
    [Space(16.0f)]
    [SerializeField] float _fOVChangeSpeed;

    float _targetFOV;

    void Start()
    {

    }

    void Update()
    {
        UpdateFOV();
    }

    void UpdateFOV()
    {
        switch (_characterMovementControl.speedType)
        {
            case CharacterMovementControl.speedTypeEnum.Idle: _targetFOV = _normalFOV; break;
            case CharacterMovementControl.speedTypeEnum.Walk: _targetFOV = _normalFOV; break;

            case CharacterMovementControl.speedTypeEnum.Crouch: _targetFOV = _crouchedFOV; break;

            case CharacterMovementControl.speedTypeEnum.Sprint: _targetFOV = _sprintingFOV; break;
        }

        if (_characterMovementControl.isMoving)
        {
            _targetFOV = _normalFOV;
        }

        if (_camera.fieldOfView > _targetFOV - 0.01f && _camera.fieldOfView < _targetFOV + 0.01f)
        {
            _camera.fieldOfView = _targetFOV;
        }
        else if (_characterMovementControl.isGrounded)
        {
            _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _targetFOV, _fOVChangeSpeed * Time.deltaTime);
        }
    }
}
