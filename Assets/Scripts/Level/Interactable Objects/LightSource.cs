using UnityEngine;

public class LightSource : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;

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
