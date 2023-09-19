using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController2 : MonoBehaviour
{
    public static GameController2 instance;

    public Text scoreText;
    public Text GameOverscoreText;
    public Text lvl2scoreText;
    public Text Finalscoretxt;
    public Image Heart1;
    public Image Heart2;
    public Image Heart3;
    public Image bHeart1;
    public Image bHeart2;
    public Image bHeart3;
    public Image bHeart4;
    public Image bHeart5;
    public float playerScore;
    private float playerhealth;
    public float enemyDamage = 25f;
    private GameObject Player;
    private GameObject spawner;
    private GameObject Boss;
    private GameObject GameManager;
    public GameObject[] Enemies;
    public GameObject[] witches;
    public GameObject gameOverScreen;
    public GameObject lvl2screen;
    public GameObject endscreen;
    private bool cs;
    public AudioSource BgMusic;
    public float bossD = 75f;
    private bool bossdead;
    private float bhealth;
    public float bossDamage = 10f;
    private bool cheat = true;

    private float EnemyScore;
    private float witchScore = 25f;
    private float bossScore = 100f;
    private bool haventadded = true;
    private bool timerisrunning;

    private GameObject lvl1Score;

    public GameObject bosshealthscreen;


    void Awake()
    {
        CreateInstance();
        Player = GameObject.FindGameObjectWithTag("Player");
        GameManager = GameObject.FindGameObjectWithTag("GameManager");
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        lvl1Score = GameObject.FindGameObjectWithTag("score");
        lvl2screen.SetActive(false);
        gameOverScreen.SetActive(false);


    }
    // Start is called before the first frame update
    void Start()
    {
        InitializeGameplayController();

        InvokeRepeating("whichScore", 0f, 0.1f);
        InvokeRepeating("checkTimer", 0f, 0.1f);
        InvokeRepeating("GetCurrentEnemies", 0f, 0.1f);
        InvokeRepeating("incrementscoreBoss", 0f, 0.1f);
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        UpdateGameplayController();
        monitorHealth();
        CheckHealth();
        CheckBossHealth();
        Boss = GameObject.FindGameObjectWithTag("Boss2");
        if (Boss != null)
            monitorBoss();

        if (!timerisrunning || playerhealth <= 0)
        {
            Debug.Log("GAME OVER");
            Destroy(Player);
            Destroy(spawner);
            Destroy(Boss);
            BgMusic.enabled = false;
            GameManager.GetComponent<Timer>().Timertxt.text = "00:00";
            foreach (var x in Enemies)
            {
                Destroy(x);
            }
            foreach (var x in witches)
            {
                Destroy(x);
            }
            gameOverScreen.SetActive(true);
            Cursor.visible = true;
        }

        if (bossdead)
        {
            Destroy(spawner);
            Destroy(Player);
            BgMusic.enabled = false;
            GameManager.GetComponent<Timer>().Timertxt.text = "00:00";
            foreach (var x in Enemies)
            {
                Destroy(x);
            }
            endscreen.SetActive(true);
            Cursor.visible = true;
        }

        if (Boss != null)
        {

            bosshealthscreen.SetActive(true);
            if (cheat)
            {
                bHeart1.enabled = true;
                bHeart2.enabled = true;
                bHeart3.enabled = true;
                bHeart4.enabled = true;
                bHeart5.enabled = true;
                cheat = false;
            }
        }


    }

    void CreateInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void InitializeGameplayController()
    {

        if (lvl1Score != null)
            playerScore = lvl1Score.GetComponent<keepscore>().getscore();
        else
            playerScore = 0;

        scoreText.text = "" + playerScore;
        GameOverscoreText.text = "" + playerScore;
        Finalscoretxt.text = "" + playerScore;


    }

    void UpdateGameplayController()
    {
        scoreText.text = "" + playerScore;
        GameOverscoreText.text = "" + playerScore;
        lvl2scoreText.text = "" + playerScore;
        Finalscoretxt.text = "" + playerScore;

    }

    void CheckHealth()
    {
        if (playerhealth <= 150)
        {
            Heart1.enabled = false;
        }
        if (playerhealth <= 75)
        {
            Heart2.enabled = false;
        }
        if (playerhealth <= 0)
        {
            Heart3.enabled = false;
        }
    }

    void CheckBossHealth()
    {
        if (bhealth <= 200)
        {
            bHeart1.enabled = false;
        }
        if (bhealth <= 150)
        {
            bHeart2.enabled = false;
        }
        if (bhealth <= 100)
        {
            bHeart3.enabled = false;
        }
        if (bhealth <= 50)
        {
            bHeart4.enabled = false;
        }
        if (bhealth <= 0)
        {
            bHeart5.enabled = false;
        }

    }

    void monitorHealth()
    {
        if (Player != null)
            playerhealth = Player.GetComponent<Player>().getHealth();
    }

    public void incrementscoreEnemy()
    {
        playerScore += EnemyScore;
    }

    public void incrementscoreWitch()
    {
        playerScore += witchScore;
    }

    public void incrementscoreBoss()
    {
        if (bossdead && haventadded)
        {
            playerScore += bossScore;
            haventadded = false;
        }
    }

    void whichScore()
    {
        if (Player != null)
            cs = Player.GetComponent<Player>().getContinuousshooting();

        if (cs)
        {
            EnemyScore = 1;
            enemyDamage = 37.5f;
            bossDamage = 2.5f;
        }
        else
        {
            EnemyScore = 3;
            enemyDamage = 25f;
            bossDamage = 10f;
        }
    }

    void checkTimer()
    {
        timerisrunning = GameManager.GetComponent<Timer>().timerIsRunning;
    }

    void GetCurrentEnemies()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        witches = GameObject.FindGameObjectsWithTag("Witch");
    }

    void monitorBoss()
    {
        if (Boss == null)
            return;

        bossdead = Boss.GetComponent<BossLvl2>().isdead();
        bhealth = Boss.GetComponent<BossLvl2>().getHealth();
        Debug.Log(bhealth);

    }
}

