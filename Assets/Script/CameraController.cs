using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Movement move;
    public static bool hideCursor = true;
    [System.Serializable]
    public class CamSetting
    {
        public float moceSpeed = 5; //tốc độ dị chuyển
        public float mouseX_Sensivity = 4;
        public float mouseY_Sensivity = 2;
        public float minClamp = -20;
        public float maxClamp = 40;
        public float rotateSpeed = 5;
        public float zoomFieldOfView = 15;
        public float originalZoomFieldOfView = 30;
        public float zoomSpeed = 5;

    }
    [SerializeField]
    CamSetting camsettings;


    [System.Serializable]
    public class camInputSettings
    {
        public string mouseXAxis = "Mouse X";
        public string mouseYAxis = "Mouse Y";
        public string aimInput = "Fire2";
    }
   
    [SerializeField]
    camInputSettings cis;

    Camera maincam;
    Transform center;
    Transform target;

    public Transform playerSpine;

    float camXRotate = 0;
    float camYRotate = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        maincam = Camera.main; // set main camera
        center = transform.GetChild(0).gameObject.transform;// khai báo tâm quay quanh của camerra
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var camcontroll = GameObject.FindGameObjectWithTag("CamController");
        // reset camera  tránh việc cam bị đơ cứng và đứng yên
        camcontroll.SetActive(false);
        camcontroll.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        rotateCam();// hàm xoay cam 
        zoomCam();// hàm zoom view của ca,
        HideCursor();// hàm ẩn con trỏ chuột
    }
    private void LateUpdate()
    {
        if (target)
        {
            FollowPlayer();// hàm theo player 

        }
        else
        {
            findPlayer();// hàm tìm player 
        }
    }
    void FollowPlayer()
    {
        // di chuyển theo nhân vật
        //Debug.Log(target.position);
        Vector3 MoveVecter = Vector3.Slerp(transform.position, target.position, Time.deltaTime * camsettings.moceSpeed);
        
        transform.position = MoveVecter;

    }

    void findPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;// tìm gameobject với tag là player 
    }

    void rotateCam()
    {
        //code di chuyển camera theo input
        camXRotate += Input.GetAxis(cis.mouseYAxis) * camsettings.mouseY_Sensivity;
        camYRotate += Input.GetAxis(cis.mouseXAxis) * camsettings.mouseX_Sensivity;

        camXRotate = Mathf.Clamp(camXRotate, camsettings.minClamp, camsettings.maxClamp);
        camYRotate = Mathf.Repeat(camYRotate, 360);

        Vector3 rotatingAngle = new Vector3(-camXRotate, camYRotate, 0);
        Quaternion rotate = Quaternion.Slerp(center.transform.localRotation, Quaternion.Euler(rotatingAngle), camsettings.rotateSpeed * Time.deltaTime);
        center.transform.localRotation = rotate;

        // aim assist system (co tham khao tren mang)
        RaycastHit hit;
        if (Input.GetButton(cis.aimInput))// nếu nhấn nút aim 
        {
            if (Physics.Raycast(maincam.transform.position, maincam.transform.forward, out hit, 3f)) // xét tiếp nếu tâm xanh ở gần mục tiêu tầm dưới 3f 
            {
                Target target = hit.transform.GetComponent<Target>();// tìm trong mục tiêu mà tâm xanh đi qua và gán gameobject nào có component Target cho biến target (tức nếu tâm xanh tìm thấy mục tiêu )
                if (target != null)// nếu mục tiêu khác null
                {
                    if (Physics.SphereCast(transform.position, 2f, transform.forward, out hit))// hỗ trợ kéo tâm về target (nhẹ)
                    {
                        //Debug.Log("Aim Assist: Hit");
                    }
                }
            }

        }

    }


    void zoomCam()
    {
        if (Input.GetButton(cis.aimInput))// nếu nhấn nút ngắm bắn tức chuột phải (zoom cam lại gần nhân vật )
        {

            maincam.fieldOfView = Mathf.Lerp(maincam.fieldOfView, camsettings.zoomFieldOfView, camsettings.zoomSpeed * Time.deltaTime);// thu cam lại player hoặc giảm tầm nhìn tức FOV về zoomFieldOfView

        }
        else // nếu ko nhấn  nút aim => bỏ zoom
        {
            maincam.fieldOfView = Mathf.Lerp(maincam.fieldOfView, camsettings.originalZoomFieldOfView, camsettings.zoomSpeed * Time.deltaTime);// trả cam lại fov bình thường
        }
    }

    void HideCursor()
    {
        if (hideCursor)// nếu biến ẩn con trỏ true 
        {
            // ản con trỏ chuột
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hideCursor = false;
            }
        }
        else // trường hợp khác => bỏ ẩn con trỏ chuột
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hideCursor = true;
            }
        }
    }
}
