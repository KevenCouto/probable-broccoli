using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss : MonoBehaviour {
    public float vida = 100f;
    Animator ani;
    Rigidbody2D corpo;
    public bool dano = false;
	// Use this for initialization
	void Start () {

        ani = GetComponent<Animator>();
        corpo = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

		
	}
    public void OnCollisionEnter2D(Collision2D Dano)
    {
        if (Dano.collider.gameObject.tag=="tiro")
        {
            Destroy(Dano.gameObject);
            vida-=10;
            corpo.GetComponent<Rigidbody2D>().AddForce(Vector3.down * 500);
            ani.SetTrigger("ddd");
            if (vida==0)
            {
                Destroy(gameObject);
            }
        }
    }
   
}
