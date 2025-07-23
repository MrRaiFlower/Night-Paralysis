using System.Collections;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [Header("References")]
    [Space(16.0f)]
    [SerializeField] GameObject _gameOverTint;
    [Space(16.0f)]
    [SerializeField] AudioSource _screamerSound;
    [SerializeField] AudioSource _noiseSound;

    void Start()
    {

    }

    void Update()
    {

    }

    public void DoGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        float _noiseVolume = _noiseSound.volume;

        _noiseSound.volume = 0f;

        _noiseSound.Play();

        for (int i = 0; i < 100; i++)
        {
            _noiseSound.volume += _noiseVolume / 100;

            yield return new WaitForSeconds(0.0075f);
        }

        _screamerSound.Play();
        
        _gameOverTint.GetComponent<Animator>().Play("Fade");
    }
}
