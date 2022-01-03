using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]

public class PlayerFPS : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public GameObject weapon;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public int healthPoint;
    public int ammo;
    public Text ammoText;
    public Text healthText;
    private float outOfBounds = 24f;

    public bool isGameOver;
    public GameObject gameOverMenu;
    public GameObject pauseMenu;
    //peluru
    public GameObject projectile;
    //posisi peluru spawn
    public GameObject spawnBullet;
    public float speed = 10;

    private AudioSource playerAudio;
    public AudioClip ammoSound;
    public AudioClip powerupSound;

    public ParticleSystem fire;

    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;

    public bool onPause;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        Resume();
        characterController = GetComponent<CharacterController>();

        playerAudio = GetComponent<AudioSource>();

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthPoint = 100;
        ammo = 120;

        healthText.text = healthPoint.ToString() + "%";
        ammoText.text = ammo.ToString()+"/120";

        isGameOver = false;
        gameOverMenu.SetActive(false);
        pauseMenu.SetActive(false);

        onPause = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && ammo > 0 && !isGameOver && !onPause)
        {
            Instantiate(projectile, spawnBullet.transform.position, spawnBullet.transform.rotation);
            ammo--;
            ammoText.text = ammo.ToString() + "/120";
            //fire.Play();
            FireParticle();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPause)
            {
                Resume();
            }
            else if (!onPause)
            {
                Pause();
                //onPause = true;
                //pauseMenu.SetActive(true);
            }
        }

        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isGameOver)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove && !isGameOver)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            
        }
        //jika health 0 atau kurang, game over
        if (healthPoint <= 0)
        {
            //isGameOver = true;
            //gameOverMenu.SetActive(true);
            Stop();
        }
        //mencegah player keluar play area
        preventOutOfBounds();
        healthText.text = healthPoint.ToString() + "%";
    }

    void FireParticle()
    {
        //fire.Play();
        //fire.Emit(2);
        Instantiate(fire, spawnBullet.transform.position, spawnBullet.transform.rotation);
    }

    public void Stop()
    {
        Time.timeScale = 0f; //Mathf.Approximately(Time.timeScale, 0.0f) ? 0.0f : 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canMove = false;
        onPause = true;
        isGameOver = true;
        gameOverMenu.SetActive(true);
        //pauseMenu.SetActive(true);
    }

    public void Pause()
    {
        Time.timeScale = 0f; //Mathf.Approximately(Time.timeScale, 0.0f) ? 0.0f : 0.0f;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        canMove = false;
        onPause = true;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1f; //Mathf.Approximately(Time.timeScale, 1.0f) ? 1.0f : 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
        pauseMenu.SetActive(false);
        onPause = false;
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
        if (other.CompareTag("Powerup"))
        {
            if (healthPoint > 50)
            {
                healthPoint = 100;
            }
            else
            {
                healthPoint += 50;
            }
            healthText.text = healthPoint.ToString() + "%";
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(powerupSound, 1f);
        }
        else if (other.CompareTag("Ammo"))
        {
            if (ammo > 90)
            {
                ammo = 120;
            }
            else
            {
                ammo += 30;
            }
            ammoText.text = ammo.ToString() + "/120";
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(ammoSound, 1f);
        }
        else if (other.CompareTag("Human"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Zombie"))
        {
            healthPoint--;
            healthText.text = healthPoint.ToString() + "%";
        }
    }
}
