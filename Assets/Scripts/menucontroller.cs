using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menucontroller : MonoBehaviour
{
    public Animator anim;
    public GameObject panelOscuro;
    public GameObject optionsPanel;

    private void Start()
    {
        Cursor.visible = true;
    }
    public void NuevoJuego()
    {        
        panelOscuro.SetActive(true);
        anim.SetBool("isClicked", true);
        StartCoroutine(LoadScene());
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(2);
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(3);
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }

    public void OptionsPanel()
    {
        optionsPanel.SetActive(true);
    }

    public void SalirJuego()
    {
        Application.Quit();
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
        //Cursor.lockState = CursorLockMode.None;
    }

    public void Test()
    {
        Debug.Log("Has clicado");
    }

}
