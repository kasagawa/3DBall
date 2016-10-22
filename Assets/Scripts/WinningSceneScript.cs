using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinningSceneScript : MonoBehaviour {


	public Text wonText;
	private PlayerController player;

	// Use this for initialization
	void Start () {
		wonText.text = "";
		player = PlayerController.Instance;
		StartCoroutine (CongratsTextDisplayer ());
	}


	//display final text 
	IEnumerator CongratsTextDisplayer(){
		player.speed = 0;
		wonText.text = "Congratulations!";
		yield return new WaitForSeconds (1);
		wonText.text = "You won!";
		yield return new WaitForSeconds (1);
		wonText.text = "Your time was: " + string.Format ("{0:00} : {1:00}", GameManager.finalMin, GameManager.finalSec);;
		yield return new WaitForSeconds (2);
		wonText.text = "Feel free to explore this Winter Wonderland.";
		yield return new WaitForSeconds (2);
		wonText.text = "Press esc at any point to restart the game.";
		yield return new WaitForSeconds (2);
		wonText.text = "";
		player.speed = player.storedSpeed;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			SceneManager.LoadScene ("StartScene");
		}
	}
}
