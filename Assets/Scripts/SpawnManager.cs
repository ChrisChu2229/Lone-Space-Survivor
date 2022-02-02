using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemyPrefab;
    [SerializeField]
    private float _spawnSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }


    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            float randomX = Random.Range(-8f, 8f);
            Vector3 enemyPos = new Vector3(randomX, 7, 0);
            Instantiate(_enemyPrefab, enemyPos, Quaternion.identity);
            yield return new WaitForSeconds(_spawnSpeed);
        }
    }
}
