using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3f;
    // 0 = triple shot
    // 1 = speed
    // 2 = shield
    [SerializeField]
    private int powerupID;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        
        if (transform.position.y <= -6)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                switch(powerupID)
                {
                    case 0:
                        player.turnOnTripleShot();
                        break;
                    case 1:
                        player.turnOnSpeedBoost();
                        break;
                    case 2:
                        player.turnOnShields();
                        break;
                    default:
                        break;
                }
                
            }
            Destroy(this.gameObject);
        }
    }
}
