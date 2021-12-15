using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public Movement move;
    [System.Serializable]
   public class InputSetting
    {
       public string horizontal = "Horizontal";// x
       public string vertical = "Vertical"; //y
        public string sprint = "Sprint";
        public string aim = "Fire2";
        public string shoot = "Fire1";
    }
    [SerializeField]
    InputSetting input;

    [Header("camera and character syncing")]
    public float lookDistance = 5f;
    public float lookSpeed = 5f;

    Transform camCenter;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<Movement>();
        camCenter = Camera.main.transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis(input.horizontal) != 0 || Input.GetAxis(input.vertical) != 0 || Input.GetButton(input.aim))
        {
            rotateToCamView();
        }
        move.CharacterMove(Input.GetAxis(input.horizontal),Input.GetAxis(input.vertical));
        move.CharacterSprint(Input.GetButton(input.sprint));

        move.CharacterAim(Input.GetButton(input.aim));
        if (Input.GetButton(input.shoot))
        {
            move.CharacterShoot();
        }
    }

    void rotateToCamView()
    {
            Vector3 camCenterPos = camCenter.position;
            Vector3 lookPoint = camCenterPos + (camCenter.forward * lookDistance);
            Vector3 dir = lookPoint - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(dir);

            lookRotation.x = 0;
            lookRotation.z = 0;

            Quaternion final = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * lookSpeed);

            transform.rotation = final;
       
    }
}
