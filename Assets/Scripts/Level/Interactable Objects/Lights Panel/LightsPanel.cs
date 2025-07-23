using UnityEngine;

public class LightsPanel : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;
    [Space(16.0f)]
    [SerializeField] GameObject _greenLights;
    [SerializeField] GameObject _redLights;
    [Space(16.0f)]
    [SerializeField] AudioSource _switchSound;
    [Space(16.0f)]
    [SerializeField] GameObject[] _lights;
    [SerializeField] GameObject[] _lightSwitches;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _powerDrain;
    [Space(16.0f)]
    [SerializeField] float _reloadPowerDecrement;

    [HideInInspector] public bool isOn;

    [HideInInspector] public float power;

    [HideInInspector] public float currentPowerDrain;

    bool _isReadyToChangeState;

    int _reloadsNumber;

    void Start()
    {
        isOn = true;

        _isReadyToChangeState = true;

        power = 100.0f;
    }

    void Update()
    {
        UpdatePower();

        if (power == 0f && isOn)
        {
            _reloadsNumber += 1;

            SwitchState();

            power = 100.0f - (_reloadPowerDecrement * _reloadsNumber);
        }
    }

    void UpdatePower()
    {
        if (!isOn)
        {
            currentPowerDrain = 0f;

            return;
        }

        int _poweredLightsNumber = 0;

        foreach (GameObject _light in _lights)
        {
            if (_light.GetComponent<LightSource>().isOn)
            {
                _poweredLightsNumber += 1;
            }
        }

        currentPowerDrain = _powerDrain * _poweredLightsNumber;

        power -= currentPowerDrain * Time.deltaTime;

        power = Mathf.Clamp(power, 0f, 100.0f);
    }

    public void SwitchState()
    {
        if (!_isReadyToChangeState)
        {
            return;
        }

        _isReadyToChangeState = false;

        _switchSound.Play();

        if (isOn)
        {
            _animator.Play("TurnOff");

            _greenLights.SetActive(false);
            _redLights.SetActive(true);
        }
        else
        {
            _animator.Play("TurnOn");

            _redLights.SetActive(false);
            _greenLights.SetActive(true);
        }

        isOn = !isOn;

        Invoke(nameof(ResetStateChangeCooldown), 0.2f);

        if (isOn)
        {
            foreach (GameObject _light in _lights)
            {
                _light.GetComponent<LightSource>().hasNoPower = false;
            }
        }
        else
        {
            foreach (GameObject _light in _lights)
            {
                _light.GetComponent<LightSource>().hasNoPower = true;
            }
        }

        foreach (GameObject _lightSwitch in _lightSwitches)
        {
            _lightSwitch.GetComponent<LightSwitch>().UpdatePower();
        }
    }

    void ResetStateChangeCooldown()
    {
        _isReadyToChangeState = true;
    }
}
