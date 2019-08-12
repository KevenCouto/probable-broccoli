using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiro : MonoBehaviour {

    private float posiçõainicial = 0f;//posição em que o tiro foi disparado
    private float diatancia = 40f;//distancia maxima que ele pode percorrer
   
    // Use this for initialization
    void Start () {

       
	}
	
	// Update is called once per frame
	void Update () {
        if ( transform.position.x -posiçõainicial>diatancia || posiçõainicial-transform.position.x>diatancia)//verificar se o tiro ja percoreu sua distancia maxima e,se sim,  destrui-lo
        {
            Destroy(gameObject);
        }
       
           
        
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject/*.tag == "Player"*/)//conferir se ele bateu em alguma coisa e,se sim,  destrui-lo
        {
            
            
            Destroy(gameObject);
            
        }
    }
}
