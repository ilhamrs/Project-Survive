using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    //kecepatan objek
    public float speed = 80.0f;
    //durasi objek
    public float lifeDuration = 3f;
    float lifeTimer;
    // Start is called before the first frame update
    void Start()
    {
        lifeTimer = lifeDuration;
    }

    // Update is called once per frame
    void Update()
    {
        //objek bergerak maju
        transform.position += transform.forward * speed * Time.deltaTime;

        //jika durasi sudah habis, objek akan didestroy
        lifeTimer -= Time.deltaTime;
        if(lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
