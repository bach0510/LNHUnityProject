using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    public Text highScore;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        if(ScoreSystem.scoreValue > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreSystem.scoreValue);
            highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        }
        
        //ScoreSystem.scoreValue = 0;
        var target = GameObject.FindGameObjectWithTag("Player");
        target.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        ScoreSystem.scoreValue = 0 ;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void StartGame()
    {
        Debug.Log("The Game Has Started ,Enjoy the game");
        ScoreSystem.scoreValue = 0;
        SceneManager.LoadSceneAsync("Game");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");

    }
}
