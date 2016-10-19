using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {
	// current level
	public int level;
	public Text timerText;

	private float time;
	private float minutes;
	private float seconds;

	// the state of the Game
	public enum GameState{
		Playing, Ended_Lost, Ended_Won
	}

	// the instance of the current GameManager
	private static GameManager instance;

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
		time = 0;
		minutes = 0; 
		seconds = 0;
		timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
	}

	void Update(){
		time += Time.deltaTime;
		minutes = Mathf.Floor(time / 60); 
		seconds = time % 60;
		if(seconds > 59) seconds = 59;
		timerText.text = "Time: " + string.Format ("{0:00} : {1:00}", minutes, seconds);
	}

	private void changeLevel() {
		if (level == 3) {
			return;
		} 
		level++;
	}

	public void EndGame() {
		
	}

	public void WinGame() {
		
	}

	public void LooseGame() {
		
	}
}

