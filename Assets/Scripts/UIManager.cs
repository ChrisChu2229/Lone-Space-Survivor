using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _livesSprite;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _restartText;
    private GameManager _gameManager;

    // Framework
    [SerializeField]
    private Text _ammoCountText;

    void Start()
    {
        _scoreText.text = "Score: 0";
        _ammoCountText.text = "Ammo Count: 15";
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL in UIManager");
        }
    }


    public void updateScoreText(int score)
    {
        _scoreText.text = "Score: " + score;
    }

    public void updateAmmoCountText(int ammo)
    {
        _ammoCountText.text = "Ammo Count: " + ammo;
    }

    public void updateLives(int currentLives)
    {
        _livesImage.sprite = _livesSprite[currentLives];

        if (currentLives == 0)
        {
            gameOverSequence();
        }    
    }

    private void gameOverSequence()
    {
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(flickerGameOverText());
        _gameManager.gameOver();
    }

    private IEnumerator flickerGameOverText()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            _gameOverText.gameObject.SetActive(true);

        }
    }

}
