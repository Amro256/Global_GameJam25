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
    [SerializeField] GameObject gameWinPanel;

    [SerializeField] GameObject CreditsPanel;
    [SerializeField] GameObject MainMenuPanel;

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
        SceneManager.LoadScene("SAMProto");
        Time.timeScale = 1;
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
        //Enable win screen here - Possibly change it to a different scene in the end
        gameWinPanel.SetActive(true);
    }

    public void showGameOverScreen()
    {
        //Enable the game over panel here
        gameOverPanel.SetActive(true);

        Time.timeScale = 0;
    }

    public void ShowCredits()
    {   
        CreditsPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    public void BackToMainMenu()
    {
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
    }
}
