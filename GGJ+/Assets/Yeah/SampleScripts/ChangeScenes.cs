using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("3D");// Se carga la escena gracias al SceneManager  lo cual servira al darle el boton start 
        Debug.Log("toco el boton yes");// manda a imprimir en la consola 
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainTrueMenu");// Se carga la escena gracias al SceneManager  lo cual servira al darle el boton start 
        Debug.Log("toco el boton yes");// manda a imprimir en la consola 
    }

    public void ExitGame()
    {
        Application.Quit();// Se carga la escena gracias al SceneManager  lo cual servira al darle el boton start 
        Debug.Log("Diga bye bye");// manda a imprimir en la consola 
    }


}
