using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public GameManager gameManager;
    public float fuerzaSalto;
    public int vida = 500;  // Nueva variable para las vidas del jugador

    private Rigidbody2D rb;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.start)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetBool("estaSaltando", true);
                rb.AddForce(new Vector2(0, fuerzaSalto));
            }
        }

        if (vida <= 0) // El juego termina si la vida llega a 0
        {
            gameManager.gameOver = true;
        }

        if (gameManager.gameOver)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Suelo")
        {
            anim.SetBool("estaSaltando", false);
        }

        if (collision.gameObject.tag == "Obstaculo")
        {
            vida -= 100;
            if(vida <= 0)
            {
                gameManager.gameOver = true;
                vida = 0; // para evitar mostrar vida negativa
            }
        }

    }
}
