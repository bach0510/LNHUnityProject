using UnityEngine;
using System.Collections;

public class LightBehaviorAuto : MonoBehaviour {
	public AnimationCurve LightCurve;
	public float GraphScaleX, GraphScaleY;
	private float startTime;
	private Light lightSource;

	void Start(){
		lightSource = GetComponent<Light>();
		lightSource.intensity = 0f;
	}

	void OnEnable () {
		startTime = Time.time;

	}
	
	void Update () {
		if(Input.GetButtonDown("Fire1") && Input.GetMouseButton(1))
        {
			var time = Time.time - startTime;
			if (time > GraphScaleX)
				startTime = Time.time;
			var eval = LightCurve.Evaluate( GraphScaleX) / GraphScaleY;
			lightSource.intensity = 1f;
		}
        else
        {
			lightSource.intensity = 0f;
		}
	}
}
