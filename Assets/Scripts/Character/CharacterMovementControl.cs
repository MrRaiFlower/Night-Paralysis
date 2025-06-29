using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovementControl : MonoBehaviour
{
    public enum speedTypeEnum
    {
        None,
        Walk,
        Crouch,
        Sprint
    }

    [Header("References")]
    [Space(16f)]
    [SerializeField] GameObject _character;
    [Space(16f)]
    [SerializeField] CharacterController _characterController;
    [Space(16f)]
    [SerializeField] CharacterHeightControl _characterHeightControl;
    [SerializeField] CharacterStamina _characterStaminaControl;
    [SerializeField] CharacterSFX _characterSFX;

    [Header("Parameters")]
    [Space(16f)]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _crouchSpeed;
    [SerializeField] float _sprintSpeed;
    [Space(16f)]
    [SerializeField] float _acceleration;
    [SerializeField] float _angularAcceleration;
    [SerializeField] float _groundDrag;
    [SerializeField] float _airDrag;
    [Space(16f)]
    [SerializeField] LayerMask _characterLayerMask;
    [Space(16f)]
    [SerializeField] float _jumpSpeed;
    [SerializeField] float _jumpCooldown;

    [HideInInspector] public bool isMoving;
    Vector3 _moveDirection;

    [HideInInspector] public bool isGrounded;
    bool _wasGrounded;
    [HideInInspector] public bool hasGrounded;

    [HideInInspector] public bool touchesCeiling;
    bool _touchedCeiling;
    [HideInInspector] public bool hasTouchedCeiling;

    [HideInInspector] public float speed;
    [HideInInspector] public speedTypeEnum speedType;

    Vector3 _horizontalVelocity;
    float _verticalVelocity;

    bool canJump;
    [HideInInspector] public bool hasJumped;

    [HideInInspector] public float landingVelocity;

    [HideInInspector] public Vector3 velocity;

    InputAction _moveAction;

    InputAction _sprintAction;
    InputAction _crouchAction;

    InputAction _jumpAction;

    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");

        _sprintAction = InputSystem.actions.FindAction("Sprint");
        _crouchAction = InputSystem.actions.FindAction("Crouch");

        _jumpAction = InputSystem.actions.FindAction("Jump");

        canJump = true;
    }

    void Update()
    {
        DoGroundCheck();
        DoCeilingCheck();

        ProcessMovementInput();

        CalculateSpeed();

        CalculateHorizontalVelocity();
        CalculateVerticalVelocity();

        if (_jumpAction.WasPressedThisFrame() && canJump && _characterStaminaControl.canJump && isGrounded && _characterController.height == _characterHeightControl.normalHeight)
        {
            Jump();
        }
        else
        {
            hasJumped = false;
        }

        ComposeVelocity();

        MoveCharacter();
    }

    void ProcessMovementInput()
    {
        isMoving = _moveAction.ReadValue<Vector2>().sqrMagnitude != 0f && isGrounded;

        if (isMoving)
        {
            if (speed == 0f)
            {
                _moveDirection = _character.transform.forward * _moveAction.ReadValue<Vector2>().y + _character.transform.right * _moveAction.ReadValue<Vector2>().x;
            }
            else
            {
                _moveDirection = Vector3.Lerp(_moveDirection, _character.transform.forward * _moveAction.ReadValue<Vector2>().y + _character.transform.right * _moveAction.ReadValue<Vector2>().x, _angularAcceleration * Time.deltaTime);
            }

            if (_moveDirection.sqrMagnitude > 1f)
            {
                _moveDirection.Normalize();
            }
        }
    }

    void DoGroundCheck()
    {
        _wasGrounded = isGrounded;

        isGrounded = Physics.CheckSphere(_character.transform.position + Vector3.up * _characterController.radius * 0.45f, _characterController.radius * 0.95f, ~_characterLayerMask);

        hasGrounded = !_wasGrounded && isGrounded;
    }

    void DoCeilingCheck()
    {
        _touchedCeiling = touchesCeiling;

        touchesCeiling = Physics.CheckSphere(_character.transform.position + Vector3.up * (_characterController.height - _characterController.radius * 0.45f), _characterController.radius * 0.95f, ~_characterLayerMask);

        hasTouchedCeiling = !_touchedCeiling && touchesCeiling;
    }

    void CalculateSpeed()
    {
        if (isGrounded)
        {
            if (isMoving)
            {
                if (_crouchAction.IsPressed())
                {
                    if (speed != _crouchSpeed)
                    {
                        speed = Mathf.Lerp(speed, _crouchSpeed, _acceleration * Time.deltaTime);

                        if (speed < _crouchSpeed + 0.01f)
                        {
                            speed = _crouchSpeed;
                        }
                    }
                }
                else if (_sprintAction.IsPressed() && _characterStaminaControl.canSprint)
                {
                    if (speed != _sprintSpeed)
                    {
                        speed = Mathf.Lerp(speed, _sprintSpeed, _acceleration * Time.deltaTime);

                        if (speed > _sprintSpeed - 0.01f)
                        {
                            speed = _sprintSpeed;
                        }
                    }
                }
                else
                {
                    if (speed != _walkSpeed)
                    {
                        speed = Mathf.Lerp(speed, _walkSpeed, _acceleration * Time.deltaTime);

                        if (speed > _walkSpeed - 0.01f && speed < _walkSpeed + 0.01f)
                        {
                            speed = _walkSpeed;
                        }
                    }
                }
            }
            else
            {
                if (speed != 0f)
                {
                    speed = Mathf.Lerp(speed, 0f, _groundDrag * Time.deltaTime);

                    if (speed < 0.01f)
                    {
                        speed = 0f;
                    }
                }
            }
        }
        else
        {
            if (speed != 0f)
                {
                    speed = Mathf.Lerp(speed, 0f, _airDrag * Time.deltaTime);

                    if (speed < 0.01f)
                    {
                        speed = 0f;
                    }
                }
        }

        if (speed == 0f)
        {
            speedType = speedTypeEnum.None;
        }
        else
        {
            if (_crouchAction.IsPressed())
            {
                speedType = speedTypeEnum.Crouch;
            }
            else if (_sprintAction.IsPressed() && _characterStaminaControl.canSprint)
            {
                speedType = speedTypeEnum.Sprint;
            }
            else
            {
                speedType = speedTypeEnum.Walk;
            }
        }
    }

    void CalculateHorizontalVelocity()
    {
        _horizontalVelocity = _moveDirection * speed;
    }

    void CalculateVerticalVelocity()
    {
        if (hasGrounded)
        {
            landingVelocity = _verticalVelocity;

            _characterSFX.PlayLandingSound();
        }

        if (!isGrounded)
        {
            _verticalVelocity -= 9.81f * Time.deltaTime;
        }
        else if (_verticalVelocity < 0f)
        {
            _verticalVelocity = 0f;
        }

        if (hasTouchedCeiling)
        {
            _verticalVelocity = 0f;
        }
    }

    void Jump()
    {
        canJump = false;

        _characterSFX.PlayJumpSound();

        _verticalVelocity = _jumpSpeed;

        hasJumped = true;

        Invoke(nameof(ResetJumpCooldown), _jumpCooldown);
    }

    void ComposeVelocity()
    {
        velocity = _horizontalVelocity + Vector3.up * _verticalVelocity;
    }

    void MoveCharacter()
    {
        _characterController.Move(velocity * Time.deltaTime);
    }
    void ResetJumpCooldown()
    {
        canJump = true;
    }
}
