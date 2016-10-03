using UnityEngine;
using System.Collections;

public class GlobalRefs : MonoBehaviour {

	[HideInInspector] public GameState gameStateScript;
	[HideInInspector] public GameVars gameVarsScript;
	[HideInInspector] public GameVarsSystem gameVarsSystemScript;
	[HideInInspector] public GameVarsCampaign gameVarsCampaignScript;

	[HideInInspector] public Campaign campaignScript;
	[HideInInspector] public PrefabRefs prefabRefsScript;
	[HideInInspector] public AudioRefs audioRefsScript;
	[HideInInspector] public HUD HUDScript;

	[HideInInspector] public GameObject loc_Zero;
	[HideInInspector] public GameObject loc_Queue1;
	[HideInInspector] public GameObject loc_Spawn;
	[HideInInspector] public GameObject loc_Despawn;
	[HideInInspector] public GameObject player;
	[HideInInspector] public GameObject campaignBox;

	[HideInInspector] public Player playerScript;
	[HideInInspector] public PlayerInput playerInputScript;
	[HideInInspector] public CoffeeMachine coffeeMachineScript;
	[HideInInspector] public MusicManager musicManagerScript;



	void Awake ()
	{
		gameVarsScript = GameObject.FindGameObjectWithTag("GameVarsBox").GetComponent<GameVars>();
		gameVarsSystemScript = GameObject.FindGameObjectWithTag("GameVarsSystemBox").GetComponent<GameVarsSystem>();
		gameVarsCampaignScript = GameObject.FindGameObjectWithTag("GameVarsCampaignBox").GetComponent<GameVarsCampaign>();

		gameStateScript = GameObject.FindGameObjectWithTag("GameStateBox").GetComponent<GameState>();
		prefabRefsScript = GameObject.FindGameObjectWithTag("PrefabRefsBox").GetComponent<PrefabRefs>();
		audioRefsScript = GameObject.FindGameObjectWithTag("AudioRefsBox").GetComponent<AudioRefs>();
		HUDScript = GameObject.FindGameObjectWithTag("HUDBox").GetComponent<HUD>();
		loc_Zero = gameObject;
		loc_Spawn = GameObject.FindGameObjectWithTag("loc_Spawn");
		loc_Despawn = GameObject.FindGameObjectWithTag("loc_Despawn");

		player = GameObject.FindGameObjectWithTag("Player");
		playerScript = player.GetComponent<Player>();
		playerInputScript = GameObject.FindGameObjectWithTag("PlayerInputBox").GetComponent<PlayerInput>();
		coffeeMachineScript = GameObject.FindGameObjectWithTag("CoffeeMachine").GetComponent<CoffeeMachine>();
		musicManagerScript = GameObject.FindGameObjectWithTag("MusicManager").GetComponent<MusicManager>();


		// CAMPAIGN BOX
		if (GameObject.FindGameObjectWithTag("CampaignBox") != null)
			campaignBox = GameObject.FindGameObjectWithTag("CampaignBox");
		
		if (campaignBox == null)
			Instantiate(prefabRefsScript.pref_CampaignBox, loc_Zero.transform.position, loc_Zero.transform.rotation);
		campaignScript = GameObject.FindGameObjectWithTag("CampaignBox").GetComponent<Campaign>();

	}



}
