using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour
{
    
    public void NuevoJuego()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

}
