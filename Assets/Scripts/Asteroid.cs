using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1.5f;
    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;



    void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<SpawnManager>();
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL in Asteroid");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rotation = new Vector3(0, 0, 45);
        transform.Rotate(rotation * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            Instantiate(_explosion, this.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.15f);

        }
    }
}
