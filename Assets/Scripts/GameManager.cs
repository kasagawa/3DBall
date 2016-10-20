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
	public Text onYourMarkText;

	private float time;
	private float minutes;
	private float seconds;
	private bool CRrunning;

	public float maxPoints = 10f;
	public float currPoints = 0f;

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
		CRrunning = false;

		time = 0;
		minutes = 0; 
		seconds = 0;
		timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
		loseText.text = "";
		quitText.text = "";

		onYourMarkText.text = "";
		StartCoroutine(OnYourMark());
	}

	//wait for 5 seconds to start the game & display starting text
		IEnumerator OnYourMark() {
			CRrunning = true;
			player.speed = 0;
			onYourMarkText.text = "On Your Mark";
			yield return new WaitForSeconds (2);
			onYourMarkText.text = "Get Set";
			yield return new WaitForSeconds (2);
			onYourMarkText.text = "Go!!";
			yield return new WaitForSeconds (1);
			onYourMarkText.text = "";
		 	player.speed = player.storedSpeed;
			CRrunning = false;
		}

	void Update(){
		if (!CRrunning) {
			if (player.transform.position.y > -10) {
				time += Time.deltaTime;
				minutes = Mathf.Floor (time / 60); 
				seconds = time % 60;
				if (seconds > 59)
					seconds = 59;
				timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
			} 
			//if the player falls too far, lose the game
			else {
				LoseGame ();
			}
		}
	}

	public void addPoints () {
		currPoints += 1f;
		ProgressBar.Instance.setProgressBar ();
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
	public void LoseGame() {
		player.speed = 0;
		loseText.text = "You Lose";
		quitText.text = "Press Esc To Play Again";
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("StartScene");
		}
		State = GameState.Ended_Lost;
	}
}

