using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour
{
    public GameObject player;
    public Text tutorial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 2.5f && Weapon.alternativeAmmo < 98) //nếu khoảng cách giữa người chơi và postion của hộp đạn dự trữ nhỏ hơn 2.5 và băng đạn dự trữ hiện tại của súng nhỏ hơn 98 
        {
            tutorial.gameObject.SetActive(true); // hiện thông báo hướng dẫn "nhấn f để nạp đầy số đạn dự trữ"
            if (Input.GetKeyDown(KeyCode.F)) // nếu nhấn F
            {
                Weapon.alternativeAmmo = 98;// set lại số đạn dự trữ của bản thân về đầy (tức 98 viên )
                Destroy(transform.gameObject);// Destroy object hộp đạn dự trữ
            }
        }
        else // trường hợp khác tức không lại gần băng đạn hoặc số đạn đã max sẵn => khoong hiện thông báo
        {
            tutorial.gameObject.SetActive(false);
        }
    }
}
