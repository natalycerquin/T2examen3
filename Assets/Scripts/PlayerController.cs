using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
     float speed = 0.1f;
     public float jumpSpeed = 1600.0f;
     bool SubirEscalera = false;
     bool Brinco = false;
     int salto = 2;
     int estado = 0;
     private bool planear = false;
     private bool control = false;
     /// Vida
     private Text vidaTxt;
     private int vida;
     private const string ENEMY = "Zombie";
     
     public GameObject Kunai;
     public GameObject Kunai2;
     public BoxCollider2D platformGround;
     float contadorSegundos = 0;
     private int contador = 0;
     Animator animator;
     Rigidbody2D rb;
     SpriteRenderer direcion;
     Transform target;
     
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        var  transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        direcion = GetComponent<SpriteRenderer>();
        target = GetComponent<Transform>();
        
        vidaTxt = GameObject.Find("Numv").GetComponent<Text>();
        vida = int.Parse(vidaTxt.text);
    }

    // Update is called once per frame
    void Update()
    { 
        estado = 0;
       animator.SetInteger("Estate", estado);
        speed = 0.1f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            estado = 2;
            speed = 7.0f;
            rb.velocity = new Vector2(speed, rb.velocity.y);    //velocidad de movimiento del personaje
            animator.SetInteger("Estate", estado);
            direcion.flipX = false;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            estado = 2;
            speed = 7.0f;
            rb.velocity = new Vector2(-speed, rb.velocity.y);    //velocidad de movimiento del personaje
            animator.SetInteger("Estate", estado);
            direcion.flipX = true;

        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            estado = 1;
            speed = 7.0f;
            rb.velocity = new Vector2(speed, rb.velocity.y);    //velocidad de movimiento del personaje
            animator.SetInteger("Estate", estado);
            direcion.flipX = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            estado = 1;
            speed = 7.0f;
            rb.velocity = new Vector2(-speed, rb.velocity.y);    //velocidad de movimiento del personaje
            animator.SetInteger("Estate", estado);
            direcion.flipX = true;
        }
        if (Input.GetKey(KeyCode.C))
        {
            estado = 4;
            animator.SetInteger("Estate", estado);
            planear = true;
        }

        if (salto <= 1)
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                Debug.Log("Space: " + contadorSegundos);
                control = false;
                contadorSegundos = 0.0f;
                estado = 6;
                animator.SetInteger("Estate", estado);
                rb.AddForce(Vector2.up * jumpSpeed);
                jumpSpeed = 800.0f;
            }
        }

        if (control)
        {
            contadorSegundos += Time.deltaTime;
        }

        if (contadorSegundos >= 0.25f && planear == false)
        {
            Destroy(this.gameObject, 1);
            vidaTxt.text = "0";
            Debug.Log("Morir: " + contadorSegundos);
        }
        

        if (!Input.GetKeyUp(KeyCode.X)) return;
        estado = 3;
        animator.SetInteger("Estate", estado);
        var bull = direcion.flipX ? Kunai2 : Kunai;
        var position = new Vector2(transform.position.x, transform.position.y);
        var rotancion = Kunai.transform.rotation;
        Instantiate(bull, position, rotancion);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == ENEMY)
        {
            rb.AddForce(Vector2.up * 400);
            vida -= 1;
            vidaTxt.text = vida.ToString();
            if (vida <= 0)
            {
                vidaTxt.text = "0";
                Destroy(this.gameObject, 1);
            }
            var flip = direcion.flipX;
            if (flip)
            {
                rb.AddForce(Vector2.right * 800);
            }
            else
            {
                Debug.Log("false");
                rb.AddForce(Vector2.left * 800);
            }
        }

        if (collision.gameObject.tag == "Suelo")
        {
            salto = 0;
            jumpSpeed = 1600.0f;
            SubirEscalera = false;
            Brinco = false;
            estado = 0;
            control = false;
            //Debug.Log("Contador: "+contadorSegundos);
            contadorSegundos = 0.0f;
            //Debug.Log("Fin: " + contadorSegundos);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Escalera")
        {
            estado = 5;
            animator.SetInteger("Estate", estado);
            if (Input.GetKey(KeyCode.UpArrow)!=false)
            {
                Debug.Log("Animar");
                Brinco = true;
                SubirEscalera = true;
                rb.gravityScale = 0f;
                rb.bodyType = RigidbodyType2D.Static;
               
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                platformGround.enabled = false;
                SubirEscalera = true;
                rb.gravityScale = 0f;
                rb.bodyType = RigidbodyType2D.Static;
                Brinco = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Escalera")
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            SubirEscalera = false;
            rb.gravityScale = 10;
            estado = 0;
            platformGround.enabled = true;
        }
        if (Brinco)
        {
            rb.AddForce(Vector2.up * 800);
        }

        if (collision.gameObject.tag == "Control")
        {
            control = true;
        }
    }
}
