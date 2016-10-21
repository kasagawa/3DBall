using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
	// current level
	public int level;

	private PlayerController player;


	//all the text to be displayed on scene
	public Text timerText;
	public Text loseText;
	public Text quitText;
	public Text onYourMarkText;

	//variables that take care of timing
	private float time;
	private float minutes;
	private float seconds;
	private bool CRrunning;


	//variables that take care of collection count

	public float maxPoints = 2f; //CHANGE
	public float currPoints = 0f;

	//variables for the audio sources
	public AudioSource easterMusic;
	public AudioSource halloweenMusic;
	public AudioSource christmasMusic;
	private AudioSource currentMusic;

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
		currentMusic = easterMusic;

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
			if (player.transform.position.y > -50) {
				time += Time.deltaTime;
				minutes = Mathf.Floor (time / 60); 
				seconds = time % 60;
				if (seconds > 59)
					seconds = 59;
				timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds); 

				if (currPoints == maxPoints) {
					currPoints = 0f;
					changeLevel ();
				}
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
			WinGame();
			return;
		} 
		level++;
		changeMusic ();

		player.GetComponent<MeshRenderer>().sharedMaterial = player.materials[level];
	}

	void changeMusic(){
		if (level == 0) {
			easterMusic.Play ();
			currentMusic = easterMusic;
		} else if (level == 1) {
			easterMusic.Stop ();
			halloweenMusic.Play ();
		currentMusic = halloweenMusic;
		} else {
			halloweenMusic.Stop ();
			christmasMusic.Play ();
		currentMusic = christmasMusic;
		}
	}

	//Takes you to the final scene if you won
	public void WinGame() {
		player.speed = 0;
		State = GameState.Ended_Won;
		SceneManager.LoadScene ("WinningScene");
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
		currentMusic.Stop ();
	}
}

