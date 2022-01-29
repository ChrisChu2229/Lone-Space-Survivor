using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 8, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -6)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 8, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.transform.name);
        // if other is player destroy us
        // damage the player
        // destroy us

        // if other is laser
        // laser
        // destroy us
    }
}
