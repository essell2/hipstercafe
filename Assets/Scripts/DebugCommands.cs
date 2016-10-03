using UnityEngine;
using System.Collections;

public class DebugCommands : GlobalRefs
{



	void Start ()
	{

	}
	
	
	void Update ()
	{
		// RESTART SCENE
		if(Input.GetKeyDown(KeyCode.Backspace))
		{
			Application.LoadLevel (Application.loadedLevel);
		}


		// SLOW MOTION
		if(Input.GetKeyDown(KeyCode.Keypad0))
			Time.timeScale = 0.4f;
		if(Input.GetKeyDown(KeyCode.Keypad1))
			Time.timeScale = 1f;
		if(Input.GetKeyDown(KeyCode.Keypad2))
			Time.timeScale = 2f;
		if(Input.GetKeyDown(KeyCode.Keypad3))
			Time.timeScale = 3f;
		if(Input.GetKeyDown(KeyCode.Keypad4))
			Time.timeScale = 4f;
		if(Input.GetKeyDown(KeyCode.Keypad5))
			Time.timeScale = 5f;
		if(Input.GetKeyDown(KeyCode.Keypad6))
			Time.timeScale = 6f;
		if(Input.GetKeyDown(KeyCode.Keypad9))
			Time.timeScale = 9f;


		// PAUSE EDITOR
		/*
		if(Input.GetButtonDown("Select"))
		{
			Debug.Break();
		}
		*/
	}
}
