using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator PlayButton;
    public Animator QuitButton;
    public Animator text;

    public Animator Spaceship;

    public void ShipMiddle()
    {
        text.Play("TextAppearing");
    }
    public void StartGame()
    {
        PlayButton.Play("PlayButtonQuit");
        QuitButton.Play("QuitButtonExit");
        text.Play("Idle");
        Spaceship.SetBool("PlayClicked",true);
    }

    public void ShipLeft()
    {
        SceneManager.LoadScene("preload");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
