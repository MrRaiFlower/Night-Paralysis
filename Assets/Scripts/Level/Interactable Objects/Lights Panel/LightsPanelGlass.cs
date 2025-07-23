using UnityEngine;

public class LightsPanelGlass : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;

    bool _isOpen;

    bool _isReadyToChangeState;

    void Start()
    {
        _isReadyToChangeState = true;
    }

    void Update()
    {

    }

    public void ChangeState()
    {
        if (!_isReadyToChangeState)
        {
            return;
        }

        _isReadyToChangeState = false;

        if (_isOpen)
        {
            _animator.Play("Close");
        }
        else
        {
            _animator.Play("Open");
        }

        _isOpen = !_isOpen;

        Invoke(nameof(ResetStateChangeCooldown), 0.8f);
    }

    void ResetStateChangeCooldown()
    {
        _isReadyToChangeState = true;
    }
}
