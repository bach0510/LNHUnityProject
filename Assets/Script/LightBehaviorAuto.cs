using UnityEngine;
using System.Collections;

public class LightBehaviorAuto : MonoBehaviour {
	private Light lightSource;

	void Start(){
		lightSource = GetComponent<Light>();
		lightSource.intensity = 0f;
	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1") && Input.GetMouseButton(1) && Weapon.currentAmmo >0)
        {
			lightSource.intensity = 2f;
		}
        else
        {
			lightSource.intensity = 0f;
		}
	}
}
