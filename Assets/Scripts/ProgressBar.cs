using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour {
	// the object representing the progress bar
	public GameObject progressBar;
	// the objects to collects
	public GameObject easterEgg, jackolantern, candyCane, teddy, present, ornament;
	// the image of the progress bar and the background of the bar
	private Image progressImg, background;

	// the colors of the progress bar depending on the level
	private static Color32 grey = new Color32 (71, 72, 74, 255);
	private static Color32 orange = new Color32 (255, 148, 0, 255);
	private static Color32 red = new Color32 (229, 15, 15, 255);
	private static Color32 green = new Color32 (135, 216, 98, 255);

	//coordinates of the progress bar 
	float barY,barX;


	// Use this for initialization
	void Start () {
		//coordinates of the progress bar 
		barY = progressBar.transform.localScale.y;
		barX = progressBar.transform.localScale.x;
		progressBar.transform.localScale = new Vector3 (0, barY, barX);

		//set icon next to progress bar
		hideIcon ();
		easterEgg.SetActive (true);

		progressImg = GameObject.Find("Progress").GetComponent<Image>();
		background = GameObject.Find("Background").GetComponent<Image>();


	}

	//sets all the icons to false 
	public void hideIcon () {
		easterEgg.SetActive (false);
		candyCane.SetActive (false);
		teddy.SetActive (false);
		present.SetActive (false);
		ornament.SetActive (false);
		jackolantern.SetActive (false);
	}

	// Moves the bar to match the player's progress in collecting objects
	public void setProgressBar (float currPoints) {
		if (currPoints <= GameManager.Instance.maxPoints) {
			//calcProgress is the percentage of completion -- num between 0 and 1
			float progress = currPoints / GameManager.Instance.maxPoints;

			barY = progressBar.transform.localScale.y;
			barX = progressBar.transform.localScale.x;
			// changes the progress bar depending on the points
			progressBar.transform.localScale = new Vector3 (progress, barY, barX);
		}
	}

	// changes teh color of the progress bar depending on the level
	public void changeColor () {
		if (GameManager.Instance.level == 1) {
			//change the color of the progress bar 
			progressImg.color = orange;
			background.color = grey;
		}

		else if (GameManager.Instance.level == 2) {
			//change the color of the progress bar 
			progressImg.color = red;
			background.color = green;
		}

	}



}



