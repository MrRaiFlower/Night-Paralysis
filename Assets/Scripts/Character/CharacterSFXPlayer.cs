using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] AudioSource _footstepsAudioSource;
    [Space(16.0f)]
    [SerializeField] AudioSource _jumpAudioSource;
    [Space(16.0f)]
    [SerializeField] AudioSource _landingAudioSource;
    [Space(16.0f)]
    [SerializeField] AudioSource _flashlightAudioSource;
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _normalFootstepsVolume;
    [SerializeField] float _lightFootstepsVolume;
    [SerializeField] float _heavyFootstepsVolume;
    [Space(16.0f)]
    [SerializeField] float _jumpVolume;
    [Space(16.0f)]
    [SerializeField] float _normalLandingVelocityTreshold;
    [SerializeField] float _lightLandingVelocityTreshold;
    [SerializeField] float _heavyLandingVelocityTreshold;
    [Space(16.0f)]
    [SerializeField] float _normalLandingVolume;
    [SerializeField] float _lightLandingVolume;
    [SerializeField] float _heavyLandingVolume;
    [Space(16.0f)]
    [SerializeField] float _flashlightVolume;

    void Start()
    {
        _jumpAudioSource.volume = _jumpVolume;
        _flashlightAudioSource.volume = _flashlightVolume;
    }

    void Update()
    {

    }

    public void PlayFootstepSound()
    {
        switch (_characterMovementControl.speedType)
        {
            case CharacterMovementControl.speedTypeEnum.Walk: _footstepsAudioSource.volume = _normalFootstepsVolume; break;
            case CharacterMovementControl.speedTypeEnum.Crouch: _footstepsAudioSource.volume = _lightFootstepsVolume; break;
            case CharacterMovementControl.speedTypeEnum.Sprint: _footstepsAudioSource.volume = _heavyFootstepsVolume; break;
        }

        _footstepsAudioSource.Play();
    }

    public void PlayJumpSound()
    {
        _jumpAudioSource.Play();
    }

    public void PlayLandingSound()
    {
        if (Mathf.Abs(_characterMovementControl.landingVelocity) < _lightLandingVelocityTreshold)
        {
            return;
        }
        else if (Mathf.Abs(_characterMovementControl.landingVelocity) < _normalLandingVelocityTreshold)
        {
            _landingAudioSource.volume = _lightLandingVolume;
        }
        else if (Mathf.Abs(_characterMovementControl.landingVelocity) < _heavyLandingVelocityTreshold)
        {
            _landingAudioSource.volume = _normalLandingVolume;
        }
        else
        {
            _landingAudioSource.volume = _heavyLandingVolume;
        }

        _landingAudioSource.Play();
    }

    public void PlayFlashlightSound()
    {
        _flashlightAudioSource.Play();
    }
}
