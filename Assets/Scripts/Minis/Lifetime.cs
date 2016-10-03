using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour {

	public float lifetimeLength;
	public float lifetimeTicker;
	public bool addRandomness = true;
	public float randomElement;

	// Use this for initialization
	void Start ()
	{
		lifetimeTicker = lifetimeLength;
		randomElement = lifetimeLength / 2f;

		if (addRandomness)
		{
			lifetimeTicker = lifetimeTicker + Random.Range(0F, randomElement);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		lifetimeTicker -= Time.fixedDeltaTime;

		if (lifetimeTicker < 0)
		{
			Destroy (gameObject);
		}
	}
}
