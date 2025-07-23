using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _corridorLight;
    [SerializeField] GameObject _closetLight;
    [SerializeField] GameObject _kitchenLight;
    [SerializeField] GameObject _livingRoomLight;
    [SerializeField] GameObject _bathroomLight;
    [SerializeField] GameObject _bedroomLight;
    [SerializeField] GameObject _balconyLight;

    [HideInInspector]
    public Dictionary<string, bool> lightStates = new Dictionary<string, bool>()
    {
        {"Corridor", false},
        {"Closet", false},
        {"Bathroom", false},
        {"Kitchen", false},
        {"Living Room", false},
        {"Bedroom", false},
        {"Balcony", false},
    };

    void Start()
    {

    }

    void Update()
    {
        UpdateLightStates();
    }

    void UpdateLightStates()
    {
        lightStates["Corridor"] = _corridorLight.GetComponent<LightSwitch>().isOn;
        lightStates["Closet"] = _closetLight.GetComponent<LightSwitch>().isOn;
        lightStates["Bathroom"] = _bathroomLight.GetComponent<LightSwitch>().isOn;
        lightStates["Kitchen"] = _kitchenLight.GetComponent<LightSwitch>().isOn;
        lightStates["Living Room"] = _livingRoomLight.GetComponent<LightSwitch>().isOn;
        lightStates["Bedroom"] = _bedroomLight.GetComponent<LightSwitch>().isOn;
        lightStates["Balcony"] = _balconyLight.GetComponent<LightSwitch>().isOn;
    }

    public void SwitchRandomLight()
    {
        switch (Random.Range(0, 7))
        {
            case 0: _corridorLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 1: _closetLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 2: _bathroomLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 3: _kitchenLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 4: _livingRoomLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 5: _bedroomLight.GetComponent<LightSwitch>().SwitchOff(); break;
            case 6: _balconyLight.GetComponent<LightSwitch>().SwitchOff(); break;
        }
    }
}
