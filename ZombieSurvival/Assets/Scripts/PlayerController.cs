using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed = 10.0f;
    public int ammo = 7;
    public int health = 100;
    public int damage = 10;
    public float shootSpeed = .25f;

    public AudioClip gunshot;
    public AudioClip reload;
    public AudioClip emptyClip;
    public AudioClip playerHit;

    private AudioSource _audioSource;
    private bool hasShot = false; 

    public GameManager gameManager;

    void Start()
    {
        //locks cursor to game window
        Cursor.lockState = CursorLockMode.Locked;
        GameManager gameManager = GetComponent<GameManager>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float translation = Input.GetAxis("Vertical") * speed;
        float strafe = Input.GetAxis("Horizontal") * speed;
        translation *= Time.deltaTime;
        strafe *= Time.deltaTime;
        shootSpeed -= Time.deltaTime;
        if (shootSpeed < 0f)
        {
            hasShot = false;
        }

        transform.Translate(strafe, 0, translation);

        if (Input.GetKeyDown("escape"))
            Cursor.lockState = CursorLockMode.None;

        if (Input.GetMouseButtonDown(0))
        {
            if (ammo > 0 && hasShot == false)
            {
                _audioSource.PlayOneShot(gunshot, 0.5f);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                // Raycast
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {

                    if (hit.transform.gameObject.tag == "Enemy")
                    {
                        hit.collider.gameObject.GetComponent<EnemyController>().takeDamage(damage);
                        Debug.Log("Enemy Hit");

                    }

                }
                hasShot = true;
                shootSpeed = 1f;
                ammo--;
                gameManager.updateAmmo();
            }

            if (ammo <= 0 && hasShot == false)
            {
                GetComponent<AudioSource>().PlayOneShot(emptyClip, 1);
            }

        }

        if (Input.GetKeyDown("r"))
        {
            _audioSource.PlayOneShot(reload, 0.5f);
            ammo = 7;
            gameManager.updateAmmo();
        }
    }

    public void takeDamage(int damage)
    {
        _audioSource.PlayOneShot(playerHit, 1);
        health -= damage;
        gameManager.updateHealth();
        //Debug.Log("Zombie Hit: " + health + " left");
        if (health <= 0)
        {
            Debug.Log("Player died");
            death();
        }
    }

    public void death()
    {
        //GetComponent<AudioSource>().Stop();
        //GetComponent<AudioSource>().PlayOneShot(deathSound, 1);
        //Destroy(this.gameObject, deathSound.length);
        Destroy(this.gameObject);
    }
}