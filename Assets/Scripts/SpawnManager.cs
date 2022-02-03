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

    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }



    private IEnumerator SpawnRoutine()
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

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
