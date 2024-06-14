using System;
using UnityEngine;

public class WinCond : MonoBehaviour
{
    public SceneManagement sceneManagement;

    private void Start()
    {
        try
        {
            sceneManagement = FindObjectOfType<SceneManagement>();
            Debug.Log("SI encuentro");
        }
        catch (Exception) { Debug.Log("NO encuentro"); }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            sceneManagement.escenaWin();
            Debug.Log("Intento cambiar de escena");
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player")
        {
            sceneManagement.escenaWin();
            Debug.Log("Intento cambiar de escena");
        }
    }
}
