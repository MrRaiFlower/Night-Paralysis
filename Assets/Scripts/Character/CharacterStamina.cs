using UnityEngine;

public class CharacterStamina : MonoBehaviour
{
    [Header("References")]
    [Space(16f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16f)]
    [SerializeField] float _maxStamina;
    [Space(16f)]
    [SerializeField] float _staminaIdleRegeneration;
    [SerializeField] float _staminaCrouchRegeneration;
    [SerializeField] float _staminaWalkRegeneration;
    [Space(16f)]
    [SerializeField] float _staminaSprintConsumption;
    [SerializeField] float _staminaJumpConsumption;
    [Space(16f)]
    [SerializeField] float _tiredCooldown;

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
            case CharacterMovementControl.speedTypeEnum.None: stamina += _staminaIdleRegeneration * Time.deltaTime; break;
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

            Invoke(nameof(ResetTiredCooldown), _tiredCooldown);
        }

        stamina = Mathf.Clamp(stamina, 0f, _maxStamina);

        canSprint = stamina > 0f && !_isTired;
        canJump = stamina > _staminaJumpConsumption && !_isTired;
    }

    void ResetTiredCooldown()
    {
        _isTired = false;
    }
}
