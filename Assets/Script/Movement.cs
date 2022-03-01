using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Movement : MonoBehaviour
{
   public Animator anim;
   public CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // di chuyển trái phải lên xuống
    public void CharacterMove(float strafe, float forward)// trigger animation di chuyển của nhân vật
    {
        anim.SetFloat("forward", forward);
        anim.SetFloat("strafe", strafe);
    }

    // tăng tốc
    public void CharacterSprint( bool isSprinting)// animation chạy của nhân vật 
    {
        anim.SetBool("sprint", isSprinting);
    }

    //ngắm
    public void CharacterAim(bool isAiming)// animation ngắm bắn 
    {
        anim.SetBool("aim", isAiming);
    }

    //reload
    public void Reload(bool reload)// animation nạp đạn
    {
        if (reload && Weapon.currentAmmo < Weapon.maxAmmo && Weapon.alternativeAmmo > 0) anim.SetTrigger("reload");
    }

    //bắn
    public void CharacterShoot()// animation bắn
    {
        anim.SetTrigger("shoot");
    }

    //dao
    //public void CharacterStab()
    //{
    //    anim.SetTrigger("stab");
    //}


}
