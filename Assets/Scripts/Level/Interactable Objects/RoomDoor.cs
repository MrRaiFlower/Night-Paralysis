using UnityEngine;

public class RoomDoor : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;
    [Space(16.0f)]
    [SerializeField] BoxCollider _collider;
    [Space(16.0f)]
    [SerializeField] AudioSource _doorSound;

    bool _isOpen;

    bool _canChangeState;

    void Start()
    {
        _canChangeState = true;
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

            _doorSound.Play();

            if (_isOpen)
            {
                _animator.Play("Close");
            }
            else
            {
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
