using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{

    public float lifetime = 100f;

    // Update is called once per frame
    void Update()
    {
        lifetime -= 1f;
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
