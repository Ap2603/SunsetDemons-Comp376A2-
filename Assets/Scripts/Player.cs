using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float speed = 10;
    [SerializeField]
    Camera mainCam;
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject playergun;
    [SerializeField]
    GameObject mBulletPrefab;
    [SerializeField]
    private float range = 1000f;
    [SerializeField]
    private float jumpForce = 100f;

    private bool floored = false;

    public float enemieskilled = 0;

    public float h = 225;
    private bool cheat = true;

    private SkinnedMeshRenderer gunrenderer;

    private bool continuousshooting = false;    
    private float continuousshootingtime = 10f;
    Rigidbody mRigidBody;
    private AudioSource grunt;

    // Start is called before the first frame update
    void Start()
    {
        mRigidBody = transform.GetComponent<Rigidbody>();
        grunt = transform.GetComponent<AudioSource>();
        gunrenderer = playergun.GetComponent<SkinnedMeshRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(x, 0, 0);
        

        transform.position += movement * 5 * Time.deltaTime;

        RaycastHit hit1;
        Ray ray1 = mainCam.ScreenPointToRay(Input.mousePosition);
        Vector3 rayOrigin1 = mainCam.transform.position;
        if (Physics.Raycast(rayOrigin1, ray1.direction, out hit1, range))
        {
            
            var Direction = Vector3.Normalize(hit1.point - player.transform.position);
            var temp = new Vector3(0, Direction.y, 0);
            player.transform.rotation = Quaternion.LookRotation(Direction);




        }

        //Vector3 playerScreenPosition = mainCam.WorldToScreenPoint((player.transform.position));
        //Vector3 cursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerScreenPosition.z);
        //Vector3 mousePosition = mainCam.ScreenToWorldPoint(cursorPosition);
        //Vector3 aimDirection = Vector3.Normalize(mousePosition - player.transform.position);

        if (continuousshooting && continuousshootingtime >= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {

                Shoot();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {

                Shoot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && floored)
        {
            //Debug.Log("BURHHHHHH");
            mRigidBody.AddForce(Vector2.up * 10, ForceMode.Impulse);
            floored = false;
            
        }
        if (enemieskilled >= 10)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                continuousshooting = true;                
                //enemieskilled = 0;
                if (cheat)
                InvokeRepeating("Continuousshooting", 0f, 1f);
            }
        }
        
        if (h <= 0)
        {
            Death();
        }

        Debug.Log(floored);
        
    }

    public void Shoot()
    {
        RaycastHit hit;
        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        Vector3 rayOrigin = mainCam.transform.position;
        if (Physics.Raycast(rayOrigin, ray.direction, out hit, range))
        {
            if (hit.transform.tag != "Player")
            {
                var aimDirection = Vector3.Normalize(hit.point - gunrenderer.rootBone.transform.position);
                var b = Instantiate(mBulletPrefab, gunrenderer.rootBone.transform.position + aimDirection  , Quaternion.identity);                
                b.GetComponent<Bullet>().Direction = aimDirection;

                

            }
        }
        
    }

    public void TakeDamage()
    {
        if (h > 0)
        {
            if (GameController.instance != null)
            h -= GameController.instance.enemyDamage;

            else
                h -= GameController2.instance.enemyDamage;

            grunt.Play();
           
        }
       
        
           

    }

    public void TakeDamageFromBoss()
    {
        if (h > 0)
        {
            if (GameController.instance != null)
                h -= GameController.instance.bossD;

            else
                h -= GameController2.instance.bossD;
            grunt.Play();
            
        }




    }

    public float getHealth()
    {
        return h;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //mRigidBody.isKinematic = true;
            collision.gameObject.GetComponent<Enemy>().Death();
            TakeDamage();           
            
            Debug.Log("Enemy Hit You");
        }

        if (collision.gameObject.tag == "Floor")
        {
            floored = true;
        }
    }

    private void Continuousshooting()
    {
       
        continuousshootingtime -= 1;
        if (continuousshootingtime <= 0)
        {
            continuousshooting = false;
            continuousshootingtime = 10f;
           cheat = false;
        }
    }

    public bool getContinuousshooting()
    {
        return continuousshooting;
    }

    void Death()
    {
        Destroy(this.gameObject);
    }
}
