using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausedStuff;
    [SerializeField] private GameObject loseStuff;
    [SerializeField] PlayerController playerInput;
    [SerializeField] private GameObject cursor;
    [SerializeField] PlayerHealth playerHealth;

    private void Start()
    {
        loseStuff.SetActive(false);
    }
    void Update()
    {
        if (playerInput.isPaused && !playerHealth.isDead)
        {
            pauseGame();
            pausedStuff.SetActive(true);
        } else if(!playerInput.isPaused && !playerHealth.isDead)
        {
            resumeGame();
            pausedStuff.SetActive(false);
        }
        if (playerHealth.isDead)
        {
            pauseGame();
            loseStuff.SetActive(true);
        }
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        playerInput.isPaused = false;
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerInput.isPaused = false;
    }

    public void resumeGame()
    {
        Time.timeScale = 1;
        playerInput.isPaused = false;
        cursor.SetActive(true);
        Cursor.visible = false;
    }

    private void pauseGame()
    {
        Time.timeScale = 0;
        cursor.SetActive(false);
        Cursor.visible = true;
    }
}
