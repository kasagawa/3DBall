using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

	public float maxPoints = 100f;
	public float currPoints = 0f;

	public GameObject progressBar;

	//coordinates of the progress bar 
	float barY,barX;

	// the instance of the current PlayerPoints
	private static ProgressBar instance;

	// static property of PlayerPoints 
	public static ProgressBar Instance {
		get { // an instance getter
			if (instance == null) {
				instance = GameObject.FindObjectOfType<ProgressBar> ();
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

		InvokeRepeating ("addPoints", 1f, 1f);
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
