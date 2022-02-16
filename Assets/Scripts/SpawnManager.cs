using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _spawnSpeed = 5f;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerupList;
    [SerializeField]
    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }


    private IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-5f, 5f);
            Vector3 enemyPos = new Vector3(randomX, 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, enemyPos, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(_spawnSpeed);
        }
    }

    private IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            float randomX = Random.Range(-5f, 5f);
            Vector3 powerupPos = new Vector3(randomX, 7, 0);
            int randomTime = Random.Range(3, 8);
            int randomPowerup = Random.Range(0, 3);
            Instantiate(_powerupList[randomPowerup], powerupPos, Quaternion.identity);
            yield return new WaitForSeconds(randomTime);
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
