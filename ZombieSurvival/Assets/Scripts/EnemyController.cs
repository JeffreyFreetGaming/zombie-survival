using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public float lookRadius = 10f;
    public int health = 30;
    public int damage = 10;

    //public AudioClip deathSound;
    //public AudioClip deathSound2;
    public float attackSpeed = 0;

    public Transform target;
    NavMeshAgent agent;

    public GameManager gameManager;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //gameManager = gameObject.GetComponent<GameManager>();
    }

    void Update()
    {

        float distance = Vector3.Distance(target.position, transform.position);
        attackSpeed -= Time.deltaTime;

        // Move towards the player
        agent.SetDestination(target.position);
        if (distance <= agent.stoppingDistance)
        {
            FaceTarget();
        }

        RaycastHit hit;
        Vector3 fwd = this.transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(this.transform.position, fwd * 1, Color.green);
        if (Physics.Raycast(this.transform.position, fwd, out hit, 1))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                if (attackSpeed <= 0)
                {
                    hit.collider.gameObject.GetComponent<PlayerController>().takeDamage(damage);
                    attackSpeed = 2.0f;
                }
            }
        }
    }

    // Point towards the player
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void takeDamage(int damage)
    {
        //GetComponent<AudioSource>().PlayOneShot(deathSound, 1);
        health -= damage;
        Debug.Log("Zombie Hit: " + health + " left");
        if (health <= 0)
        {
            Debug.Log("enemy Died");
            gameManager.updateScore();
            death();
        }
    }

    public void death()
    {
        agent.isStopped = true;
        //GetComponent<AudioSource>().Stop();
        //GetComponent<AudioSource>().PlayOneShot(deathSound2, 1);
        //Destroy(this.gameObject, deathSound2.length);
        Destroy(this.gameObject);
    }

}