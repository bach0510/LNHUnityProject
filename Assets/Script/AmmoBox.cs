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
        if (Vector3.Distance(transform.position, player.transform.position) < 2.5f && Weapon.alternativeAmmo < 98)
        {
            tutorial.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Weapon.alternativeAmmo = 98;
                Destroy(transform.gameObject);
            }
        }
        else
        {
            tutorial.gameObject.SetActive(false);
        }
    }
}
