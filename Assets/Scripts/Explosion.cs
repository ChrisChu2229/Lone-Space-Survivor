using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Update is called once per frame
    void Start()
    {
        Destroy(gameObject, 2.4f);
    }
}
