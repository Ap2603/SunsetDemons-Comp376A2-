using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float bulletspeed = 100f;

    public Vector3 Direction = Vector3.zero;
    public float visible = 5f;
    public int score;
    public GameObject Broom;
    private GameObject Player;
    private bool continuousshooting;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * bulletspeed * Time.deltaTime;
        visible -= Time.deltaTime;

        if (visible <= 0)
        {
            Destroy(this.gameObject);
        }
        if (Player != null)
        continuousshooting = Player.GetComponent<Player>().getContinuousshooting();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit!");            
            other.GetComponent<Enemy>().TakeDamage();
            Destroy(this.gameObject);
            Player.GetComponent<Player>().enemieskilled += 1;
            if (continuousshooting)
            {
                Player.GetComponent<Player>().enemieskilled = 0;
            }
            Debug.Log(Player.GetComponent<Player>().enemieskilled);

        }
        if (other.gameObject.tag == "Witch")
        {
            Debug.Log("Witch Hit!");
            other.GetComponent<witch>().TakeDamage();
            Destroy(this.gameObject);
            Player.GetComponent<Player>().enemieskilled += 1;
            if (continuousshooting)
            {
                Player.GetComponent<Player>().enemieskilled = 0;
            }

        }
        if (other.gameObject.tag == "Boss")
        {
            Debug.Log("Hit!");
            other.GetComponent<Boss>().TakeDamage();
            Destroy(this.gameObject);

        }

        if (other.gameObject.tag == "Boss2")
        {
            if (other.GetComponent<BossLvl2>().getbossStop() == 3)
            {
                Debug.Log("Hit!");
                other.GetComponent<BossLvl2>().TakeDamage();
            }
            Destroy(this.gameObject);

        }

    }
}
