using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{

    public Vector3 check1;
    public Vector3 check2;
    public Vector3 check3;
    public Vector3 check4;
    public Vector3 check5;
    public Vector3 check6;


    void Awake(){
        if (PlayerPrefs.GetInt("Checkpoints") == 1)
        {
            this.transform.position = check1;
        }

        if (PlayerPrefs.GetInt("Checkpoints") == 2)
        {
            this.transform.position = check2;
        }
        if (PlayerPrefs.GetInt("Checkpoints") == 3)
        {
            this.transform.position = check3;
        }

        if (PlayerPrefs.GetInt("Checkpoints") == 4)
        {
            this.transform.position = check4;
        }
        if (PlayerPrefs.GetInt("Checkpoints") == 5)
        {
            this.transform.position = check5;
        }

        if (PlayerPrefs.GetInt("Checkpoints") == 6)
        {
            this.transform.position = check6;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Check1")
        {
            PlayerPrefs.SetInt("Checkpoints",1);
        }

        if (other.gameObject.tag == "Check2")
        {
            PlayerPrefs.SetInt("Checkpoints",2);
        }

        if (other.gameObject.tag == "Check3")
        {
            PlayerPrefs.SetInt("Checkpoints",3);
        }

        if (other.gameObject.tag == "Check4")
        {
            PlayerPrefs.SetInt("Checkpoints",4);
        }
        if (other.gameObject.tag == "Check5")
        {
            PlayerPrefs.SetInt("Checkpoints",5);
        }

        if (other.gameObject.tag == "Check6")
        {
            PlayerPrefs.SetInt("Checkpoints",6);
        }
    }
}
