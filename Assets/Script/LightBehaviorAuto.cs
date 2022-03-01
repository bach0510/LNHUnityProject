using UnityEngine;
using System.Collections;

public class LightBehaviorAuto : MonoBehaviour {
	private Light lightSource;

	void Start(){
		lightSource = GetComponent<Light>();// gán component Light
		lightSource.intensity = 0f;
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1") && Input.GetMouseButton(1) && Weapon.currentAmmo >0)// nếu nhấn nút bắn khi băng đạn lớn hơn 0 
        {
			lightSource.intensity = 2f;// set light 
		}
        else
        {
			lightSource.intensity = 0f;// giảm light
		}
	}
}
