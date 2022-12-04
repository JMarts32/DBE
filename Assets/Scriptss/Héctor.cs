using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Héctor : MonoBehaviour
{
    GameObject player;
    [Tooltip("Puntos de vida")]
    public int maxHp = 1;
    [Tooltip("Vida actual")]
    public int hp;
    private int falsehp;
    private int falsemaxHp = 100;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Hector");
        hp = maxHp;
        falsehp = falsemaxHp;
    }

    public void Attacked(){
        if (--hp <= 0)
        {
        Destroy(gameObject);
        SceneManager.LoadScene("Creditos");
        }
    }
    
    void OnGUI(){
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        
        GUI.Box(
            new Rect(
                pos.x - 20,
                Screen.height - pos.y + 60,
                60,
                24
            ),falsehp + "/" + falsemaxHp
        );
    }

}
