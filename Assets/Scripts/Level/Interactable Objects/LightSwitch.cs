using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;
    [Space(16.0f)]
    [SerializeField] GameObject[] _lights;
    [Space(16.0f)]
    [SerializeField] AudioSource _switchSound;

    [HideInInspector] public bool isOn;

    bool _isReadyToSwitch;

    void Start()
    {
        _isReadyToSwitch = true;
    }

    void Update()
    {

    }

    public void SwitchState()
    {
        if (_isReadyToSwitch)
            {
                _isReadyToSwitch = false;

                foreach (GameObject _light in _lights)
                {
                    if (!_light.GetComponent<LightSource>().hasNoPower)
                    {
                        _light.GetComponent<LightSource>().SwitchState();
                    }
                }

                _switchSound.Play();

                if (isOn)
                {
                    _animator.Play("TurnOff");
                }
                else
                {
                    _animator.Play("TurnOn");
                }

                isOn = !isOn;

                Invoke(nameof(ResetReadyToSwitch), 0.2f);
            }
    }

    void ResetReadyToSwitch()
    {
        _isReadyToSwitch = true;
    }

    public void SwitchOff()
    {
        if (isOn)
        {
            SwitchState();
        }
    }

    public void UpdatePower()
    {
        foreach (GameObject _light in _lights)
        {
            if (_light.GetComponent<LightSource>().hasNoPower && _light.GetComponent<LightSource>().isOn && isOn)
            {
                _light.GetComponent<LightSource>().SwitchState();
            }

            if (!_light.GetComponent<LightSource>().hasNoPower && !_light.GetComponent<LightSource>().isOn && isOn)
            {
                _light.GetComponent<LightSource>().SwitchState();
            }
        }
    }
}
