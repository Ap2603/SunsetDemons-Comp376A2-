using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvl2 : MonoBehaviour
{
    private GameObject player;
    public GameObject boss;
    public GameObject mprojectilePrefab;
    private Animator animator;
    public GameObject enemyprefab;
    private float BossHealth = 250f;
    private bool dead = false;
    private float bossdeathtime = 3f;
    Rigidbody rb;
    //public Transform pointA;
    //public Transform pointB;
    private float bosspeed = 1f;
    private float rand;
    private Vector3 pos;
    private bool spawnedonce = false;
    private bool cantshoot = false;
    private bool stopped = false;
    private float stopseconds = 10f;
    public float t = 3f;


    // Start is called before the first frame update
    void Start()
    {

        InvokeRepeating("ShootProjectile", 1f, 4f);
        InvokeRepeating("bossstopping", 3f, 6f);
        animator = transform.GetComponentInChildren<Animator>();
        rb = transform.GetComponent<Rigidbody>();
        animator.SetFloat("BossHealth", BossHealth);
        player = GameObject.FindGameObjectWithTag("PlayerBody");
        

    }

    // Update is called once per frame
    void Update()
    {
        if (BossHealth <= 0)
        {
            Death();
        }

        if (dead)
        {
            bossdeathtime -= Time.deltaTime;
            if (bossdeathtime <= 0)
            {

                Destroy(this.gameObject);
            }
        }
        float x;
        if (!dead && rand != 3)
            transform.position = Vector3.Lerp(new Vector3(-20f, 0f, 6f), new Vector3(20f, 0f, 6f), 0.5f * Mathf.Sin(Time.time * bosspeed) + 0.5f);

        if (rand == 3)
        {
            if (!spawnedonce)
            spawnEnemies();
            cantshoot = true;
        }

    }

    void ShootProjectile()
    {
        if (player == null)
            return;

        if (!cantshoot)
        {
            var p = Instantiate(mprojectilePrefab, (boss.transform.position + new Vector3(0f, 5f, 0f)), Quaternion.identity);
            var aimDirection = Vector3.Normalize(player.transform.position - (boss.transform.position + new Vector3(0f, 5f, 0f)));
            p.GetComponent<Projectile>().Direction = aimDirection;
        }


    }

    public void TakeDamage()
    {
        if (dead)
            return;

        if (BossHealth > 0)
        {
            BossHealth -= GameController2.instance.bossDamage;
            Debug.Log("BossHealth: " + BossHealth);
        }
    }

    void Death()
    {
        dead = true;
        rb.isKinematic = true;
        animator.Play("Death");
    }

    public bool isdead()
    {
        return dead;
    }
    public float getHealth()
    {
        return BossHealth;
    }

    IEnumerator bossStop()
    {
        Debug.Log("Test: " + t);
        rand = Random.Range(1, 5);
        Debug.Log(rand);
        if (rand == 3)
        {
            stopped = true;
            setTest(7f);
            
        }
        if (rand != 3)
        {
            spawnedonce = false;
            cantshoot = false;
            setTest(3f);
        }

        yield return new WaitForSeconds(t);

    }

    void bossstopping()
    {
        rand = Random.Range(1, 5);
        Debug.Log(rand);
        if (rand == 3)
        {
            stopped = true;
            setTest(7f);

        }
        if (rand != 3)
        {
            spawnedonce = false;
            cantshoot = false;
            setTest(3f);
        }
    }

    IEnumerator testing()
    {
        Debug.Log("testing");

            yield return new WaitForSeconds(0.5f);
    }
    public float getbossStop()
    {
        return rand;
    }

    void spawnEnemies()
    {
        float v = Random.Range(3, 5);
        for (int i = 0; i <=v; i++)
        {
            pos = boss.transform.position;

            Instantiate(enemyprefab, pos, Quaternion.identity);
        }
        spawnedonce = true;
    }

    void stoptime()
    {
        stopseconds--;

        if (stopseconds <= 0)
        {
            stopped = false;
            stopseconds = 10;
        }
    }
    void setTest(float x)
    {
        t = x;
        
    }
    void trying()
    {
        StartCoroutine(bossStop());
    }
}
