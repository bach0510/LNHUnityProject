using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    //public Gun[] loadout;
    public Transform weaponParent;
    public Motion movementController;

    public AudioSource shoot;
    public AudioSource reload;

    public Text currentAmmoDisplay;
    public Text alternativeAmmoDisplay;
    public ParticleSystem muzzleFlash;
    //Animator animator;

    private GameObject currentGun;
    private int currentIndex;


    public static int maxAmmo = 14;
    public static int currentAmmo;

    private float sprintAdjValue;
    private bool reloading = false;

    public static int alternativeAmmo = 98;


    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        //audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1)) Equip(0);

        //if (currentGun != null)
        //{
        //    Aim(Input.GetMouseButton(1));

        //}
        if (Input.GetButtonDown("Fire1") && Input.GetMouseButton(1) && currentAmmo > 0 && !reloading)
        {
            Shoot();
        }
        if ((currentAmmo == 0 && Input.GetButtonDown("Fire1")))
        {
            Stab();
        }
        if ((Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && alternativeAmmo > 0))
        {
            reload.Play();
            reloading = true;
            StartCoroutine(Reload(2));
        }
        currentAmmoDisplay.text = currentAmmo.ToString();
        alternativeAmmoDisplay.text = alternativeAmmo.ToString();
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

    void Stab()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 3f))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                //if (hit.collider is BoxCollider)
                //{
                target.TakeDamage(50);

                //}
                //if (hit.collider is CapsuleCollider)
                //{
                //    target.TakeDamage(35 * 3f);
                //}

            }

        }
    }

    void Shoot()
    {

        currentAmmo -= 1;
        shoot.Play();
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
        if (alternativeAmmo > (maxAmmo - currentAmmo))
        {
            alternativeAmmo -= (maxAmmo - currentAmmo);// neu con dan du tru se nap lai va tru di so luong nap vao so dan du tru
            currentAmmo = maxAmmo;
        }
        else
        {
            currentAmmo += (alternativeAmmo);
            alternativeAmmo = 0;
        }
        
        reloading = false;

    }
}
