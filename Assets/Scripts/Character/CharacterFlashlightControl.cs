using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterFlashlightControl : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;
    [Space(16.0f)]
    [SerializeField] CharacterSFX _characterSFX;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _switchCooldown;
    [Space(16.0f)]
    [SerializeField] float _maxCharge;
    [SerializeField] float _dischargeSpeed;

    bool _canSwitch;

    bool _isOn;

    [HideInInspector] public float charge;

    InputAction _flashlightAction;

    void Start()
    {
        _flashlightAction = InputSystem.actions.FindAction("Flashlight");

        _canSwitch = true;

        charge = _maxCharge;
    }

    void Update()
    {
        if (_flashlightAction.WasPressedThisFrame() && _canSwitch)
        {
            _characterSFX.PlayFlashlightSound();

            if (charge > 0f)
            {
                _canSwitch = false;

                _characterSFX.PlayFlashlightSound();

                if (_isOn)
                {
                    _animator.Play("TurnOff");
                }
                else
                {
                    _animator.Play("TurnOn");
                }

                _isOn = !_isOn;

                Invoke(nameof(ResetSwitchCooldown), _switchCooldown);
            }
        }

        if (_isOn)
        {
            charge -= _dischargeSpeed * Time.deltaTime;
        }

        if (charge < 0f)
        {
            charge = 0f;

            _isOn = false;

            _animator.Play("TurnOff");
        }
    }

    void ResetSwitchCooldown()
    {
        _canSwitch = true;
    }
}
