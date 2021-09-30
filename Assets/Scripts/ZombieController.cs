using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{
    float velocityX = 5.0f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(-velocityX , rb.velocity.y);
    }
    //
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Des")
        {
            Destroy(this.gameObject, 1);
        }
    }
}
