using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ballshoot : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 100f;
    void Start()
    {
        Destroy(gameObject, 6f);

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed*Time.deltaTime);
    }
}
