using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public float playerHealth = 100;
    public Image healthBar;
    public GameObject player;

    public GameoverScreen gameover;

    public bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            // do something when game is over
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Destroy(player);
            gameover.Setup(0);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!isGameOver)
        {
            playerHealth -= damage;
            //Debug.Log("hit");
            //Debug.Log(playerHealth);
            healthBar.fillAmount = playerHealth / 100;
            if (playerHealth <= 0)
            {
                isGameOver = true;
            }
        }
    }
}
