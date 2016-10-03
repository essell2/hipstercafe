using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : GlobalRefs {

	public GameObject text_TL;
	public GameObject endOfDay;
	public GameObject text_EndOfDay;
	public GameObject text_TC;

	public GameObject bt_TurnLeft;
	public GameObject bt_TurnRight;
	public GameObject bt_Serve;
	public GameObject bt_Topdown;
	public GameObject bt_ReturnToCounter;
	public GameObject bt_Continue;
	public GameObject bt_TrashDrink;
	public GameObject text_DrinkIsReallyCold;
	
	public GameObject CoffeeMachine1;
	public GameObject CoffeeMachine2;
	public GameObject CoffeeMachine3;
	public GameObject bt_CoffeeMachine1;
	public GameObject bt_CoffeeMachine2;
	public GameObject bt_CoffeeMachine3;

	// PHONE MODE
	public GameObject scr_Phone;
	public GameObject text_PhoneStats;

	
	void Start()
	{
		bt_Continue.SetActive(false);
		endOfDay.SetActive(false);
	}

	void Update ()
	{
		text_TL.GetComponent<Text>().text = "Sales: $"+gameStateScript.salesToday;
		text_TL.GetComponent<Text>().text += "\nTips: $"+gameStateScript.tipsToday;
		text_TL.GetComponent<Text>().text += "\n"+gameStateScript.timeRemaining.ToString("0");


		// HANDLE TURN LEFT AND RIGHT BUTTONS
		if (gameStateScript.currentCamera < 3)
			bt_TurnRight.SetActive(true);
		else
			bt_TurnRight.SetActive(false);

		if (gameStateScript.currentCamera > 1)
			bt_TurnLeft.SetActive(true);
		else
			bt_TurnLeft.SetActive(false);


		// BUTTON TOPDOWN 
		if (gameStateScript.viewMode == "Counter" && !gameStateScript.dayComplete)
			bt_Topdown.SetActive(true);
		else
			bt_Topdown.SetActive(false);

		if (gameStateScript.viewMode == "Topdown" && !gameStateScript.dayComplete)
			bt_ReturnToCounter.SetActive(true);
		else
			bt_ReturnToCounter.SetActive(false);

		if (gameStateScript.dayComplete && gameStateScript.timeDayHasBeenOver > 2f)
			bt_Continue.SetActive(true);

		if (gameStateScript.viewMode == "Phone")
			HUDScript.scr_Phone.SetActive(true);
		else
			HUDScript.scr_Phone.SetActive(false);

		// TRASH DRINK BUTTON
		if (playerScript.isHoldingDrink && gameStateScript.viewMode == "Counter")
		{
			bt_TrashDrink.SetActive(true);
			if (playerScript.heldDrinkAge > (gameVarsScript.coldDrinkAge * 2))
				text_DrinkIsReallyCold.SetActive(true);
			else
				text_DrinkIsReallyCold.SetActive(false);
		}
		else
		{
			text_DrinkIsReallyCold.SetActive(false);
			bt_TrashDrink.SetActive(false);
		}
	}

	public void DisplayEndOfDay()
	{
		text_EndOfDay.GetComponent<Text>().text =
			"CLOSING TIME!\n\n\n\n"+
			"Customers served: "+gameStateScript.customersServedToday+
			"\n\nToday's sales: $"+gameStateScript.salesToday+
			"\nToday's tips: $"+gameStateScript.tipsToday+
			"\n\nBad Orders: "+gameStateScript.badOrdersToday+
			"\nUnseated Customers: "+gameStateScript.unseatedCustomersToday;
		endOfDay.SetActive(true);
		text_TL.SetActive(false);
	}

	public void UpdateNightStats()
	{
		text_PhoneStats.GetComponent<Text>().text = "";
		if (campaignScript.currentDay < gameVarsScript.totalDays)
			text_PhoneStats.GetComponent<Text>().text += "AFTER DAY "+campaignScript.currentDay+"\n\n";
		else
			text_PhoneStats.GetComponent<Text>().text += "END OF THE WEEK!\n\n";

		campaignScript.totalBalance -= gameStateScript.costElectricityToday;
		campaignScript.totalBalance -= gameStateScript.costSuppliesToday;
		campaignScript.totalBalance -= gameStateScript.costStockToday;

		text_PhoneStats.GetComponent<Text>().text +=
			"Previous bank balance: $"+(gameStateScript.startBalance)+"\n\n" +
			"Today's sales: $"+gameStateScript.salesToday+"\n" +
				"Today's tips: $"+gameStateScript.tipsToday+"\n" +
				"Today's costs: -$"+(gameStateScript.costElectricityToday + gameStateScript.costSuppliesToday + gameStateScript.costStockToday)+"\n\n" +
				"New bank balance: $"+campaignScript.totalBalance+"\n\n" +

				"Total customers served: "+campaignScript.totalCustomersServed+"\n" +
				"Total mistaken orders: "+campaignScript.totalBadOrders+"\n"+
				"Total unseated customers: "+campaignScript.totalUnseatedCustomers+"\n\n"+

				"Likes on Hipstrgram: "+campaignScript.totalLikes+"";
	}

}



