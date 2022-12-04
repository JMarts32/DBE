using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueFinal : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento en unidades del mundo")]

    public float speed;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target, dir;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hector");
        rb2d = GetComponent<Rigidbody2D>();

        if (player != null){
            target =  player.transform.position;
            dir = (target - transform.position).normalized;
        }
    }

    void FixedUpdate()
    {
        if (target != Vector3.zero){
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Hector") {
            Destroy(gameObject);
            col.SendMessage("Attacked");
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}

