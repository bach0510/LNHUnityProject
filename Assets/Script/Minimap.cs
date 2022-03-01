using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;// khai báo trnassform của player 
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 newPos = player.position;// gán vị trí của plaer khi di chuyển 
        newPos.y = transform.position.y;
        transform.position = newPos;

        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);// di chuyển camera trên cao theo player 
    }
}
