using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class witch : MonoBehaviour
{
    private float witchhealth = 100;
    private float witchtimer = 25f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("witchTiming", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (witchhealth <= 0)
        {

            Death();
        }

        if (witchtimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

   public void TakeDamage()
    {
        witchhealth -= 20;
    }

    public void Death()
    {
        
        if (GameController.instance != null)
            GameController.instance.incrementscoreWitch();

        else
            GameController2.instance.incrementscoreWitch();
        Destroy(this.gameObject);
    }

    void witchTiming()
    {
        if (witchtimer <= 0)
            return;

        witchtimer--;
    }
}
