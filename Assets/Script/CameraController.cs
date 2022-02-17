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
        
        maincam = Camera.main;
        center = transform.GetChild(0).gameObject.transform;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        var camcontroll = GameObject.FindGameObjectWithTag("CamController");
        camcontroll.SetActive(false);
        camcontroll.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            return;
        rotateCam();
        zoomCam();
        HideCursor();
    }
    private void LateUpdate()
    {
        if (target)
        {
            FollowPlayer();

        }
        else
        {
            findPlayer();
        }
    }
    void FollowPlayer()
    {
        //Debug.Log(target.position);
        Vector3 MoveVecter = Vector3.Slerp(transform.position, target.position, Time.deltaTime * camsettings.moceSpeed);
        
        transform.position = MoveVecter;

    }

    void findPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void rotateCam()
    {
        camXRotate += Input.GetAxis(cis.mouseYAxis) * camsettings.mouseY_Sensivity;
        camYRotate += Input.GetAxis(cis.mouseXAxis) * camsettings.mouseX_Sensivity;

        camXRotate = Mathf.Clamp(camXRotate, camsettings.minClamp, camsettings.maxClamp);
        camYRotate = Mathf.Repeat(camYRotate, 360);

        Vector3 rotatingAngle = new Vector3(-camXRotate, camYRotate, 0);
        Quaternion rotate = Quaternion.Slerp(center.transform.localRotation, Quaternion.Euler(rotatingAngle), camsettings.rotateSpeed * Time.deltaTime);
        center.transform.localRotation = rotate;


        //RaycastHit hit;
        //if (Input.GetButton(cis.aimInput))
        //{
        //    Target target = hit.transform.GetComponent<Target>();
        //    if (target != null)
        //    {
        //        if (Physics.SphereCast(transform.position, 2f, transform.forward,  hit))
        //        {
        //            Debug.Log("Aim Assist: Hit");
        //        }
        //    }
                
        //}

    }


    void zoomCam()
    {
        if (Input.GetButton(cis.aimInput))
        {

            maincam.fieldOfView = Mathf.Lerp(maincam.fieldOfView, camsettings.zoomFieldOfView, camsettings.zoomSpeed * Time.deltaTime);

        }
        else
        {
            maincam.fieldOfView = Mathf.Lerp(maincam.fieldOfView, camsettings.originalZoomFieldOfView, camsettings.zoomSpeed * Time.deltaTime);
        }
    }

    void HideCursor()
    {
        if (hideCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                hideCursor = false;
            }
        }
        else
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
