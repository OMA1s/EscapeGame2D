using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D myRb;
    PlayerMovement player;
    float xSpeed;

    [SerializeField] float bulletSpeed = 2f;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    
    void Update()
    {
        myRb.velocity = new Vector2(xSpeed, 0f);
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.collider.gameObject.layer == 11)
        {
            Destroy(collision.gameObject);
        }
        */
        Destroy(this.gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }
}
