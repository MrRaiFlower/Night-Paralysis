using TMPro;
using UnityEngine;

public class StatsOverlay : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] CharacterStamina _characterStamina;
    [SerializeField] CharacterFlashlightControl _characterFlashlightControl;
    [Space(16.0f)]
    [SerializeField] TMP_Text _staminaText;
    [SerializeField] TMP_Text _flashlightChargeText;

    void Start()
    {
        _staminaText.text = "Stamina: " + Mathf.Round(_characterStamina.stamina);
        _staminaText.text = "Flashlight charge: " + Mathf.Round(_characterFlashlightControl.charge);
    }

    void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        _staminaText.text = "Stamina: " + Mathf.Round(_characterStamina.stamina);
        _flashlightChargeText.text = "Flashlight charge: " + Mathf.Round(_characterFlashlightControl.charge);
    }
}
