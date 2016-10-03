using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CoffeeMachine : GlobalRefs {

	public GameObject bt_CoffeeMake;
	public GameObject bt_CoffeePickup;
	public GameObject text_CoffeeMake;
	public GameObject text_CoffeePickup;
	public string drinkName;

	public bool isMakingCoffee = false;
	public float makingCoffeeTicker = 0f;
	public bool coffeeReady = false;
	public float coffeeReadyTicker = 0f;
	public float drinkReadyTime = 4f;

	public AudioSource snd_CoffeeMaking;
	public AudioSource snd_CoffeeReady;

	public GameObject mug1;
	public GameObject mug2;
	public GameObject mug3;
	public GameObject mug4;
	public GameObject coffeeStream1;
	public GameObject coffeeStream2;
	public GameObject coffeeStream3;
	public GameObject coffeeStream4;

	void Start ()
	{
		if (drinkName == "Hot Chocolate")
			drinkReadyTime = gameVarsScript.timeToMakeHotChocolate;
		else if (drinkName == "Cappucino")
			drinkReadyTime = gameVarsScript.timeToMakeCappucino;
		else if (drinkName == "Espresso")
			drinkReadyTime = gameVarsScript.timeToMakeEspresso;

		text_CoffeeMake.GetComponent<Text>().text = "Make "+drinkName;
		text_CoffeePickup.GetComponent<Text>().text = "Pick Up "+drinkName;
	}

	void Update ()
	{
		if (isMakingCoffee)
			makingCoffeeTicker += Time.deltaTime;

		if (makingCoffeeTicker > drinkReadyTime)
			CoffeeReady();

		if (coffeeReady)
			coffeeReadyTicker += Time.deltaTime;

		// HUD STUFF

		
		// MAKE COFFEE BUTTON
		if (gameStateScript.currentCamera == 1 && gameStateScript.viewMode == "Counter")
		{
			if (!isMakingCoffee && !coffeeReady)
				bt_CoffeeMake.SetActive(true);
			else
				bt_CoffeeMake.SetActive(false);
			
			if (coffeeReady && !playerScript.isHoldingDrink)
				bt_CoffeePickup.SetActive(true);
			else
				bt_CoffeePickup.SetActive(false);
		}
		else
		{
			bt_CoffeeMake.SetActive(false);
			bt_CoffeePickup.SetActive(false);
		}
	}

	/*
	// UI BUTTON PRESS
	public void CoffeeInteract()
	{
		if (!coffeeReady && !isMakingCoffee)
			MakeCoffee();
		else if (coffeeReady)
			PickupCoffee();
	}*/

	// MAKE COFFEE
	public void MakeCoffee()
	{
		isMakingCoffee = true;
		mug3.SetActive(true);
		coffeeStream3.SetActive(true);
		snd_CoffeeMaking.Play();
		gameStateScript.costStockToday++;
	}
	
	// COFFEE READY
	public void CoffeeReady()
	{
		coffeeStream3.SetActive(false);
		makingCoffeeTicker = 0f;
		isMakingCoffee = false;
		coffeeReady = true;
		snd_CoffeeMaking.Stop();
		snd_CoffeeReady.Play();
	}

	// PICKUP COFFEE
	public void PickupCoffee()
	{
		audioRefsScript.snd_Pickup.Play();
		playerScript.isHoldingDrink = true;
		playerScript.heldDrinkName = drinkName;
		playerScript.heldDrinkAge = coffeeReadyTicker;
		mug3.SetActive(false);
		coffeeReady = false;
		coffeeReadyTicker = 0f;
	}
}


