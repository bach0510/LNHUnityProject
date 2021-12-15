using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScreen : MonoBehaviour
{
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        var target = GameObject.FindGameObjectWithTag("Player");
        target.SetActive(false);
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        
    }
}
