using UnityEngine;
using System.Collections;

public class AutoRotate : MonoBehaviour {

	public float rotateSpeed = 250f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
	}
}
