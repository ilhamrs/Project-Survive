using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    public float lifeDuration = 20f;
    float lifeTimer;

    private PlayerFPS playerFPSScript;
    
    // Start is called before the first frame update
    void Start()
    {
        //zombieAnim = GetComponent<Animator>();
        lifeTimer = lifeDuration;

        playerFPSScript = GameObject.Find("TestPlayer").GetComponent<PlayerFPS>();
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //jika karakter zombie tertembak, objek akan didestroy
        //Destroy(gameObject);
        //zombieAnim.SetBool("isDeath", true);

        if(other.tag == "Zombie")
        {
            //Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else if(other.tag == "Human")
        {
            //Destroy(other.gameObject);
            playerFPSScript.healthPoint -= 20;
            Destroy(gameObject);
        }
        

    }
}
