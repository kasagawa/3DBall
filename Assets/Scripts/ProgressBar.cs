using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

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
	}
		
	// Moves the bar to match the player's progress in collecting objects
	public void setProgressBar () {

		//calcProgress is the percentage of completion -- num between 0 and 1
		float progress = GameManager.Instance.currPoints / GameManager.Instance.maxPoints;

		barY = progressBar.transform.localScale.y;
		barX = progressBar.transform.localScale.x;

		progressBar.transform.localScale = new Vector3 (progress, barY, barX);
	}
}
