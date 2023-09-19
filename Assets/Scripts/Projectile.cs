using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float projectilespeed = 200f;
    public Vector3 Direction = Vector3.zero;
    public float visible = 5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * projectilespeed * Time.deltaTime;
        visible -= Time.deltaTime;

        if (visible <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Ouch!");
            other.GetComponentInParent<Player>().TakeDamageFromBoss();
            Destroy(this.gameObject);


        }
    }
}
