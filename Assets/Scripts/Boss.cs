using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private GameObject player;
    public GameObject boss;
    public GameObject mprojectilePrefab;
    private Animator animator;
    private float BossHealth = 250f;
    private bool dead = false;
    private float bossdeathtime = 3f;
    Rigidbody rb;
    //public Transform pointA;
    //public Transform pointB;
    private float bosspeed = 1f;
    


    // Start is called before the first frame update
    void Start()
    {
        
        InvokeRepeating("ShootProjectile", 1f, 2f);
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
            if (bossdeathtime<= 0)
            {
                
                Destroy(this.gameObject);
            }
        }
        float x;
        if(!dead)
       transform.position = Vector3.Lerp(new Vector3(-20f,0f,6f),new Vector3(20f,0f,6f), 0.5f*Mathf.Sin(Time.time * bosspeed) + 0.5f);

        

    }

    void ShootProjectile()
    {
        if (player == null)
            return;

        var p = Instantiate(mprojectilePrefab, (boss.transform.position + new Vector3(0f, 5f, 0f)), Quaternion.identity);
        var aimDirection = Vector3.Normalize(player.transform.position - (boss.transform.position + new Vector3(0f, 5f, 0f)));
        p.GetComponent<Projectile>().Direction = aimDirection;


    }

     public void TakeDamage()
    {if (dead)
            return;

        if (BossHealth > 0)
        {
            BossHealth -= GameController.instance.bossDamage;
            Debug.Log("BossHealth: "+ BossHealth);
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
}
