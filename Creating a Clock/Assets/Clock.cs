using System.Collections;
using System.Collections.Generic;
using System; 
using UnityEngine;

public class Clock : MonoBehaviour {
	const float DEGREES_PER_HOUR = 30f; 
	const float DEGREES_PER_MINUTE = 6f; 
	const float DEGREES_PER_SECOND = 6f; 

	public Transform hoursTransform; 
	public Transform minutesTransform; 
	public Transform secondsTransform; 

	public bool continuous; 

	void Update() {
		if (continuous) {
			UpdateContinuous(); 
		}
		else {
			UpdateDiscrete(); 
		}
	}
	
	void UpdateContinuous() {
		TimeSpan time = DateTime.Now.TimeOfDay; 
		hoursTransform.localRotation = Quaternion.Euler(0f, (float) time.TotalHours * DEGREES_PER_HOUR, 0f); 
		minutesTransform.localRotation = Quaternion.Euler(0f, (float) time.TotalMinutes * DEGREES_PER_MINUTE, 0f); 
		secondsTransform.localRotation = Quaternion.Euler(0f, (float) time.TotalSeconds * DEGREES_PER_SECOND, 0f); 
	}

	void UpdateDiscrete() {
		DateTime time = DateTime.Now; 
		hoursTransform.localRotation = Quaternion.Euler(0f, time.Hour * DEGREES_PER_HOUR, 0f); 
		minutesTransform.localRotation = Quaternion.Euler(0f, time.Minute * DEGREES_PER_MINUTE, 0f); 
		secondsTransform.localRotation = Quaternion.Euler(0f, time.Second * DEGREES_PER_SECOND, 0f); 
	}
}
