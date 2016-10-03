using UnityEngine;
using System.Collections;

public class PlayerInput : GlobalRefs
{
	public Camera TopdownCamera;
	public LayerMask floorLayerMask;
	public GameObject cursorLight;
	public RaycastHit hit;

	void Start ()
	{

	}

	void Update ()
	{
		// SPAWN NEW CUSTOMER
		if (Input.GetKeyDown(KeyCode.Space))
			gameStateScript.SpawnNewCustomer();

		if (Input.GetKeyDown(KeyCode.Tab))
			ToggleTopdown();

		// CLICKING TO MOVE
		Ray ray = TopdownCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out hit) && Input.GetKeyDown(KeyCode.Mouse0) && gameStateScript.viewMode == "Topdown" && Vector3.Distance(playerScript.moveTarget.transform.position, hit.point) > 0.2f && hit.collider.gameObject.tag != "BlockMoveClick" && !gameStateScript.dayComplete)
		{
			Debug.DrawLine(ray.origin, hit.point);
			Debug.Log("(PLAYERINPUT) rayTrace HIT: "+hit.collider.gameObject.name);
			playerScript.moveTarget.transform.position = hit.point;
			if (Vector3.Distance(playerScript.moveTarget.transform.position, playerScript.loc_Counter.transform.position) < gameVarsScript.counterSnapDistance)
				playerScript.moveTarget.transform.position = playerScript.loc_Counter.transform.position;
		}
	}

	// TOGGLE TOPDOWN
	public void ToggleTopdown()
	{
		if (gameStateScript.viewMode == "Topdown" && playerScript.isAtCounter)
		{
			gameStateScript.viewMode = "Counter";
			gameStateScript.CameraBoxTopdown.GetComponent<Camera>().depth = -10;
			gameStateScript.currentCamera = 2;
			UpdateCameraDirection();
		}
		else
		{
			HUDScript.bt_TurnLeft.SetActive(false);
			HUDScript.bt_TurnRight.SetActive(false);
			gameStateScript.viewMode = "Topdown";
			gameStateScript.CameraBoxTopdown.GetComponent<Camera>().depth = 10;
		}
	}


	// TURN CAMERA
	public void TurnLeft()
	{
		Debug.Log("(PLAYERINPUT) TurnLeft()");
		gameStateScript.currentCamera--;
		UpdateCameraDirection();
	}

	public void TurnRight()
	{
		Debug.Log("(PLAYERINPUT) TurnRight()");
		gameStateScript.currentCamera++;
		UpdateCameraDirection();
	}

	public void UpdateCameraDirection()
	{
		if (gameStateScript.currentCamera == 1)
			gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().cameraLookTarget = gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().loc_CameraTarget1;
		else if (gameStateScript.currentCamera == 2)
			gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().cameraLookTarget = gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().loc_CameraTarget2;
		else if (gameStateScript.currentCamera == 3)
			gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().cameraLookTarget = gameStateScript.CameraBoxMain.GetComponent<TrackingCamera>().loc_CameraTarget3;
	}
}
