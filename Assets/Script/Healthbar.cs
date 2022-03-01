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
        if (isGameOver)// nếu game kết thúc 
        {
            // do something when game is over
            // hiện con trỏ chuột khi game kết thúc
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //Destroy(player);
            gameover.Setup(0);// set up
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20f); // test nhận dame người chơi
        }
    }

    public void TakeDamage(float damage) // hàm gay dame cho người chơi
    {
        if (!isGameOver)// nếu game chưa kết thúc
        {
            playerHealth -= damage;// trừ thanh máu nhân vật

            //Debug.Log("hit");
            //Debug.Log(playerHealth);
            healthBar.fillAmount = playerHealth / 100;// tăng giảm độ đầy của UI thanh máu
            if (playerHealth <= 0)// nếu thanh máu nhỏ hơn 0 
            {
                isGameOver = true;// trò chơi kết thúc 
            }
        }
    }
}
