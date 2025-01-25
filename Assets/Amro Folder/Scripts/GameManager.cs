using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Make this an instance so it can be accessed by other scripts
    public static GameManager insance;
    [SerializeField] GameObject gameOverPanel;

    void Awake() 
    {
        if(insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    //Method for starting and quiting the game
    public void StartGame()
    {
        SceneManager.LoadScene("ProtoScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void RetunToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //Add two new methods - Win & Lose State

    public void showWinScreen()
    {
        //Enable panel here
    }

    public void showGameOverScreen()
    {
        //Enable the game over panel here
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}
