using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roca : MonoBehaviour
{
    [Tooltip("Velocidad de movimiento en unidades del mundo")]

    private GameObject healthbar;
    public float speed;

    GameObject player;
    Rigidbody2D rb2d;
    Vector3 target, dir;

    AudioSource myAudio;
    public AudioClip speedSound;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2d = GetComponent<Rigidbody2D>();

        if (player != null){
            target =  player.transform.position;
            dir = (target - transform.position).normalized;
        }

        healthbar = GameObject.Find("Healthbar");
        myAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (target != Vector3.zero){
            rb2d.MovePosition(transform.position + (dir * speed) * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "Player" || col.transform.tag == "Attack"){
            myAudio.PlayOneShot(speedSound);
            Destroy(gameObject);
            healthbar.SendMessage("TakeDamage", 15);
            
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
