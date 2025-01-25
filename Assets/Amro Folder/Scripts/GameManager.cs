using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Make this an instance so it can be accessed by other scripts
    public static GameManager insance;

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
}
