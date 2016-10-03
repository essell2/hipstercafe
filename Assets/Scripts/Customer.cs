using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Customer : GlobalRefs {

	public GameObject mug;

	public GameObject moveTarget;
	public GameObject currentQueueSpot;
	public GameObject seat;

	// RANDOMISED CHARACTER
	public bool isMale;
	public GameObject phone;
	public GameObject glasses;
	public GameObject beard;
	public GameObject shirt;
	public GameObject boobs;
	public GameObject head;
	public GameObject legs;
	public GameObject hair;
	public int phoneColor;
	public int hairColor;
	public int glassesColor;
	public int shirtColor;
	public int legsColor;

	public GameObject speechBubble;
	public GameObject text_Speech;
	public string drinkRequest;
	public string receivedDrinkName;
	public float receivedDrinkAge;

	public float timeAlive = 0f;
	public float timeHasHadDrink = 0f;
	public int happiness = 7;
	public int tipAmount = 0;

	public float distanceToMoveTarget = 999f;
	public bool isAtMoveTarget = false;
	public bool isQueuing = true;
	public bool isLeaving = false;
	public bool hasGivenOrder = false;
	public bool hasDrink = false;
	public bool hasSeat = false;

	public AudioSource snd_NeutralMale1;
	public AudioSource snd_NeutralMale2;
	public AudioSource snd_NeutralMale3;
	public AudioSource snd_NeutralMale4;
	public AudioSource snd_NeutralMale5;
	public AudioSource snd_NeutralFemale1;
	public AudioSource snd_NeutralFemale2;
	public AudioSource snd_NeutralFemale3;
	public AudioSource snd_NeutralFemale4;
	public AudioSource snd_NeutralFemale5;

	void Start () {

		// GENERATE A HIPSTER
		if (Random.value < 0.5f)
			isMale = true;
		else
			isMale = false;

		if (isMale)
		{
			if (Random.value < 0.6f)
				beard.SetActive(true);
		}

		// HAPPINESS
		happiness = Random.Range(4,8);

		// Colors
		/*
		hairColor = Random.Range(1,6);
		if (hairColor == 1) hair.GetComponent<Renderer>().material.color = prefabRefsScript.hair1;
		if (hairColor == 2) hair.GetComponent<Renderer>().material.color = prefabRefsScript.hair2;
		if (hairColor == 3) hair.GetComponent<Renderer>().material.color = prefabRefsScript.hair3;
		if (hairColor == 4) hair.GetComponent<Renderer>().material.color = prefabRefsScript.hair4;
		if (hairColor == 5) hair.GetComponent<Renderer>().material.color = prefabRefsScript.hair5;
		*/
		phoneColor = Random.Range(1,6);
		if (phoneColor == 1) phone.GetComponent<Renderer>().material.color = prefabRefsScript.phone1;
		if (phoneColor == 2) phone.GetComponent<Renderer>().material.color = prefabRefsScript.phone2;
		if (phoneColor == 3) phone.GetComponent<Renderer>().material.color = prefabRefsScript.phone3;
		if (phoneColor == 4) phone.GetComponent<Renderer>().material.color = prefabRefsScript.phone4;
		if (phoneColor == 5) phone.GetComponent<Renderer>().material.color = prefabRefsScript.phone5;
		if (Random.value < 0.6f) phone.SetActive(false);

		glassesColor = Random.Range(1,6);
		if (glassesColor == 1) glasses.GetComponent<Renderer>().material.color = prefabRefsScript.glasses1;
		if (glassesColor == 2) glasses.GetComponent<Renderer>().material.color = prefabRefsScript.glasses2;
		if (glassesColor == 3) glasses.GetComponent<Renderer>().material.color = prefabRefsScript.glasses3;
		if (glassesColor == 4) glasses.GetComponent<Renderer>().material.color = prefabRefsScript.glasses4;
		if (glassesColor == 5) glasses.GetComponent<Renderer>().material.color = prefabRefsScript.glasses5;
		if (Random.value < 0.4f) glasses.SetActive(false);
		
		shirtColor = Random.Range(1,6);
		if (shirtColor == 1) shirt.GetComponent<Renderer>().material.color = prefabRefsScript.shirt1;
		if (shirtColor == 2) shirt.GetComponent<Renderer>().material.color = prefabRefsScript.shirt2;
		if (shirtColor == 3) shirt.GetComponent<Renderer>().material.color = prefabRefsScript.shirt3;
		if (shirtColor == 4) shirt.GetComponent<Renderer>().material.color = prefabRefsScript.shirt4;
		if (shirtColor == 5) shirt.GetComponent<Renderer>().material.color = prefabRefsScript.shirt5;

		if (!isMale)
		{
			boobs.SetActive(true);
			if (shirtColor == 1) boobs.GetComponent<Renderer>().material.color = prefabRefsScript.shirt1;
			if (shirtColor == 2) boobs.GetComponent<Renderer>().material.color = prefabRefsScript.shirt2;
			if (shirtColor == 3) boobs.GetComponent<Renderer>().material.color = prefabRefsScript.shirt3;
			if (shirtColor == 4) boobs.GetComponent<Renderer>().material.color = prefabRefsScript.shirt4;
			if (shirtColor == 5) boobs.GetComponent<Renderer>().material.color = prefabRefsScript.shirt5;
		}

		legsColor = Random.Range(1,6);
		if (legsColor == 1) legs.GetComponent<Renderer>().material.color = prefabRefsScript.legs1;
		if (legsColor == 2) legs.GetComponent<Renderer>().material.color = prefabRefsScript.legs2;
		if (legsColor == 3) legs.GetComponent<Renderer>().material.color = prefabRefsScript.legs3;
		if (legsColor == 4) legs.GetComponent<Renderer>().material.color = prefabRefsScript.legs4;
		if (legsColor == 5) legs.GetComponent<Renderer>().material.color = prefabRefsScript.legs5;


		mug.SetActive(false);
		currentQueueSpot = gameStateScript.list_QueueSpots[gameStateScript.list_QueuedCustomers.Length-1];
		moveTarget = currentQueueSpot;
		gameStateScript.list_QueueSpots[gameStateScript.list_QueuedCustomers.Length-1].GetComponent<QueueSpot>().isTaken = true;

		// WHICH DRINK?
		float randomDrinkFloat = Random.value;
		if (randomDrinkFloat < 0.35f)
			drinkRequest = "Hot Chocolate";
		else if (randomDrinkFloat < 0.75f)
			drinkRequest = "Cappucino";
		else if (randomDrinkFloat < 1f)
			drinkRequest = "Espresso";

		// WHICH PHRASE?
		float randomSpeechFloat = Random.value;
		if (randomSpeechFloat < 0.3f)
			text_Speech.GetComponent<TextMesh>().text = "\""+drinkRequest+", please.\"";
		else if (randomSpeechFloat < 0.7f)
			text_Speech.GetComponent<TextMesh>().text = "\""+drinkRequest+"?\"";
		else if (randomSpeechFloat < 1f)
			text_Speech.GetComponent<TextMesh>().text = "\"Hmm... "+drinkRequest+", please?\"";

	}

	void Update ()
	{
		timeAlive += Time.deltaTime;

		// AUTO SERVE
		if (gameStateScript.autoServeMode && timeAlive > 10f && !hasDrink)
			AutoServe();



		if (moveTarget != null)
			distanceToMoveTarget = Vector3.Distance(gameObject.transform.position, moveTarget.transform.position);

		// HAS DRINK TICKER
		if (hasDrink)
			timeHasHadDrink += Time.deltaTime;

		if (timeHasHadDrink > 30f && !isLeaving)
			LeaveCafe();


		// IS AT MOVETARGET?
		if (distanceToMoveTarget > 1f)
		{
			isAtMoveTarget = false;
			GetComponent<NavMeshAgent>().destination = moveTarget.transform.position;
		}
		else
		{
			isAtMoveTarget = true;



			// IF IS AT MOVE TARGET
			if (distanceToMoveTarget < 1f)
			{
				// Look at player if is at front of queue
				if (moveTarget == gameStateScript.list_QueueSpots[0] || moveTarget == gameStateScript.list_QueueSpots[1])
					gameObject.transform.LookAt(player.transform.position);

				// Despawn point
				else if (moveTarget == loc_Despawn)
					Destroy(gameObject);
			}
		}


	}


	// GIVE ORDER
	public void GiveOrder()
	{
		speechBubble.SetActive(true);
		hasGivenOrder = true;
		phone.SetActive(false);

		// RANDOM BARK
		float randomBarkFloat1 = Random.value;
		if (isMale)
		{
			if (randomBarkFloat1 < 0.2f)
				snd_NeutralMale1.Play();
			else if (randomBarkFloat1 < 0.4f)
				snd_NeutralMale2.Play();
			else if (randomBarkFloat1 < 0.6f)
				snd_NeutralMale3.Play();
			else if (randomBarkFloat1 < 0.8f)
				snd_NeutralMale4.Play();
			else if (randomBarkFloat1 < 1f)
				snd_NeutralMale5.Play();
		}
		else
		{
			if (randomBarkFloat1 < 0.2f)
				snd_NeutralFemale1.Play();
			else if (randomBarkFloat1 < 0.4f)
				snd_NeutralFemale2.Play();
			else if (randomBarkFloat1 < 0.6f)
				snd_NeutralFemale3.Play();
			else if (randomBarkFloat1 < 0.8f)
				snd_NeutralFemale4.Play();
			else if (randomBarkFloat1 < 1f)
				snd_NeutralFemale5.Play();
		}
	}

	// REVIEW ORDER
	public void ReviewOrder()
	{
		gameStateScript.salesToday += 6;
		campaignScript.totalBalance += 6;
		audioRefsScript.snd_Kerching.Play();
		hasDrink = true;
		mug.SetActive(true);
		gameStateScript.customersServedToday++;

		if (receivedDrinkName != drinkRequest && receivedDrinkAge > gameVarsScript.coldDrinkAge)
		{
			text_Speech.GetComponent<TextMesh>().text = "Not what I wanted and cold :(";
			happiness -= 5;
			Invoke("LeaveCafe", 2f);
			gameStateScript.badOrdersToday++;
			if (Random.value < 0.5f && campaignScript.totalLikes > 10) campaignScript.totalLikes--;
		}

		else if (receivedDrinkName != drinkRequest)
		{
			text_Speech.GetComponent<TextMesh>().text = "Huh? I said "+drinkRequest+"...";
			happiness -= 3;
			Invoke("FindSeat", 2f);
			gameStateScript.badOrdersToday++;
			if (Random.value < 0.3f && campaignScript.totalLikes > 10) campaignScript.totalLikes--;
		}
		else if (receivedDrinkAge > gameVarsScript.coldDrinkAge)
		{
			text_Speech.GetComponent<TextMesh>().text = "It's cold!";
			happiness -= 3;
			Invoke("FindSeat", 2f);
			gameStateScript.badOrdersToday++;
			if (Random.value < 0.3f && campaignScript.totalLikes > 10) campaignScript.totalLikes--;
		}
		else
		{
			text_Speech.GetComponent<TextMesh>().text = "Thanks!";
			Invoke("FindSeat", 1f);
			if (Random.value < 0.7f) campaignScript.totalLikes++;
		}
	}

	// AUTO SERVE
	public void AutoServe()
	{
		gameStateScript.costStockToday++;
		GiveOrder();
		ReviewOrder();
	}

	// FIND SEAT
	public void FindSeat()
	{
		if (Random.value < 0.4f) phone.SetActive(true);

		bool hasSeat = false;
		foreach (GameObject thisTable in gameStateScript.list_Tables)
		{
			Debug.Log("(CUSTOMER) FindSeat ForEach log 1");
			if (thisTable.GetComponent<Table>().isClean && thisTable.GetComponent<Table>().allSeatsFree)
			{
			 //	Debug.Log("(CUSTOMER) FindSeat ForEach log 2");
			thisTable.GetComponent<Table>().seat1IsFree = false;
			UpdateMoveTarget(thisTable.GetComponent<Table>().seat1);
			hasSeat = true;
			SitDown();
			}

			if (hasSeat)
			{
				Debug.Log("(CUSTOMER) FindSeat ForEach log 3 (hasSeat = true)");
				break;
			}
		}

		if (!hasSeat)
		{
			foreach (GameObject thisTable in gameStateScript.list_Tables)
			{
				Debug.Log("(CUSTOMER) FindSeat ForEach log 1");
				if (thisTable.GetComponent<Table>().isClean && thisTable.GetComponent<Table>().seat1IsFree)
				{
					//	Debug.Log("(CUSTOMER) FindSeat ForEach log 2");
					thisTable.GetComponent<Table>().seat1IsFree = false;
					UpdateMoveTarget(thisTable.GetComponent<Table>().seat1);
					hasSeat = true;
					SitDown();
				}
				else if (thisTable.GetComponent<Table>().isClean && thisTable.GetComponent<Table>().seat2IsFree)
				{
					thisTable.GetComponent<Table>().seat2IsFree = false;
					UpdateMoveTarget(thisTable.GetComponent<Table>().seat2);
					hasSeat = true;
					SitDown();
				}
				if (hasSeat)
				{
					Debug.Log("(CUSTOMER) FindSeat ForEach log 3 (hasSeat = true)");
					break;
				}
			}
		}



		if (!hasSeat)
		{
			Debug.Log("(CUSTOMER) Customer FAILED TO FIND SEAT!");
			happiness -= 3;
			gameStateScript.unseatedCustomersToday++;
			LeaveCafe();
		}
	}

	// UPDATE MOVETARGET
	public void UpdateMoveTarget(GameObject newMoveTarget)
	{
		seat = newMoveTarget;
		moveTarget = newMoveTarget;
		GetComponent<NavMeshAgent>().destination = newMoveTarget.transform.position;
		distanceToMoveTarget = Vector3.Distance(gameObject.transform.position, moveTarget.transform.position);
	}

	// SIT DOWN
	public void SitDown()
	{
		if (Random.value < 0.4f) phone.SetActive(true);

		speechBubble.SetActive(false);
		currentQueueSpot.GetComponent<QueueSpot>().isTaken = false;
		//currentQueueSpot = null;
		isQueuing = false;
		gameObject.tag = "Untagged";
		gameStateScript.list_QueuedCustomers = GameObject.FindGameObjectsWithTag("QueuedCustomer");
		gameStateScript.UpdateQueue();
	}

	// LEAVE CAFE
	public void LeaveCafe()
	{
		gameObject.tag = "Customer";
		isQueuing = false;
		isLeaving = true;
		gameStateScript.list_QueuedCustomers = GameObject.FindGameObjectsWithTag("QueuedCustomer");


		if (seat != null)
		{
			seat.transform.parent.GetComponent<Table>().isClean = false;

			if (seat = seat.transform.parent.GetComponent<Table>().seat1)
				seat.transform.parent.GetComponent<Table>().seat1IsFree = true;
			if (seat = seat.transform.parent.GetComponent<Table>().seat2)
				seat.transform.parent.GetComponent<Table>().seat2IsFree = true;
		}

		if (Random.value < 0.4f) phone.SetActive(true);
		mug.SetActive(false);
		moveTarget = loc_Despawn;
		GetComponent<NavMeshAgent>().destination = loc_Despawn.transform.position;
		distanceToMoveTarget = Vector3.Distance(gameObject.transform.position, loc_Despawn.transform.position);
		gameStateScript.UpdateQueue();
		LeaveTip();
	}

	// LEAVE TIP
	public void LeaveTip()
	{
		tipAmount = (happiness - 3);
		if (tipAmount < 0) tipAmount = 0;

		gameStateScript.tipsToday += tipAmount;
		campaignScript.totalTips += tipAmount;
		campaignScript.totalBalance += tipAmount;
	}
}
