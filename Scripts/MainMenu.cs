﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text; 
using System;


public class MainMenu : MonoBehaviour {
	private GameStateManager t_GameStateManager;
	public Text TopText;

	public GameObject VolumePanel;
	public GameObject SoundSlider;
	public GameObject MusicSlider;

	public bool volumePanelActive;

	// Use this for initialization
	void Start () {
		t_GameStateManager = FindObjectOfType<GameStateManager> ();
		t_GameStateManager.ConfigNewGame ();

		int currentHighScore = PlayerPrefs.GetInt ("highScore", 0);
	

		TopText.text = "TOP- " + currentHighScore.ToString ("D6");

		if (!PlayerPrefs.HasKey ("soundVolume")) {
			PlayerPrefs.SetFloat ("soundVolume", 1);
		}

		if (!PlayerPrefs.HasKey ("musicVolume")) {
			PlayerPrefs.SetFloat ("musicVolume", 1);
		}

		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");

		Debug.Log (this.name + " Start: Volume Setting sound=" + PlayerPrefs.GetFloat ("soundVolume")
			+ "; music=" + PlayerPrefs.GetFloat ("musicVolume"));
	}

	public void OnMouseHover(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.Find ("Cursor").gameObject;
			cursor.SetActive (true);
		}
	}

	public void OnMouseHoverExit(Button button) {
		if (!volumePanelActive) {
			GameObject cursor = button.transform.Find ("Cursor").gameObject;
			cursor.SetActive (false);
		}
	}

	public int RandomNumber (int min, int max)
	{
		System.Random random = new System.Random();
		return random.Next(min, max);
	}
	public void StartNewGame() {
		
		int map_val = UnityEngine.Random.Range(1,13);
		Console.WriteLine("{0}", map_val);

		
		//since the previous changes aren't necessary as the game is totally PCG generated so it doesn't matter what type of map is being generated initally, this 
		// hard-coded randomness is unnecessary now
		//As World 5-5 is the random PCG ..xD

		map_val = 15;
		//map_val = 4;
		if (map_val == 1)
		{
		 	if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

		if (map_val == 2)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 10-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}
		if (map_val == 3)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 7-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 4)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 4-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 5)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 5-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 6)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 6-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 8)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 8-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 9)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 9-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 10)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 10-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 11)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 11-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

				if (map_val == 12)
		{
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 12-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
		}

		if (map_val == 15)
		{
			if (!volumePanelActive)
			{

				t_GameStateManager.sceneToLoad = "World 1-1";
				SceneManager.LoadScene("Level Start Screen");
			}
		}

	}

	public void StartWorld1_2() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-2";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}
		
	public void StartWorld1_3() {
		if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 1-3";
			SceneManager.LoadScene ("Level Start Screen");
		}
	}


	public void StartWorld1_4() {
			if (!volumePanelActive) {
			t_GameStateManager.sceneToLoad = "World 7-1";
			SceneManager.LoadScene ("Level Start Screen");
			}
	}

	public void StartWorld2_1() {
			t_GameStateManager.sceneToLoad = "World 2-1";
			SceneManager.LoadScene ("Level Start Screen");
		
	}


	public void QuitGame() {
		if (!volumePanelActive) {
			Application.Quit ();
		}
	}

	public void SelectVolume() {
		VolumePanel.SetActive (true);
		volumePanelActive = true;
	}

	public void SetVolume() {
		PlayerPrefs.SetFloat ("soundVolume", SoundSlider.GetComponent<Slider> ().value);
		PlayerPrefs.SetFloat ("musicVolume", MusicSlider.GetComponent<Slider> ().value);
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

	public void CancelSelectVolume() {
		SoundSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("soundVolume");
		MusicSlider.GetComponent<Slider> ().value = PlayerPrefs.GetFloat ("musicVolume");
		VolumePanel.SetActive (false);
		volumePanelActive = false;
	}

}
