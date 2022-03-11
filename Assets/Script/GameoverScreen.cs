using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    public Text highScore;// điểm cao
    public Text graphic;

    public void Update()
    {
        if(highScore != null) highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();// set text điểm cao
        if(graphic != null) graphic.text = PlayerPrefs.GetInt("Graphic") == 0 ? "Low Graphic" : PlayerPrefs.GetInt("Graphic") == 2 ? "Medium Graphic" : "High Graphic";// set graphic
    }
    public void Setup(int score) // set up
    {
        gameObject.SetActive(true);// game object là màn hình lúc người chơi bị hết thanh máu
        // hệ thống tính điểm
        highScore.text = "HighScore: " + PlayerPrefs.GetInt("HighScore").ToString();// set text điểm cao
        if (ScoreSystem.scoreValue > PlayerPrefs.GetInt("HighScore")) // nếu điểm hiện tại lớn hơn điểm cao trc đó thì set điểm cao là giá trị mới 
        {
            PlayerPrefs.SetInt("HighScore", ScoreSystem.scoreValue);// set điểm cao là giá trị mới
            highScore.text = "HighScore: " +  PlayerPrefs.GetInt("HighScore").ToString();// set text điểm cao
        }
        
        //ScoreSystem.scoreValue = 0;
        var target = GameObject.FindGameObjectWithTag("Player");
        target.SetActive(false);// disable nhân vật khi hết máu 
    }

    public void Restart()// nút restart trò chơi 
    {
        Debug.Log("Restarting");
        ScoreSystem.scoreValue = 0 ;// restet điểm của màn chơi mới về 0 
        Weapon.alternativeAmmo = 98;// reset số đạn tối đa
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);// load lại scene hiện tại tức scene màn chơi
        // aanr con trỏ chuột
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void StartGame() //nút bắt đầu game
    {
        Debug.Log("The Game Has Started ,Enjoy the game");
        // reset các giá trị điểm và băng đạn 
        ScoreSystem.scoreValue = 0;
        Weapon.alternativeAmmo = 98;
        SceneManager.LoadSceneAsync("Game");// load scene game 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ReturnToMainMenu()// nut quay lại menu chính
    {
        // load scene menu
        SceneManager.LoadSceneAsync("MainMenu");

    }

    public void SetGraphic(int level)// nút thay đổi đồ họa 
    {
        QualitySettings.SetQualityLevel(level);
        PlayerPrefs.SetInt("Graphic", level);
    }

    public void ExitGame()
    {
        Application.Quit();// thoat game
    }
}
