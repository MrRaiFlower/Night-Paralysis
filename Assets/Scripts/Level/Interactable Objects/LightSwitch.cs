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

    bool _isOn;

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
                _light.GetComponent<LightSource>().SwitchState();
            }

            _switchSound.Play();

            if (_isOn)
            {
                _animator.Play("TurnOff");
            }
            else
            {
                _animator.Play("TurnOn");
            }

            _isOn = !_isOn;

            Invoke(nameof(ResetReadyToSwitch), 0.2f);
        }
    }

    void ResetReadyToSwitch()
    {
        _isReadyToSwitch = true;
    }
}
