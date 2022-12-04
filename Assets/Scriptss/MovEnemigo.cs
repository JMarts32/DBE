using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovEnemigo : MonoBehaviour
{
    public float RadioVision;
    public float RadioAtaque;
    public float speed;

    [Tooltip("Prefab de la roca que se disparara")]
    public GameObject roca;
    [Tooltip("Velocidad de ataque (segundos entre ataque)")]
    public float attackspeed;
    bool attacking;

    GameObject player;
    Vector3 initialPosition, target;
    Animator anim;
    Rigidbody2D rb2d;

    [Tooltip("Puntos de vida")]
    public int maxHp = 4;
    [Tooltip("Vida actual")]
    public int hp;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        initialPosition = transform.position;

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();

        hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        target = initialPosition;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            player.transform.position - transform.position,
            RadioVision,
            1 << LayerMask.NameToLayer("Default")
        );

        Vector3 forward = transform.TransformDirection(player.transform.position - transform.position);
        Debug.DrawRay(transform.position, forward, Color.red);
        
        if (hit.collider != null){
            if (hit.collider.tag == "Player"){
                target = player.transform.position;
            }
        }

        float distance = Vector3.Distance(target, transform.position);
        Vector3 dir = (target - transform.position).normalized;

        if (target != initialPosition && distance < RadioAtaque){
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.Play("Enemigo walk", -1, 0); //Congela la animacion de andar

            if (!attacking) StartCoroutine(Attack(attackspeed));
        }
        else{
            rb2d.MovePosition(transform.position + dir * speed * Time.deltaTime);
            anim.speed = 1;
            anim.SetFloat("movX", dir.x);
            anim.SetFloat("movY", dir.y);
            anim.SetBool("Walking", true);
        }
        
        if (target == initialPosition && distance < 0.05f){
            transform.position = initialPosition;
            anim.SetBool("Walking", false);
        }

        Debug.DrawLine(transform.position, target, Color.green);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, RadioVision);
        Gizmos.DrawWireSphere(transform.position, RadioAtaque);
    }

    IEnumerator Attack(float seconds) {
        attacking = true;
        if (target != initialPosition && roca  != null){
            Instantiate(roca, transform.position, transform.rotation);
            yield return new WaitForSeconds(seconds);
        }
        attacking = false;
    }

    public void Attacked(){
        if (--hp <= 0) Destroy(gameObject);
    }  

    void OnGUI(){
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        
        GUI.Box(
            new Rect(
                pos.x - 20,
                Screen.height - pos.y + 60,
                40,
                24
            ), hp + "/" + maxHp
        );
    } 

}
