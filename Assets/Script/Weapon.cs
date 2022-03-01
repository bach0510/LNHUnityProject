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
        if (Input.GetButtonDown("Fire1") && Input.GetMouseButton(1) && currentAmmo > 0 && !reloading)// nếu nhấn nuts bắn khi đang ngắm bắn và lượng đạn trong băng > 0 và không ở trạng thái nạp đạn 
        {
            Shoot(); // hàm bắn 
        }
        if ((currentAmmo == 0 && Input.GetButtonDown("Fire1")))
        {
            Stab();
        }
        if ((Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo && alternativeAmmo > 0))// nếu băng đạn còn 0 viên đạn và số đạn dự trữ > 0 và nhấn nút R 
        {
            reload.Play();// chạy âm thanh nạp đạn 
            reloading = true;// set trạng thái đang nạp đạn = true
            StartCoroutine(Reload(2));// chạy hàm reload
        }
        currentAmmoDisplay.text = currentAmmo.ToString();// set text cho số lượng đạn hiện tại 
        alternativeAmmoDisplay.text = alternativeAmmo.ToString();// set text cho số lượng đạn dự trữ
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

    void Stab()// hàm đâm dao zombie (giống hàm bắn nhưng khoảng cách gay dame bây h sẽ chỉ là 3)
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

    void Shoot() // hàm bắn 
    {

        currentAmmo -= 1;// trừ đi số lượng đạn trong băng đi 1
        shoot.Play();// chạy âm thanh tiếng súng bắn 
        muzzleFlash.Play();// chạy hiệu ứng tia lửa bắn ở đầu nòng súng
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 20f))// nếu bắn mục tiêu trong phạm vi 20f thì mới gay dame
        {
            Target target = hit.transform.GetComponent<Target>();// gán mục tiêu 
            if (target != null)// nếu bắn trúng tức target hay mục tiêu khác null
            {
                Debug.Log(hit.collider);
                if (hit.collider is BoxCollider)// nếu đối tượng bắn trúng là box collider (người của zombie có collider là box collider)
                {
                    target.TakeDamage(35); // gây dame cho mục tiêu zombie nhưng dame chỉ có 35f vì bắn vào người nên phải bắn 3 phát vào người mới đủ 100 dame để giết mục tiêu và ghi điểm

                }
                if (hit.collider is CapsuleCollider)// nếu đối tượng bắn trúng là Capsule collider (đầu của zombie có collider là Capsule collider)
                {
                    target.TakeDamage(35 * 3f); // gây dame đầu cho mục tiêu tức dame headshot x3 dame người => nếu bắn trúng đầu có cơ hội giết luôn mục tiêu chỉ với 1 viên đạn
                }

            }

        }
    }


    IEnumerator Reload(float reloadTime)
    {

        yield return new WaitForSeconds(reloadTime);
        if (alternativeAmmo > (maxAmmo - currentAmmo)) // nếu số đạn dự trữ lớn hơn số đạn cần thay 
        {
            alternativeAmmo -= (maxAmmo - currentAmmo);// số lượng đạn dự trữ sẽ giảm đi số lượng đạn mình nạp vào băng 
            currentAmmo = maxAmmo;// set lại băng đạn lên mức đầy
        }
        else // nếu số đạn dự trữ nhỏ hơn số lượng đạn mình cần thay 
        {
            currentAmmo += (alternativeAmmo);// cộng thêm số đạn còn sót lại
            alternativeAmmo = 0; // số đạn dự trữ = 0 
        }
        
        reloading = false;// set trạng thái nạp đạn về false

    }
}
