using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }
    public void playGame()
    {
        SceneManager.LoadScene("Game");
        Cursor.visible = false;
    }

    public void quitGame()
    {
        Application.Quit();
    }
    public void options()
    {
        SceneManager.LoadScene("Options");
    }
}
