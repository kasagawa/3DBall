using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	// current level
	public int level;

	private PlayerController player;

	public Text timerText;
	public Text loseText;
	public Text quitText;

	private float time;
	private float minutes;
	private float seconds;

	// the state of the Game
	public enum GameState{
		Playing, Ended_Lost, Ended_Won
	}

	// the instance of the current GameManager
	private static GameManager instance;

	public static GameState State = GameState.Playing;

	// static property of a GameManager 
	public static GameManager Instance {
		get { // an instance getter
			if (instance == null) {
				instance = GameObject.FindObjectOfType<GameManager> ();
			}
			return instance;
		}
	}

	void Start() {
		player = PlayerController.Instance;

		time = 0;
		minutes = 0; 
		seconds = 0;
		timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
		loseText.text = "";
		quitText.text = "";
	}

	void Update(){
		if (player.transform.position.y > -5) {
			time += Time.deltaTime;
			minutes = Mathf.Floor (time / 60); 
			seconds = time % 60;
			if (seconds > 59)
				seconds = 59;
			timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
		} 
		//if the player falls too far, lose the game
		else {
			LooseGame ();
		}
	}

	public void changeLevel() {
		if (level == 2) {
			EndGame();
			return;
		} 
		level++;

		player.GetComponent<MeshRenderer>().sharedMaterial = player.materials[level];
	}

	public void EndGame() {
		
	}

	public void WinGame() {
		
	}


	//Call this method if you fall off the platform
	public void LooseGame() {
		player.speed = 0;
		loseText.text = "You Lose";
		quitText.text = "Press Esc To Play Again";
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("StartScene");
		}
		State = GameState.Ended_Lost;
	}
}

