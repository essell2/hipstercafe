using UnityEngine;
using System.Collections;

public class Table : GlobalRefs {

	public GameObject seat1;
	public GameObject seat2;
	public GameObject seat3;
	public GameObject seat4;

	public bool seat1IsFree = true;
	public bool seat2IsFree = true;
	public bool seat3IsFree = true;
	public bool seat4IsFree = true;
	public bool allSeatsFree = true;

	public GameObject dirty1;

	public bool isClean = true;
	public float distanceToPlayer = 999f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		distanceToPlayer = Vector3.Distance(gameObject.transform.position, player.transform.position);

		// ARE ALL SEATS FREE?
		if (seat1IsFree && seat2IsFree && seat3IsFree && seat4IsFree)
			allSeatsFree = true;
		else
			allSeatsFree = false;

		// IS CLEAN?
		if (isClean)
			dirty1.SetActive(false);
		else
			dirty1.SetActive(true);

		// BECOMES CLEAN IF PLAYER IS CLOSE
		if (distanceToPlayer < 5f && !isClean) 
		{
			audioRefsScript.snd_Pickup.Play();
			isClean = true;
		}
	}
}

