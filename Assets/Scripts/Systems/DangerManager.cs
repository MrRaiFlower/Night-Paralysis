using UnityEngine;

public class DangerManager : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject[] _windows;
    [SerializeField] GameObject _entranceDoor;
    [SerializeField] GameObject[] _vents;
    [Space(16.0f)]
    [SerializeField] GameOverManager _gameOverManager;

    [Header("Parameters")]
    [Space(16.0f)]
    [SerializeField] float _dangerUpdateInterval;

    bool _isGameOver;

    void Start()
    {
        InvokeRepeating(nameof(CheckDangers), 0f, _dangerUpdateInterval);
    }

    void Update()
    {

    }

    void CheckDangers()
    {
        int _dangerNumber = 0;

        foreach (GameObject _window in _windows)
        {
            if (_window.GetComponent<Window>().dangerStage == Window.dangerStageEnum.High)
            {
                _dangerNumber += 1;
            }
        }

        if (_entranceDoor.GetComponent<EntranceDoor>().dangerStage == EntranceDoor.dangerStageEnum.High)
        {
            _dangerNumber += 1;
        }

        foreach (GameObject _vent in _vents)
        {
            if (_vent.GetComponent<VentTunnel>().dangerStage == VentTunnel.dangerStageEnum.High)
            {
                _dangerNumber += 1;
            }
        }

        if (_dangerNumber != 0 && !_isGameOver)
        {
            if (Random.Range(0f, 4.0f) < _dangerNumber)
            {
                _isGameOver = true;

                _gameOverManager.DoGameOver();
            }
        }
    }
}
