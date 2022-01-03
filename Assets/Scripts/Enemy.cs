using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody enemyRb;
    private GameObject player;
    private Animator zombieAnim;
    public float speed;
    Transform target;
    private float outOfBounds = 24f;
    bool isDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("TestPlayer");
        target = player.GetComponent<Transform>();
        zombieAnim = GetComponent<Animator>();
        isDeath = false;
    }

    // Update is called once per frame
    void Update()
    {
        //enemyRb.AddForce((player.transform.position- transform.position).normalized * speed);
        Vector3 pos = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //enemyRb.MovePosition(pos);
        transform.LookAt(target);

        float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= 1 && gameObject.CompareTag("Zombie"))
        {
            zombieAnim.SetBool("isNear", true);
        }
        else
        {
            zombieAnim.SetBool("isNear", false);
            enemyRb.MovePosition(pos);
        }

        preventOutOfBounds();
        
    }

    void preventOutOfBounds()
    {
        Vector3 tempPos = transform.position;
        if (transform.position.x < -outOfBounds)
        {
            tempPos.x = -outOfBounds;
            transform.position = tempPos;
        }
        else if (transform.position.x > outOfBounds)
        {
            tempPos.x = outOfBounds;
            transform.position = tempPos;
        }

        if (transform.position.z < -outOfBounds)
        {
            tempPos.z = -outOfBounds;
            transform.position = tempPos;
        }
        else if (transform.position.z > outOfBounds)
        {
            tempPos.z = outOfBounds;
            transform.position = tempPos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            zombieAnim.SetBool("isDeath", true);
            speed = 0;
            isDeath = true;
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        yield return new WaitForSeconds(5); //waits 5 seconds
        Destroy(gameObject); //this will work after 5 seconds.
    }

}
