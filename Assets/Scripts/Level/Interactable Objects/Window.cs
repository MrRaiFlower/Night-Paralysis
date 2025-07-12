using UnityEngine;

public class Window : MonoBehaviour
{
    public enum dangerStageEnum
    {
        Safe,
        Low,
        Moderate,
        High
    }

    [Header("References")]
    [Space(16.0f)]
    [SerializeField] Animator _animator;

    [HideInInspector] public dangerStageEnum dangerStage;

    bool _canChangeState;

    void Start()
    {
        dangerStage = dangerStageEnum.Safe;

        _canChangeState = true;
    }

    void Update()
    {

    }

    public void IncreaseDangerStage()
    {
        if (dangerStage != dangerStageEnum.High && _canChangeState)
        {
            _canChangeState = false;

            dangerStage += 1;

            switch (dangerStage)
            {
                case dangerStageEnum.Low: _animator.Play("HandleOpenHalf"); break;
                case dangerStageEnum.Moderate: _animator.Play("HandleOpenFull"); break;
                case dangerStageEnum.High: _animator.Play("WindowOpen"); break;
            }

            Invoke(nameof(ResetStateChangeCooldown), 0.2f);
        }
    }

    public void DecreaseDangerStage()
    {
        if (dangerStage != dangerStageEnum.Safe && _canChangeState)
        {
            _canChangeState = false;

            dangerStage -= 1;

            switch (dangerStage)
            {
                case dangerStageEnum.Safe: _animator.Play("HandleCloseFull"); break;
                case dangerStageEnum.Low: _animator.Play("HandleCloseHalf"); break;
                case dangerStageEnum.Moderate: _animator.Play("WindowClose"); break;
            }

            Invoke(nameof(ResetStateChangeCooldown), 0.2f);
        }
    }
    
    void ResetStateChangeCooldown()
    {
        _canChangeState = true;
    }
}
