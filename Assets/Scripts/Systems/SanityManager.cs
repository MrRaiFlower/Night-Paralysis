using UnityEngine;

public class SanityManager : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject[] _windows;
    [SerializeField] GameObject _entranceDoor;
    [SerializeField] GameObject[] _vents;
    [Space(16.0f)]
    [SerializeField] RoomManager _roomManager;
    [SerializeField] LightManager _lightManager;
    [SerializeField] LightsPanel _lightsPanel;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _baseSanityDecrement;
    [Space(16.0f)]
    [SerializeField] float _lightOffSanityDecrement;
    [Space(16.0f)]
    [SerializeField] float _dangerUpdatePeriod;
    [Space(16.0f)]
    [SerializeField] float _sanityEventUpdatePeriod;

    [HideInInspector] public float sanity;

    [HideInInspector] public float sanityDelta;

    void Start()
    {
        sanity = 100.0f;

        InvokeRepeating(nameof(UpdateDanger), 0f, _dangerUpdatePeriod);
        InvokeRepeating(nameof(UpdateSanityEvent), 0f, _sanityEventUpdatePeriod);
    }

    void Update()
    {
        UpdateSanityLevel();
    }

    void UpdateSanityLevel()
    {
        sanityDelta = -_baseSanityDecrement;

        bool _isInLight = false;

        if (_lightsPanel.isOn)
        {
            foreach (var _roomEntry in _roomManager.roomStates)
            {
                if (_roomEntry.Value && _lightManager.lightStates[_roomEntry.Key])
                {
                    _isInLight = true;

                    break;
                }
            }
        }

        if (!_isInLight)
        {
            sanityDelta -= _lightOffSanityDecrement;
        }

        sanity += sanityDelta * Time.deltaTime;

        sanity = Mathf.Clamp(sanity, 0f, 100.0f);
    }

    void UpdateDanger()
    {
        if (Random.Range(0f, 1.0f) >= sanity / 100.0f)
        {
            int _dangerID = Random.Range(0, 10);

            if (_dangerID < 5) // Windows - 5
            {
                _windows[Random.Range(0, 4)].GetComponent<Window>().IncreaseDangerStage();
            }
            else if (_dangerID < 7) // Entrance Door - 2
            {
                _entranceDoor.GetComponent<EntranceDoor>().IncreaseDangerStage();
            }
            else if (_dangerID < 10) // Vents - 3
            {
                _vents[Random.Range(0, 3)].GetComponent<VentTunnel>().IncreaseDangerStage();
            }
        }
    }

    void UpdateSanityEvent()
    {
        if (Random.Range(0f, 1.0f) >= sanity / 100.0f)
        {
            int _dangerID = Random.Range(0, 10);

            if (_dangerID < 10) // Random Light Switch - 10
            {
                _lightManager.SwitchRandomLight();
            }
        }
    }
}
