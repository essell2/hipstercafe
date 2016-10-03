using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour {



	void Start ()
	{
		Vector3 euler = transform.eulerAngles;
		euler.y = Random.Range(0f, 360f);
		transform.eulerAngles = euler;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
