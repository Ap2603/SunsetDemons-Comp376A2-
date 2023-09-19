using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float availabletime = 10f;
    public bool destroy = false;
    public int miss = 0;
    public GameObject mPlayer;
    UnityEngine.AI.NavMeshAgent agent;
    private float enemyhealth = 25f;
    Animator animator;
    public float enemydeathtime = 3f;
    private bool dead = false;
    private bool running = false;
    public Material redmat;
    public Material originalMat;
    private SkinnedMeshRenderer renderer;
    private bool flickeron = false;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        renderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        rb = transform.GetComponent<Rigidbody>();
        
    }

    void Start()
    {
        InvokeRepeating("EnemyTimer", 0f, 1f);
        animator = GetComponentInChildren<Animator>();
        animator.SetFloat("EnemyHealth", enemyhealth);
        animator.SetBool("IsRunning", running);

    }

    // Update is called once per frame
    void Update()
    {
        if (flickeron)
        {
            Flicker();
        }
        if (agent != null && mPlayer != null)
        {
            running = true;
            animator.Play("Running");            
            agent.SetDestination(mPlayer.transform.position);
        }
        if (enemyhealth <= 0)
        {
            Death();
        }

        if (dead)
        {
            enemydeathtime -= Time.deltaTime;
            if (enemydeathtime <= 0)
            {                
                Destroy(this.gameObject);

                
            }
        }
    }

    public int getMiss()
    {
        return miss;
    }

    public void EnemyTimer()
    {
        availabletime--;
        if (availabletime == 5)
        {
            flickeron = true;
        }
        if (availabletime <= 0)
        {
            dead = true;            
            if (mPlayer != null)
            mPlayer.GetComponent<Player>().TakeDamage();
            Death();
            Debug.Log("Enemy Deleted");
        }
    }

    public void TakeDamage()
    {
        if (dead)
            return;
        enemyhealth -= 25;

        if (GameController.instance != null)
            GameController.instance.incrementscoreEnemy();

        else
            GameController2.instance.incrementscoreEnemy();
        //animator.SetFloat("EnemyHealth", enemyhealth);
    }

    public void Death()
    {
        flickeron = false;
        renderer.material = originalMat;
        rb.isKinematic = true;
        animator.Play("Death");
        dead = true;
        Destroy(transform.GetComponent<BoxCollider>());
        Destroy(agent);
    }

    public void DestroyEnemy()
    {
        enemydeathtime--;
    }

    void Flicker()
    {
       
        if (Mathf.Sin(Time.time * 10) < 0)
        renderer.material = originalMat;
        else
        renderer.material = redmat;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Death();
        }
    }

}


