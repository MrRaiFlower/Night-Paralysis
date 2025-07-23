using UnityEngine;
using UnityEngine.XR;

public class CharacterHeadBobbing : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _camera;
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;
    [SerializeField] CharacterSFX _characterSFX;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _footstepsSpeed;
    [SerializeField] float _footstepsDepth;
    [Space(16.0f)]
    [SerializeField] float _normalFootstepsSpeedMultiplier;
    [SerializeField] float _lightFootstepsSpeedMultiplier;
    [SerializeField] float _heavyFootstepsSpeedMultiplier;
    [Space(16.0f)]
    [SerializeField] float _normalFootstepsDepthMultiplier;
    [SerializeField] float _lightFootstepsDepthMultiplier;
    [SerializeField] float _heavyFootstepsDepthMultiplier;
    [Space(16.0f)]
    [SerializeField] float _landingOffsetVelocityTreshold;
    [SerializeField] float _landingOffsetVelocityProportion;
    [SerializeField] float _LandingOffset;
    [SerializeField] float _maxLandingOffset;
    [SerializeField] float _landingOffsetResetSpeed;

    bool _makesFootstep;
    bool _stepsUp;
    bool _stepsRight;

    float _foostepsSpeedMultiplier;
    float _foostepsDepthMultiplier;
    
    Vector3 _footstepOffset;

    bool _lands;

    float _landingVelocity;

    float _targetLandingOffset;
    float _currentLandingOffset;

    Vector3 _offset;

    void Start()
    {

    }

    void Update()
    {
        if (_characterMovementControl.speed > 1.75f && _characterMovementControl.isGrounded && !_makesFootstep)
        {
            _makesFootstep = true;
        }

        HandleFootsteps();

        HandleLanding();

        ComposeOffset();

        ApplyOffset();
    }

    void HandleFootsteps()
    {
        if (_makesFootstep)
        {
            switch (_characterMovementControl.speedType)
            {
                case CharacterMovementControl.speedTypeEnum.Walk:_foostepsSpeedMultiplier = _normalFootstepsSpeedMultiplier; _foostepsDepthMultiplier = _normalFootstepsDepthMultiplier; break;
                case CharacterMovementControl.speedTypeEnum.Crouch: _foostepsSpeedMultiplier = _lightFootstepsSpeedMultiplier; _foostepsDepthMultiplier = _normalFootstepsDepthMultiplier; break;
                case CharacterMovementControl.speedTypeEnum.Sprint: _foostepsSpeedMultiplier = _heavyFootstepsSpeedMultiplier; _foostepsDepthMultiplier = _normalFootstepsDepthMultiplier; break;
            }

            if (_stepsUp)
            {
                _footstepOffset.y += _footstepsSpeed * _foostepsSpeedMultiplier * Time.deltaTime;

                if (_footstepOffset.y > 0f)
                {
                    _footstepOffset.y = 0f;

                    _stepsUp = false;

                    _stepsRight = !_stepsRight;

                    _makesFootstep = false;
                }
            }
            else
            {
                _footstepOffset.y -= _footstepsSpeed * _foostepsSpeedMultiplier * Time.deltaTime;

                if (_footstepOffset.y < -_footstepsDepth * _foostepsDepthMultiplier)
                {
                    _footstepOffset.y = -_footstepsDepth;

                    _characterSFX.PlayFootstepSound();

                    _stepsUp = true;
                }
            }

            if (_stepsRight)
            {
                _footstepOffset.x = Mathf.Abs(_footstepOffset.y) / 2.0f;
            }
            else
            {
                _footstepOffset.x = -Mathf.Abs(_footstepOffset.y) / 2.0f;
            }
        }
    }

    void HandleLanding()
    {
        if (_characterMovementControl.hasGrounded && -_characterMovementControl.landingVelocity >= _landingOffsetVelocityTreshold)
        {
            _landingVelocity = _characterMovementControl.landingVelocity;

            _targetLandingOffset = _LandingOffset * _landingVelocity / _landingOffsetVelocityProportion;

            if (_targetLandingOffset > _maxLandingOffset)
            {
                _targetLandingOffset = _maxLandingOffset;
            }

            _lands = true;
        }

        if (_lands)
        {
            _currentLandingOffset += _landingVelocity * Time.deltaTime;

            if (_currentLandingOffset < _targetLandingOffset)
            {
                _currentLandingOffset = _targetLandingOffset;

                _lands = false;
            }
        }
        else if (_currentLandingOffset != 0f)
        {
            _currentLandingOffset = Mathf.Lerp(_currentLandingOffset, 0f, _landingOffsetResetSpeed * Time.deltaTime);

            if (_currentLandingOffset > -0.01f)
            {
                _currentLandingOffset = 0f;
            }
        }
    }

    void ComposeOffset()
    {
        _offset = _footstepOffset + Vector3.up * _currentLandingOffset;
    }

    void ApplyOffset()
    {
        _camera.transform.localPosition = _offset;
    }
}
