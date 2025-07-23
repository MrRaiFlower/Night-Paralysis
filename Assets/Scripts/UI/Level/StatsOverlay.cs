using TMPro;
using UnityEngine;

public class StatsOverlay : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] CharacterStamina _characterStamina;
    [SerializeField] CharacterFlashlightControl _characterFlashlightControl;
    [SerializeField] SanityManager _sanityManager;
    [SerializeField] LightsPanel _lightsPanel;
    [Space(16.0f)]
    [SerializeField] TMP_Text _staminaText;
    [SerializeField] TMP_Text _flashlightChargeText;
    [SerializeField] TMP_Text _sanityText;
    [SerializeField] TMP_Text _sanityDrainText;
    [SerializeField] TMP_Text _lightsPowerText;
    [SerializeField] TMP_Text _lightsDrainText;

    void Start()
    {
        _staminaText.text = "Stamina: " + Mathf.Round(_characterStamina.stamina);
        _staminaText.text = "Flashlight charge: " + Mathf.Round(_characterFlashlightControl.charge);
        _sanityText.text = "Sanity: " + Mathf.Round(_characterFlashlightControl.charge);
        _sanityDrainText.text = ("Sanity Drain: " + Mathf.Abs(_sanityManager.sanityDelta)).Replace(",", ".");
        _lightsPowerText.text = "Power: " + Mathf.Round(_lightsPanel.power);
        _lightsDrainText.text = ("Power Drain: " + _lightsPanel.currentPowerDrain).Replace(",", ".");
    }

    void Update()
    {
        UpdateStats();
    }

    void UpdateStats()
    {
        _staminaText.text = "Stamina: " + Mathf.Round(_characterStamina.stamina);
        _flashlightChargeText.text = "Flashlight charge: " + Mathf.Round(_characterFlashlightControl.charge);
        _sanityText.text = "Sanity: " + Mathf.Round(_sanityManager.sanity);
        _sanityDrainText.text = ("Sanity Drain: " + Mathf.Abs(_sanityManager.sanityDelta)).Replace(",", ".");
        _lightsPowerText.text = "Power: " + Mathf.Round(_lightsPanel.power);
        _lightsDrainText.text = ("Power Drain: " + _lightsPanel.currentPowerDrain).Replace(",", ".");
    }
}
