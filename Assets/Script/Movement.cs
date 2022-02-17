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
    public void CharacterMove(float strafe, float forward)
    {
        anim.SetFloat("forward", forward);
        anim.SetFloat("strafe", strafe);
    }

    // tăng tốc
    public void CharacterSprint( bool isSprinting)
    {
        anim.SetBool("sprint", isSprinting);
    }

    //ngắm
    public void CharacterAim(bool isAiming)
    {
        anim.SetBool("aim", isAiming);
    }

    //reload
    public void Reload(bool reload)
    {
        if (reload && Weapon.currentAmmo < Weapon.maxAmmo) anim.SetTrigger("reload");
    }

    //bắn
    public void CharacterShoot()
    {
        anim.SetTrigger("shoot");
    }

    //dao
    public void CharacterStab()
    {
        anim.SetTrigger("stab");
    }


}
