using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameState : GlobalRefs
{
	public GameObject playerInputBox;

	public GameObject[] list_QueueSpots;
	public GameObject[] list_Customers;
	public GameObject[] list_QueuedCustomers;
	public GameObject[] list_Tables;
	public GameObject[] list_Seats;

	public GameObject CameraBoxMain;
	public GameObject CameraBoxTopdown;
	
	public bool displayHelp = true;
	public bool autoServeMode = false;

	public int startBalance;
	public int salesToday = 0;
	public int tipsToday = 0;
	public int customersServedToday = 0;
	public int badOrdersToday = 0;
	public int unseatedCustomersToday = 0;

	public int costElectricityToday = 40;
	public int costSuppliesToday = 4;
	public int costStockToday = 0;

	public float timeRemaining = 999f;
	public float timeRemainingInitial;
	public float timeUntilNextCustomer = 0f;
	public float timeDayHasBeenOver = 0f;


	public int currentCamera = 2;

	public string viewMode = "Counter";

	public bool dayStarted = true;
	public bool dayComplete = false;





	void Start()
	{
		startBalance = campaignScript.totalBalance;
		timeRemaining = gameVarsScript.dayLength;
		timeRemainingInitial = timeRemaining;
		list_Tables = GameObject.FindGameObjectsWithTag("Table");
		list_Seats = GameObject.FindGameObjectsWithTag("Seat");
	}

	void Update()
	{
		list_Customers = GameObject.FindGameObjectsWithTag("Customer");
		list_QueuedCustomers = GameObject.FindGameObjectsWithTag("QueuedCustomer");

		if (dayComplete)
			timeDayHasBeenOver += Time.deltaTime;

		if (dayStarted && !dayComplete)
		{
			// SPAWN NEW CUSTOMER
			timeUntilNextCustomer -= Time.deltaTime;
			if (timeUntilNextCustomer < 0f)
				SpawnNewCustomer();

			// TIME TICKS DOWN
			timeRemaining -= Time.deltaTime;
			if (timeRemaining < 0.2f && !dayComplete)
			{
				EndDay();
			}
		}
		else if (dayComplete)
		{
			HUDScript.DisplayEndOfDay();
		}
	}


	// UPDATE QUEUE
	public void UpdateQueue()
	{
		foreach (GameObject thisQueueSpot in gameStateScript.list_QueueSpots)
		{
			thisQueueSpot.GetComponent<QueueSpot>().isTaken = false;
		}
		int n = 0;
		foreach (GameObject thisCustomer in gameStateScript.list_QueuedCustomers)
		{
				gameStateScript.list_QueuedCustomers[n].GetComponent<Customer>().currentQueueSpot = list_QueueSpots[n];
				gameStateScript.list_QueuedCustomers[n].GetComponent<Customer>().moveTarget = thisCustomer.GetComponent<Customer>().currentQueueSpot;
				thisCustomer.GetComponent<Customer>().currentQueueSpot.GetComponent<QueueSpot>().isTaken = true;
				n++;
		}
		//For each queuedCustomer, while x < queueSpots.length, see if is empty, and if so, assign this customer to it and break
	}
	

	// SPAWN NEW CUSTOMER
	public void SpawnNewCustomer()
	{
		if (list_Customers.Length < list_QueueSpots.Length)
		{
			Instantiate(prefabRefsScript.Customer, loc_Spawn.transform.position, loc_Spawn.transform.rotation);
			list_QueuedCustomers = GameObject.FindGameObjectsWithTag("QueuedCustomer");
			timeUntilNextCustomer = Random.Range(gameVarsScript.customerSpawnTimeMin, gameVarsScript.customerSpawnTimeMax);

			if (timeRemaining > (timeRemainingInitial - gameVarsScript.customerDoubleSpawnTime))
				timeUntilNextCustomer = timeUntilNextCustomer / 2f;

		}
	}



	// END DAY
	public void EndDay()
	{
		musicManagerScript.LowerVolume();

		timeRemaining = 0f;
		dayComplete = true;
		campaignScript.totalCustomersServed += customersServedToday;
		campaignScript.totalBadOrders += badOrdersToday;
		campaignScript.totalUnseatedCustomers += unseatedCustomersToday;

		if (gameStateScript.viewMode == "Counter")
			playerInputScript.ToggleTopdown();

		foreach (GameObject thisCustomer in gameStateScript.list_QueuedCustomers)
		{
			if (!thisCustomer.GetComponent<Customer>().isLeaving)
				thisCustomer.GetComponent<Customer>().LeaveCafe();
		}


	}

	// NIGHT MODE
	public void NightMode()
	{
		HUDScript.UpdateNightStats();
		viewMode = "Phone";
	}

	// NEXT DAY 
	public void NextDay()
	{
		campaignScript.currentDay++;
		musicManagerScript.StopAllTracks();
		Invoke("SceneRestart", 1f);
	}

	// SCENE RESTART
	public void SceneRestart()
	{
		Application.LoadLevel (Application.loadedLevel);
	}
}
