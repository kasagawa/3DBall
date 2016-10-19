using UnityEngine;

public class GameManager : MonoBehaviour {
	// current level
	public int level;

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

