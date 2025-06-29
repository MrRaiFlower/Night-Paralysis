using UnityEngine;

public class CharacterCameraLeaning : MonoBehaviour
{
    [Header("References")]
    [Space(16f)]
    [SerializeField] GameObject _camera;
    [Space(16f)]
    [SerializeField] CharacterMovementControl _characterMovementControl;

    [Header("Parameters")]
    [Space(16f)]
    [SerializeField] float leanAngle;
    [SerializeField] float leanAngleProportion;
    [SerializeField] float leanSpeed;

    float currentLeanAngle;

    void Start()
    {

    }

    void Update()
    {
        CalculateCameraLean();

        UpdateCameraLean();
    }

    void CalculateCameraLean()
    {
        currentLeanAngle = Mathf.Lerp(currentLeanAngle, -leanAngle * transform.InverseTransformDirection(_characterMovementControl.velocity).x / leanAngleProportion, leanSpeed * Time.deltaTime);
    }

    void UpdateCameraLean()
    {
        _camera.transform.localRotation = Quaternion.Euler(0f, 0f, currentLeanAngle);
    }
}
