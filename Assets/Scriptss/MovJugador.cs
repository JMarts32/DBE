using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovJugador : MonoBehaviour
{
    public float velocidad;
    Animator anim;
    Rigidbody2D rb2d;
    Vector2 mov;

    [Tooltip("Prefab del fuego que se disparara")]
    public GameObject fuego;
    [Tooltip("Velocidad de ataque (segundos entre ataque)")]
    public float attackspeed;
    private bool attacking;

    CircleCollider2D attackCollider;

    public AudioClip speedSound;
    AudioSource myAudio;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        attackCollider = transform.GetChild(0).GetComponent<CircleCollider2D>();
        attackCollider.enabled = false;

        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        mov = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        if (mov != Vector2.zero) {
            anim.SetFloat("movX", mov.x);
            anim.SetFloat("movY", mov.y);
            anim.SetBool("caminando", true);
        } else {
            anim.SetBool("caminando", false);
        }

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        bool atacando = stateInfo.IsName("Jugador_Attack");

        if (Input.GetKeyDown(KeyCode.G) && !atacando )
        {
            anim.SetTrigger("atacando");
            myAudio.PlayOneShot(speedSound);
        }


        if (mov != Vector2.zero) attackCollider.offset = new Vector2(mov.x/2, mov.y/2);

        if (atacando)
        {
            float playbackTime = stateInfo.normalizedTime;
            if (playbackTime > 0.0 && playbackTime < 1.0) attackCollider.enabled = true;
            else attackCollider.enabled = false;
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            attacking = true;
            if (fuego  != null){
                Instantiate(fuego, transform.position, transform.rotation);
            }
            attacking = false;            
        }
        
    }

    void FixedUpdate ()
    {
        rb2d.MovePosition(rb2d.position + mov * velocidad * Time.deltaTime);
    }

}
