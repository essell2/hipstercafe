using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

	// MUSIC MANAGER
	public GameObject[] list_Music;
	public bool musicStarted = false;
	public int randomTrackNumber = 1;
	public GameObject MenuMusic;
	
	void Start()
	{
		list_Music = GameObject.FindGameObjectsWithTag("Music");
		RestoreVolume();
		StartRandomTrack();
	}
	
	
	// MUSIC - STOP ALL TRACKS
	public void StopAllTracks()
	{
		//MenuMusic.GetComponent<AudioSource>().Stop();
		
		foreach (GameObject thisObject in list_Music)
			thisObject.GetComponent<AudioSource>().Stop();
	}
	
	// MUSIC - START RANDOM TRACK
	public void StartRandomTrack()
	{
		StopAllTracks();
		if (Application.loadedLevelName == "MainMenu")
			MenuMusic.GetComponent<AudioSource>().Play();
		else
		{
			randomTrackNumber = Random.Range(0, (list_Music.Length-1));
			list_Music[randomTrackNumber].GetComponent<AudioSource>().Play ();
		}
		
		musicStarted = true;
	}
	
	// MUSIC - START MENU MUSIC
	public void StartMenuMusic()
	{
		StopAllTracks();
		MenuMusic.GetComponent<AudioSource>().Play();
		musicStarted = true;
	}

	// MUSIC - LOWER VOLUME
	public void LowerVolume()
	{
		foreach (GameObject thisObject in list_Music)
			thisObject.GetComponent<AudioSource>().volume = 0.4f;
	}

	// MUSIC - RESTORE VOLUME
	public void RestoreVolume()
	{
		foreach (GameObject thisObject in list_Music)
			thisObject.GetComponent<AudioSource>().volume = 1f;
	}
}
