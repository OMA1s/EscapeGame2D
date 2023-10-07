using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    Rigidbody2D myRb;
    [SerializeField] int moveSpeed;

    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        myRb.velocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            moveSpeed = -moveSpeed;
            this.transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1f);
        }
        
    }
}
