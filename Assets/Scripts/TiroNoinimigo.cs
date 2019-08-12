using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroNoinimigo : MonoBehaviour {
    public GameObject tiro;//Prefab do tiro
    public Transform localtiro;//Local da onde o tiro saira
    public float velotiro = 6000f;//velocidade em que o tiro sera disparado
    public float tempo = 0;//tempo entre um tiro e outro
    public bool olhandoParaDireita;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (tempo > 0)
        {
            tempo -= Time.deltaTime;//contagem entre um tiro e outro
        }

        if ( tempo <= 0)
        {
            
            
            GameObject tirodisparado = (GameObject)Instantiate(tiro, localtiro.position, Quaternion.identity);//intanciando um prefab do tiro para o local do tiro
            tempo = 2f;//adicionado tempo entre tiros
            if (olhandoParaDireita == true)//saber para que lado a força vai ser exercida
            {
                tirodisparado.GetComponent<Rigidbody2D>().AddForce(Vector3.left * velotiro);

            }
            else
            {
                tirodisparado.GetComponent<Rigidbody2D>().AddForce(Vector3.right * velotiro);

            }
        }
		
	}
}
