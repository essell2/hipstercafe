using UnityEngine;
using System.Collections;

public class TrackingCamera : GlobalRefs {

	public bool trackPosition = false;
	public bool trackLookAtTarget = false;

	public GameObject cameraPosTarget;
	public GameObject cameraLookTarget;
	public float lerpSpeedPosition = 3f;
	public float lerpSpeedRotation = 3f;



	public GameObject loc_CameraTarget1;
	public GameObject loc_CameraTarget2;
	public GameObject loc_CameraTarget3;
	public GameObject loc_CameraTarget4;
	
	void Start ()
	{
	}
	
	
	
	void Update ()
	{
		if (trackLookAtTarget)
		{
			if (playerScript.isAtCounter)
				gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, cameraLookTarget.transform.rotation, Time.deltaTime * lerpSpeedRotation);
			else
				player.transform.LookAt (playerScript.moveTarget.transform.position);
		}
	}
	

	public void TurnLeft()
	{
		cameraLookTarget = loc_CameraTarget1;
		gameStateScript.currentCamera++;
		HUDScript.bt_TurnLeft.SetActive(false);
		HUDScript.bt_TurnRight.SetActive(true);

	}

	public void TurnRight()
	{
		cameraLookTarget = loc_CameraTarget2;
		gameStateScript.currentCamera = 2;
		HUDScript.bt_TurnLeft.SetActive(true);
		HUDScript.bt_TurnRight.SetActive(false);
	}
}
