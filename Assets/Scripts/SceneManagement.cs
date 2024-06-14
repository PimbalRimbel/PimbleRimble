using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{ 


    public void escenaJuego()
    {
        SceneManager.LoadScene("JUEGO");
    }

    public void escenaWin()
    {
        SceneManager.LoadScene("WIN_SCENE");
    }

    public void escenaCreditos()
    {
        SceneManager.LoadScene("CREDITOS");
    }

    public void escenaMenu()
    {
        SceneManager.LoadScene("MENU");
    }


    public void Salir()
    {
        Application.Quit();
    }


}

