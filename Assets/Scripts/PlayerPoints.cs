using UnityEngine;
using System.Collections;

public class PlayerPoints : MonoBehaviour {

	public float maxPoints = 100f;
	public float currPoints = 0f;

	public GameObject progressBar;

	//coordinates of the progress bar 
	float barY,barX;

	// the instance of the current PlayerPoints
	private static PlayerPoints instance;

	// static property of PlayerPoints 
	public static PlayerPoints Instance {
		get { // an instance getter
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PlayerPoints> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		//coordinates of the progress bar 
		barY = progressBar.transform.localScale.y;
		barX = progressBar.transform.localScale.x;
		progressBar.transform.localScale = new Vector3 (0, barY, barX);

//		InvokeRepeating ("addPoints", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addPoints () {
		Debug.Log ("inside add points");
		currPoints += 1f;

		//calcProgress is the percentage of completion -- num between 0 and 1
		float calcProgress = currPoints / maxPoints;

		setProgressBar (calcProgress);

	}

	// Moves the bar to match the player's progress in collecting objects
	public void setProgressBar (float progress){
		barY = progressBar.transform.localScale.y;
		barX = progressBar.transform.localScale.x;

		progressBar.transform.localScale = new Vector3 (progress, barY, barX);
	}
}
