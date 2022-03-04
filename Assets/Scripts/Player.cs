using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private float _canFire = -1f;
    [SerializeField]
    private int _health = 3;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isSpeedBoostActive = false;
    [SerializeField]
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _shields;
    [SerializeField]
    private int _score;


    private SpawnManager _spawnManager;
    private UIManager _uiManager;


    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL in Player.");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL in Player.");
        }
    }

    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }

        
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        if (_isSpeedBoostActive)
        {
            transform.Translate(direction * _speed * 3 * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), 0);


        if (transform.position.x >= 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.5f)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        // This adds our fire rate to the current Time of the frame and assign the value to _canFire
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }

    public void Damage()
    {

        if (_isShieldActive)
        {
            _isShieldActive = false;
            _shields.gameObject.SetActive(false);
            return;
        }

        // check if shield active
        // do nothing
        // deactive shield
        // return
        _health -= 1;

        _uiManager.updateLives(_health);

        if (_health < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void turnOnTripleShot()
    {
        _isTripleShotActive = true;
        StartCoroutine(turnOffTripleShot());
    }

    IEnumerator turnOffTripleShot()
    {
        yield return new WaitForSeconds(5f);
        _isTripleShotActive = false; 
    }

    public void turnOnSpeedBoost()
    {
        _isSpeedBoostActive = true;
        StartCoroutine(turnOffSpeedBoost());
    }

    IEnumerator turnOffSpeedBoost()
    {
        yield return new WaitForSeconds(5f);
        _isSpeedBoostActive = false;
    }

    public void turnOnShields()
    {
        _isShieldActive = true;
        _shields.gameObject.SetActive(true);
        StartCoroutine(turnOffShields());
    }

    IEnumerator turnOffShields()
    {
        yield return new WaitForSeconds(7f);
        _isShieldActive = false;
        _shields.gameObject.SetActive(false);
    }

    public void updateScore(int points)
    {
        _score += points;
        _uiManager.updateScoreText(_score);

    }


}
