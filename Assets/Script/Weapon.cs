using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //public Gun[] loadout;
    public Transform weaponParent;
    public Motion movementController;

    private AudioSource audio;

    //public Text currentAmmoDisplay;
    public ParticleSystem muzzleFlash;
    //Animator animator;

    private GameObject currentGun;
    private int currentIndex;


    private int maxAmmo;
    private int currentAmmo;

    private float sprintAdjValue;


    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);

        //if (currentGun != null)
        //{
        //    Aim(Input.GetMouseButton(1));

        //}
        if (Input.GetButtonDown("Fire1") && Input.GetMouseButton(1))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R) )
        {
            
            StartCoroutine(Reload(2));
        }
        //currentAmmoDisplay.text = currentAmmo.ToString();
    }

    //void Equip(int i)
    //{
    //    if (currentGun != null) Destroy(currentGun);

    //    currentIndex = i;

    //    GameObject newEquip = Instantiate (loadout[i].prefab, weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
    //    newEquip.transform.localPosition = Vector3.zero;
    //    newEquip.transform.localEulerAngles = Vector3.zero;

    //    currentGun = newEquip;
    //    animator = currentGun.transform.Find("Anchor/Gun").GetComponent<Animator>();

    //    maxAmmo = loadout[i].ammo;
    //    currentAmmo = maxAmmo;        //currentAmmo = maxAmmo;
    //}

    void Shoot()
    {

        currentAmmo -= 1;
        audio.Play();
        muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20f))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                //if (hit.collider is BoxCollider)
                //{
                    target.TakeDamage(35);
                    
                //}
                //if (hit.collider is CapsuleCollider)
                //{
                //    target.TakeDamage(35 * 3f);
                //}

            }

        }
    }

    IEnumerator Reload(float reloadTime)
    {

        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;


    }
}
