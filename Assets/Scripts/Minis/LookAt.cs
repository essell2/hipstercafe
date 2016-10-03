using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {

	public GameObject lookAtCamera;
	public GameObject lookAtTarget;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		lookAtCamera.transform.LookAt(lookAtTarget.transform.position);
	}
}
