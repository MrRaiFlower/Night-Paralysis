using UnityEngine;

public class CharacterStamina : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _maxStamina;
    [Space(16.0f)]
    [SerializeField] float _staminaIdleRegeneration;
    [SerializeField] float _staminaCrouchRegeneration;
    [SerializeField] float _staminaWalkRegeneration;
    [Space(16.0f)]
    [SerializeField] float _staminaSprintConsumption;
    [SerializeField] float _staminaJumpConsumption;
    [Space(16.0f)]
    [SerializeField] float _tiredStaminaTreshold;

    [HideInInspector] public float stamina;

    bool _isTired;

    [HideInInspector] public bool canSprint;
    [HideInInspector] public bool canJump;

    void Start()
    {
        stamina = _maxStamina;

        _isTired = false;
    }

    void Update()
    {
        switch (_characterMovementControl.speedType)
        {
            case CharacterMovementControl.speedTypeEnum.Idle: stamina += _staminaIdleRegeneration * Time.deltaTime; break;
            case CharacterMovementControl.speedTypeEnum.Crouch: stamina += _staminaCrouchRegeneration * Time.deltaTime; break;
            case CharacterMovementControl.speedTypeEnum.Walk: stamina += _staminaWalkRegeneration * Time.deltaTime; break;

            case CharacterMovementControl.speedTypeEnum.Sprint: stamina -= _staminaSprintConsumption * Time.deltaTime; break;
        }

        if (_characterMovementControl.hasJumped)
        {
            stamina -= _staminaJumpConsumption;
        }

        if (stamina <= 0f)
        {
            _isTired = true;
        }

        if (stamina >= _tiredStaminaTreshold)
        {
            _isTired = false;
        }

        stamina = Mathf.Clamp(stamina, 0f, _maxStamina);

        canSprint = stamina > 0f && !_isTired;
        canJump = stamina > _staminaJumpConsumption && !_isTired;
    }
}
