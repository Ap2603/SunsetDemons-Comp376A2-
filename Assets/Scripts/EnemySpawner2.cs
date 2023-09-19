using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner2 : MonoBehaviour
{
    [SerializeField]
    public GameObject mEnemyPrefab;

    [SerializeField]
    public GameObject mWitchPrefab;

    [SerializeField]
    public GameObject mBossPrefab;

    [SerializeField]
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public GameObject spawner4;
    public GameObject spawner5;
    public GameObject spawner6;
    public GameObject spawner7;
    public GameObject spawner8;
    public GameObject spawner9;
    public GameObject spawner10;

    public float maxX = 5f;
    public float maxY = 5f;
    public float enemytime = 5f;
    public float availabletime = 3f;
    private float random;

    private float enemyCap = 50f;
    private Vector3 pos;
    private bool spawnboss = false;
    private float bosscount = 0;
    private bool spawnenemies = true;
    private GameObject GameManager;
    private float bossdelay = 20f;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //SpawnWitch();
        InvokeRepeating("WitchRandomizer", 1f, 2f);
        InvokeRepeating("Randomnumgen", 0f, 1f);
        InvokeRepeating("SpawnEnemies", 0f, 3f);
        InvokeRepeating("SpawnBoss", 0f, 4f);
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");

    }


    // Update is called once per frame
    void Update()
    {
        if (enemyCap <= 0 && bosscount <= 0)
        {
            spawnboss = true;
            spawnenemies = false;
        }
    }

    public void SpawnEnemies()
    {
        if (spawnenemies)
        {
            for (int i = 0; i < random; i++)
            {

                float rand = Random.Range(1, 6);
                float randz = Random.Range(-5, -20);

                if (rand == 1)
                {
                    pos = spawner1.transform.position;
                }
                else if (rand == 2)
                {
                    pos = spawner2.transform.position;
                }
                else if (rand == 3)
                {
                    pos = spawner3.transform.position;
                }
                else if (rand == 4)
                {
                    pos = spawner4.transform.position;
                }
                else if (rand == 5)
                {
                    pos = spawner5.transform.position;
                }
                else
                {
                    pos = spawner6.transform.position;
                }



                Instantiate(mEnemyPrefab, pos, Quaternion.identity);
                enemyCap--;
                

            }



        }
    }

    public void SpawnWitch()
    {
        if (spawnenemies)
        {
            Vector3 witchpos = new Vector3(-12, 8, -15);

            GameObject witch = Instantiate(mWitchPrefab, witchpos, Quaternion.identity);
            witch.transform.Rotate(0f, 90f, 0f, Space.Self);
            witch.GetComponent<Rigidbody>().AddForce(new Vector2(2f, 0f), ForceMode.Impulse);
        }
    }

    public void WitchRandomizer()
    {
        float witchnum = Random.Range(1, 10);

        if (witchnum == 3f)
        {
            SpawnWitch();
        }
    }

    void Randomnumgen()
    {
        random = Random.Range(1f, 5f);
        //Debug.Log(random);
    }

    public void SpawnBoss()
    {

        if (spawnboss)
        {

            InvokeRepeating("bosstimer", 0f, 1f);
            if (bossdelay <= 0)
            {
                GameObject boss = Instantiate(mBossPrefab, new Vector3(0f, 0f, 6f), Quaternion.identity);
                spawnboss = false;
                bosscount++;
                GameManager.GetComponent<Timer>().timeRemaining = 181f;
                player.GetComponent<Player>().h = 225;
                GameManager.GetComponent<GameController2>().Heart1.enabled = true;
                GameManager.GetComponent<GameController2>().Heart2.enabled = true;
                GameManager.GetComponent<GameController2>().Heart3.enabled = true;


            }
        }

    }

    void bosstimer()
    {
        if (bossdelay <= 0)
            return;

        bossdelay--;
        Debug.Log("Boss Delay:" + bossdelay);
    }

}

