using UnityEngine;
using UnityEngine.UI;

public class KunaiController : MonoBehaviour
{
    float velocityX = 10;
    private const string ENEMY = "Zombie";
    
    /// Puntaje
    private Text puntajeTxt;
    private int puntaje;
    Rigidbody2D rb;
    void Start()
    {
        Destroy(this.gameObject, 5);
        rb = GetComponent<Rigidbody2D>();
        
        ///Sumar Puntaje
        puntajeTxt = GameObject.Find("Nump").GetComponent<Text>();
        puntaje = int.Parse(puntajeTxt.text);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(velocityX, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ENEMY)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            puntaje += 10;
            puntajeTxt.text = puntaje.ToString();
        }
    }
}
