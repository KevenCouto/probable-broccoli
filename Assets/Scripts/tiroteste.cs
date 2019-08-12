using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiroteste : MonoBehaviour {


    public float tempo = 0;
    public float distancia = 3;
    public bool olhandoParaDireita;
    public float velocidade = 4;
    public float velocidadePerseguicao = 7;
    public Transform DetectaChao;
    public bool spot = false; //booleana para saber se o jogador esta dentro do campo de visão
    public Transform target; //alvo que o inimigo vai perseguir, nesse caso o jogador
    public Transform inicioCP; //inicio do campo de visão 
    public Transform fimCP; //final do campo de visão
    public GameObject tiro;
    public Transform localtiro;
    public float velotiro = 600f;
    private Animator bos;

    // Use this for initialization
    void Start()
    {
        bos = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Patrulha();
        Raycasting();
        Persegue();
        if (tempo>0)
        {
            tempo -= Time.deltaTime;
        }

    }

    public void Patrulha()
    {
        transform.Translate(Vector2.right * velocidade * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(DetectaChao.position, Vector2.down, distancia);
        if (groundInfo.collider == false)
        {
            if (olhandoParaDireita == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                olhandoParaDireita = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                olhandoParaDireita = true;
            }
        }
    }

    public void Raycasting()
    {
        Debug.DrawLine(inicioCP.position, fimCP.position, Color.green);
        spot = Physics2D.Linecast(inicioCP.position, fimCP.position, 1 << LayerMask.NameToLayer("Player"));
    }

    public void Persegue()
    {
        if (spot == true&tempo<=0)
        {
            //velocidade = velocidadePerseguicao;
            bos.SetTrigger("atacar");

            GameObject tirod = (GameObject)Instantiate(tiro, localtiro.position, Quaternion.identity);
            tempo = 2f;
            if (olhandoParaDireita == true)
            {
                tirod.GetComponent<Rigidbody2D>().AddForce(Vector3.left * velotiro);

            }
            else
            {
                tirod.GetComponent<Rigidbody2D>().AddForce(Vector3.right * velotiro);

            }
        }

        else if (spot == false)
        {
            velocidade = 0;
        }
    }
}
