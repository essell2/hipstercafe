using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : GlobalRefs {

	public bool isHoldingDrink = false;
	public string heldDrinkName;
	public float heldDrinkAge = 0f;

	public GameObject bt_CustomerInteract;
	public GameObject bt_CustomerInteract2;
	public GameObject text_CustomerInteract;
	public GameObject text_CustomerInteract2;

	public GameObject meshes;
	public GameObject mug;

	public bool isAtCounter = true;
	public GameObject moveTarget;
	public float distanceToMoveTarget;
	public float distanceToCounter;
	public float distanceBetweenMoveTargetAndCounter;
	public GameObject loc_Counter;


	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		// IS HOLDING A DRINK?
		if (isHoldingDrink && isAtCounter)
			mug.SetActive(true);
		else
			mug.SetActive(false);

		if (isHoldingDrink)
			heldDrinkAge += Time.deltaTime;

		// IS AT COUNTER?
		distanceToCounter = Vector3.Distance(gameObject.transform.position, loc_Counter.transform.position);
		distanceBetweenMoveTargetAndCounter = Vector3.Distance(moveTarget.transform.position, loc_Counter.transform.position);
		if (distanceToCounter < gameVarsScript.counterSnapDistance)
		{
			if (!isAtCounter)
			{
				isAtCounter = true;
				playerInputScript.ToggleTopdown();
			}
			if (distanceBetweenMoveTargetAndCounter < gameVarsScript.counterSnapDistance)
				moveTarget.transform.position = loc_Counter.transform.position;
		}
		else
			isAtCounter = false;

		// MOVING TO MOVETARGET
		distanceToMoveTarget = Vector3.Distance(gameObject.transform.position, moveTarget.transform.position);
		GetComponent<NavMeshAgent>().destination = moveTarget.transform.position;

		// CUSTOMER INTERACT BUTTON 1
		if (gameStateScript.currentCamera == 2)
		{
			if (gameStateScript.viewMode == "Counter" && gameStateScript.list_QueuedCustomers.Length > 0 && gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().isAtMoveTarget && !gameStateScript.dayComplete)
			{
				if (!gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().hasGivenOrder)
				{
					text_CustomerInteract.GetComponent<Text>().text = "Take Order";
					bt_CustomerInteract.SetActive(true);
				}
				
				else if (gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().hasGivenOrder && playerScript.isHoldingDrink)
				{
					text_CustomerInteract.GetComponent<Text>().text = "Give Drink";
					bt_CustomerInteract.SetActive(true);
				}
				else
					bt_CustomerInteract.SetActive(false);
			}
			else
				bt_CustomerInteract.SetActive(false);
		}
		else
			bt_CustomerInteract.SetActive(false);

		// CUSTOMER INTERACT BUTTON 2
		if (gameStateScript.currentCamera == 2)
		{
			if (gameStateScript.viewMode == "Counter" && gameStateScript.list_QueuedCustomers.Length > 1 && gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().isAtMoveTarget && !gameStateScript.dayComplete)
			{
				if (!gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().hasGivenOrder)
				{
					text_CustomerInteract2.GetComponent<Text>().text = "Take Order";
					bt_CustomerInteract2.SetActive(true);
				}
				
				else if (gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().hasGivenOrder && playerScript.isHoldingDrink)
				{
					text_CustomerInteract2.GetComponent<Text>().text = "Give Drink";
					bt_CustomerInteract2.SetActive(true);
				}
				else
					bt_CustomerInteract2.SetActive(false);
			}
			else
				bt_CustomerInteract2.SetActive(false);
		}
		else
			bt_CustomerInteract2.SetActive(false);
	}

	// CUSTOMER INTERACT
	public void CustomerInteract()
	{
		// TAKE ORDER
		if (!gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().hasGivenOrder)
		{
			gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().GiveOrder();
		}

		// GIVE DRINK
		else if (gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().hasGivenOrder && playerScript.isHoldingDrink)
		{
			gameStateScript.salesToday += 6;
			audioRefsScript.snd_Kerching.Play();
			isHoldingDrink = false;
			gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().receivedDrinkName = heldDrinkName;
			gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().receivedDrinkAge = heldDrinkAge;
			gameStateScript.list_QueuedCustomers[0].gameObject.GetComponent<Customer>().ReviewOrder();
		}
	}

	// CUSTOMER INTERACT 2
	public void CustomerInteract2()
	{
		// TAKE ORDER
		if (!gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().hasGivenOrder)
		{
			gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().GiveOrder();
		}
		
		// GIVE DRINK
		else if (gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().hasGivenOrder && playerScript.isHoldingDrink)
		{
			isHoldingDrink = false;			
			gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().receivedDrinkName = heldDrinkName;
			gameStateScript.list_QueuedCustomers[1].gameObject.GetComponent<Customer>().ReviewOrder();
		}
	}

	// TRASH DRINK
	public void TrashDrink()
	{
		isHoldingDrink = false;
		audioRefsScript.snd_Pickup.Play();
	}

	// RETURN TO COUNTER 
	public void ReturnToCounter()
	{
		playerScript.moveTarget.transform.position = playerScript.loc_Counter.transform.position;
		if (playerScript.isAtCounter)
			playerInputScript.ToggleTopdown();
	}
}
