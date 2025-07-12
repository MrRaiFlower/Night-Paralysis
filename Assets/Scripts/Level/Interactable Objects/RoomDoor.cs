using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;
    [Space(16.0f)]
    [SerializeField] BoxCollider _collider;
    [Space(16.0f)]
    [SerializeField] AudioSource _doorOpenSound;
    [SerializeField] AudioSource _doorCloseSound;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _audioVolume;

    bool _isOpen;

    bool _canChangeState;

    void Start()
    {
        _canChangeState = true;

        _doorOpenSound.volume = _audioVolume;
        _doorCloseSound.volume = _audioVolume;
    }

    void Update()
    {

    }

    public void ChangeState()
    {
        if (_canChangeState)
        {
            _canChangeState = false;

            _collider.enabled = false;

            if (_isOpen)
            {
                _doorCloseSound.Play();
                _animator.Play("Close");
            }
            else
            {
                _doorOpenSound.Play();
                _animator.Play("Open");
            }

            _isOpen = !_isOpen;

            Invoke(nameof(ResetStateChangeCooldown), 0.5f);
        }
    }

    void ResetStateChangeCooldown()
    {
        _canChangeState = true;
        
        _collider.enabled = true;
    }
}
