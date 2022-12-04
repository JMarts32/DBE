using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu_controlador : MonoBehaviour
{
    public void cambiarEscena(string nombreScena)
    {
        SceneManager.LoadScene(nombreScena);
    }
}
