using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {
	private LevelManager t_LevelManager;
	private Transform flag;
	private Transform flagStop;
	private bool moveFlag;

	private float flagVelocityY = 0.025f;
	public string sceneName = "World 10-1";
	public string[] sceneArray = new string[] 
	{
		"World 1-1", "World 2-1", "World 13-1", "World 4-1", "World 5-1", "World 6-1",
		"World 7-1", "World 8-1", "World 9-1", "World 10-1", "World 11-1", "World 12-1"
	};
	//int size = sceneArray.Length;
	public int num = 4;
	// Use this for initialization
	void Start () {
		t_LevelManager = FindObjectOfType<LevelManager> ();
		flag = transform.Find ("Flag");
		flagStop = transform.Find ("Flag Stop");
	}

	void FixedUpdate() {
		if (moveFlag) {
			if (flag.position.y < flagStop.position.y) {
				flag.position = new Vector2 (flag.position.x, flag.position.y + flagVelocityY);
			} else {
				/*int val = Random.Range(0, 11);
				sceneName = sceneArray[val];*/
				sceneName = "World 5-5";
				t_LevelManager.LoadNewLevel (sceneName, t_LevelManager.levelCompleteMusic.length);
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "Player") {
			moveFlag = true;
			t_LevelManager.MarioCompleteLevel ();
		}
	}
}
