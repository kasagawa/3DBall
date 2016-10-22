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
	private float time, minutes, seconds;
	public static float finalMin, finalSec;
	private bool CRrunning;


	//variables that take care of collection count
	public float maxPoints = 2f; //CHANGE
	public float currPoints = 0f; //also used for candy cane
	public float presentPoints = 0f;
	public float ornamentPoints = 0f;
	public float teddyPoints = 0f;

	//variables for the progress bars
	public GameObject progressBar, presentBar, teddyBar, ornamentBar;

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
		presentBar.SetActive (false);
		teddyBar.SetActive (false);
		ornamentBar.SetActive (false);

		player = PlayerController.Instance;
		CRrunning = false;
		currentMusic = easterMusic;

		time = 0;
		minutes = 0; 
		seconds = 0;
		finalMin = 0;
		finalSec = 0;
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
		yield return new WaitForSeconds (1);
		onYourMarkText.text = "Get Set";
		yield return new WaitForSeconds (1);
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

				//check if level is over 
				if (level == 2) {
					if (currPoints >= maxPoints && presentPoints >= maxPoints &&
					    teddyPoints >= maxPoints && ornamentPoints >= maxPoints) {
						changeLevel ();
					}
				} else if (currPoints == maxPoints) {
					changeLevel ();
				}
			}
			//if the player falls too far, lose the game
			else {
				LoseGame ();
			}
		}
	}

	public void addPoints (string pickupName) {
		if (pickupName == "EasterEgg" || pickupName == "Jackolantern") {
			//if the bar is full, don't change anything. else update bar 
			if (currPoints < maxPoints) {
				currPoints += 1f;
				progressBar.GetComponent<ProgressBar> ().setProgressBar (currPoints);
			}
		} else if (pickupName == "CandyCane" && currPoints < maxPoints) {
			currPoints += 1f;
			progressBar.GetComponent<ProgressBar> ().setProgressBar (currPoints);
		} else if (pickupName == "Present" && presentPoints < maxPoints) {
			presentPoints += 1f;
			presentBar.GetComponent<ProgressBar> ().setProgressBar (presentPoints);
		} else if (pickupName == "Ornament" && ornamentPoints < maxPoints) {
			ornamentPoints += 1f;
			ornamentBar.GetComponent<ProgressBar> ().setProgressBar (ornamentPoints);

		} else if (pickupName == "TeddyBear" && teddyPoints < maxPoints) {
			teddyPoints += 1f;
			teddyBar.GetComponent<ProgressBar> ().setProgressBar (teddyPoints);
		}
	}

	public void changeLevel() {
		if (level == 2) { //if finish christmas
			WinGame();
			return;
		} 

		level++;
		//reset the point counter, update progress bar, music, and ball color for each level
		currPoints = 0f;
		progressBar.GetComponent<ProgressBar> ().changeColor ();
		progressBar.GetComponent<ProgressBar> ().setProgressBar (currPoints);
		changeMusic ();
		player.GetComponent<MeshRenderer>().sharedMaterial = player.materials[level];

		//update the progress bars 
		if (level == 1) {
			halloweenConstructor ();
			player.speedUp ();
		} else if (level == 2) {
			christmasConstructor ();
			player.speedUp ();
		}
	}

	//change icons next to progress bar  
	void halloweenConstructor() {
		progressBar.GetComponent<ProgressBar> ().easterEgg.SetActive(false);
		progressBar.GetComponent<ProgressBar> ().jackolantern.SetActive (true);
	}

	//change icons next to progress bars
	//initalize the three additional progress bars for christmas level 
	void christmasConstructor () {
		progressBar.GetComponent<ProgressBar> ().candyCane.SetActive (true);
		progressBar.GetComponent<ProgressBar> ().jackolantern.SetActive (false);

		presentBar.SetActive (true);
		presentBar.GetComponent<ProgressBar> ().hideIcon ();
		presentBar.GetComponent<ProgressBar> ().present.SetActive (true);
		presentBar.GetComponent<ProgressBar> ().changeColor ();


		ornamentBar.SetActive (true);
		ornamentBar.GetComponent<ProgressBar> ().hideIcon ();
		ornamentBar.GetComponent<ProgressBar> ().ornament.SetActive (true);
		ornamentBar.GetComponent<ProgressBar> ().changeColor ();


		teddyBar.SetActive (true);
		teddyBar.GetComponent<ProgressBar> ().hideIcon ();
		teddyBar.GetComponent<ProgressBar> ().teddy.SetActive (true);
		teddyBar.GetComponent<ProgressBar> ().changeColor ();

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
		finalMin = minutes;
		finalSec = seconds;

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

