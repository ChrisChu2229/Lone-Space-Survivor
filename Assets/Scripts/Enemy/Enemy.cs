using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    private Player _player;
    Animator _enemyDeathAnimation;
    private bool _isAlive = true;
    [SerializeField]
    private GameObject _laserPrefab;
    private float _fireRate = 3.0f;
    private float _canFire = -1;

    private BoxCollider2D _boxCollider;

    private AudioSource _explosionAudioSource;





    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _enemyDeathAnimation = GetComponent<Animator>();
        _explosionAudioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        if (_player == null)
        {
            Debug.LogError("Player is NULL in Enemy.");
        }

        if (_enemyDeathAnimation == null)
        {
            Debug.LogError("Animator is NULL in Enemy");
        }

        if (_explosionAudioSource == null)
        {
            Debug.LogError("Explosion Audio Source is NULL in Enemy.");
        }

        if (_boxCollider == null)
        {
            Debug.LogError("Box Collider is NULL in Enemy.");
        }


    }



    void Update()
    {
        CalculateMovement();
        
        if (Time.time > _canFire && _isAlive)
        {
            _fireRate = Random.Range(2f, 7f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position - new Vector3(0, 1, 0), Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }
            
        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -6)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }
            DeadEnemy();
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.updateScore(10);

            }
            DeadEnemy();
        }
        
    }

    private void DeadEnemy()
    {
        _isAlive = false;
        _speed = 0;
        _enemyDeathAnimation.SetTrigger("OnEnemyDeath");
        _explosionAudioSource.Play();
        _boxCollider.enabled = false;
        Destroy(this.gameObject, _explosionAudioSource.clip.length);
    }


}

