using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void howToPlay()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void spellCodex()
    {
        SceneManager.LoadScene("SpellCodex");
    }

    public void controls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void backToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
