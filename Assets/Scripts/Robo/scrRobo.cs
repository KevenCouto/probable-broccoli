using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrRobo : MonoBehaviour {

    #region Propriedades e Objetos Gerais
    Rigidbody2D rbPlayer;
    Animator aPlayer;
    float eixoX;
    public float velocidadeMaxima = 0.0f;
    public bool olhandoDireita = true;
    #endregion

    #region Propriedades e Objetos para Movimentação
    [Header("Movimento")]
    public Transform localPes;
    public bool noChao;
    public LayerMask camadaChao;
    #endregion

    #region Propriedades e Objetos para Pulo e Pulo Duplo
    [Header("Pulo")]
    public float forcaPulo;
    int qtdPulos = 0;
    #endregion

    #region Propriedades e Objetos para Ataque de Slash
    [Header("Ataque")]
    public bool atacando = false;
    #endregion
    public GameObject tiro;
    public Transform localtiro;
    public float velotiro = 600f;
    public float vida = 100f;

    // Use this for initialization
    void Start()
    {
        #region Associa os Componentes do GameObject
        rbPlayer = GetComponent<Rigidbody2D>();
        aPlayer = GetComponent<Animator>();
        //tiro = GetComponent<>(p);
        #endregion
    }

    void FixedUpdate()
    {
        #region Aplica movimento horizontal baseado no Eixo e na Física
        float aceleracao = eixoX * velocidadeMaxima;
        rbPlayer.velocity = new Vector2(aceleracao * Time.deltaTime, rbPlayer.velocity.y);
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Detecção do Contato com o Chão
        noChao = Physics2D.OverlapCircle(localPes.position, 0.02f, camadaChao);
        #endregion

        #region Percebe a interação no eixo Horizontal e Flipa a Imagem do Jogador conforme direção
        eixoX = Input.GetAxisRaw("Horizontal");
        if (eixoX < 0 && olhandoDireita)
        {
            Virar();
        }
        if (eixoX > 0 && !olhandoDireita)
        {
            Virar();
        }
        #endregion

        #region Aplica Animação Adequada
        aPlayer.SetBool("noChao", noChao);
        aPlayer.SetFloat("velY", rbPlayer.velocity.y);
        if (eixoX == 0)
            aPlayer.SetInteger("indiceAnim", 0);
        else
            aPlayer.SetInteger("indiceAnim", 1);
        #endregion

        #region Aplicação do Pulo e Pulo Duplo
        if (Input.GetButtonDown("Jump") && noChao)
        {
            rbPlayer.AddForce(new Vector2(0f, forcaPulo), ForceMode2D.Impulse);
            qtdPulos++;
        }
        if (Input.GetButtonDown("Jump") && !noChao)
        {
            if (rbPlayer.velocity.y > 0 && qtdPulos <= 1)
            {
                rbPlayer.AddForce(new Vector2(0f, forcaPulo - 2), ForceMode2D.Impulse);
                qtdPulos++;
            }
        }
        #endregion

        #region Zera quantidade de Pulos ao voltar para o Chão
        if (noChao)
        {
            qtdPulos = 0;
        }
        #endregion

        #region Aplicação do Ataque de Slash ( com Animação )
        if (Input.GetButtonDown("Fire1") && !atacando)
        {
            aPlayer.SetTrigger("atacar");
            GameObject tirod = (GameObject)Instantiate(tiro, localtiro.position, Quaternion.identity);
            if (olhandoDireita==true)
            {
                tirod.GetComponent<Rigidbody2D>().AddForce(Vector3.right * velotiro);
               
            }
            else
            {
                tirod.GetComponent<Rigidbody2D>().AddForce(Vector3.left * velotiro);
             
            }
           
            
        }
        #endregion
    }
    private void OnCollisionEnter2D(Collision2D alvejado)
    {
        if (alvejado.gameObject.tag == "tiro")
        {
            aPlayer.SetTrigger("ddd");
            vida -= 10f;
           
        }
        if (vida == 0)
        {
            Destroy(gameObject);
        }


    }
    #region Método que faz a Inversão da Imagem ( FLIP )
    public void Virar()
    {
        olhandoDireita = !olhandoDireita;
        float x = transform.localScale.x;
        x *= -1;
        transform.localScale = new Vector3(x, 1, 1);
    }
    #endregion
}
