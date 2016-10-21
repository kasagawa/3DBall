using UnityEngine;
using UnityEngine.UI;
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

	public void changeColor() {

		Color32 grey = new Color32 (71, 72, 74, 255);
		Color32 orange = new Color32 (255, 148, 0, 255);
		Color32 red = new Color32 (229, 15, 15, 255);
		Color32 green = new Color32 (135, 216, 98, 255);


		if (GameManager.Instance.level == 1) {
			Image progressImg = GameObject.Find("Progress").GetComponent<Image>();
			progressImg.color = orange;
			Image background = GameObject.Find("Background").GetComponent<Image>();
			background.color = grey;
		}

		else if (GameManager.Instance.level == 2) {
			Image progressImg = GameObject.Find("Progress").GetComponent<Image>();
			progressImg.color = red;
			Image background = GameObject.Find("Background").GetComponent<Image>();
			background.color = green;
		}

	}



}



