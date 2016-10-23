using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneManager : MonoBehaviour {

	// amount of planes to create at the start of the game
	private static int n = 100;

	// int used to normalize n (n % k = 0)
	private static int k = 25;

	// int used to normalize randomization
	private static int s = 3;

	// the game manager
	private GameManager manager;

	// the current plane/tile to spawn from
	public GameObject current;

	// materials for each level (0 is level 1 and n is the last level)
	public Material[] materials;

	// the available planes/tiles to spawn
	public GameObject[] planes;

	// objects to spawn on a plane
	public GameObject[] easterCollectables;

	public GameObject[] halloweenCollectables;

	public GameObject[] halloweenObstacles;

	public GameObject[] christmasCollectables;

	public GameObject[] christmasTrees;

	public GameObject[] christmasRocks;

	public GameObject[] fence;

	public GameObject star;
	// end of objects to spawn on a plane

	// a stack of inactive left planes
	private Stack<GameObject> leftPlanes;
	// a stack of inactive top planes
	private Stack<GameObject> topPlanes;

	// the instance of the current PlaneManager
	private static PlaneManager instance;

	// static property of a PlaneManager 
	public static PlaneManager Instance {
		get { // an instance getter
			if (instance == null) {
				instance = GameObject.FindObjectOfType<PlaneManager> ();
			}
			return instance;
		}
	}

	// Use this for initialization
	void Start () {
		manager = GameManager.Instance;

		leftPlanes = new Stack<GameObject> ();
		topPlanes = new Stack<GameObject> ();

		CreatePlanes (n);

		for (int i = 0; i < n / k; i++) {
			SpawnPlane ();
		}
	}

	// creates n planes to be added to the stacks
	public void CreatePlanes(int n) {
		for (int i = 0; i < n; i++) {
			leftPlanes.Push (Instantiate (planes [0]));
			topPlanes.Push (Instantiate (planes [1]));
			// deactivate objects
			leftPlanes.Peek ().SetActive (false);
			topPlanes.Peek ().SetActive (false);
		}
	}

	// spawns a single plane
	public void SpawnPlane() {
		if (leftPlanes.Count == 0 || topPlanes.Count == 0) {
			CreatePlanes (n / k);
		}

		// generating random number between 0 and the amount of available plane prefabs
		int rand = Random.Range (0, planes.Length);

		GameObject tmp = leftPlanes.Peek();
		if (rand == 0) {
			tmp = leftPlanes.Pop ();
		} else if (rand == 1) {
			tmp = topPlanes.Pop ();
		}

		// changes the material of the plane based on the level
		tmp.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = materials[manager.level];
		tmp.SetActive (true); // sets the plane as active in order to appear in the scene
		// makes the position of the new plane the random attachment (either left or right)
		tmp.transform.position = current.transform.GetChild (0).transform.GetChild (rand).position;

		current = tmp;

		current.transform.GetComponent<Plane> ().level = manager.level;

		SpawnObjects ();
	}

	// spawns objects on the current plane
	public void SpawnObjects() {
		// get the current plane
		Plane plane = current.transform.GetComponent<Plane> ();
		// is the player has objects dont add them again
		if (plane.objects[manager.level].Count > 0)
			return;

		if (manager.level == 0) {
			var easter = current.transform.FindChild ("CollectablesSpaces"); // get the easter collectible spaces

			var rand = Random.Range (0, easter.childCount / s); // gets a random number of collectables to 
			//spawn based on the normalized child count

			var uniquePos = generateNRandom (easter.childCount, rand); // generates rand unique random positions
			foreach (int i in uniquePos) {
				var r = Random.Range (0, easterCollectables.Length);
				var obj = (GameObject)Instantiate (easterCollectables [r], easter.GetChild (i).position, Quaternion.identity); 
				plane.objects[manager.level].Push (obj);
			}
		} else if (manager.level == 1) {
			var halloween = current.transform.FindChild ("CollectablesSpaces"); // get the halloween collectible spaces

			var rand = Random.Range (0, halloween.childCount / s); // gets a random number of collectables to 
			//spawn based on the normalized child count

			var uniquePos = generateNRandom (halloween.childCount, rand);
			foreach (int i in uniquePos) {
				var r = Random.Range (0, halloweenCollectables.Length);
				var obj = (GameObject)Instantiate (halloweenCollectables [r], halloween.GetChild (i).position, Quaternion.identity);
				plane.objects[manager.level].Push (obj);
			}

			rand = Random.Range (0, s); // randomly determine if to add a fence
			if (rand == 1) {
				var fenceSpace = current.transform.FindChild ("FenceSpace");
				var f = fence [0];
				if (current.CompareTag ("TopPlane")) {
					f = fence [1];
				}
				var obj = (GameObject)Instantiate (f, fenceSpace.GetChild (0).position, Quaternion.identity);
				plane.objects[manager.level].Push (obj);
			}

			rand = Random.Range (0, n/k); // randomly determine if to add obsticles
			if (rand == 1) {
				var hObsticles = current.transform.FindChild ("HalloweenObstacles");
				rand = Random.Range (0, hObsticles.childCount);
				uniquePos = generateNRandom (hObsticles.childCount, rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, halloweenObstacles.Length);
					var obj = (GameObject)Instantiate (halloweenObstacles [r], hObsticles.GetChild (i).position, Quaternion.identity);
					plane.objects[manager.level].Push (obj);
				}
			}
		} else if (manager.level == 2) {
			var christmas = current.transform.FindChild ("CollectablesSpaces"); // get the christmas collectible spaces

			var rand = Random.Range (0, christmas.childCount / s); 

			var uniquePos = generateNRandom (christmas.childCount, rand);
			foreach (int i in uniquePos) {
				var r = Random.Range (0, christmasCollectables.Length);
				var obj = (GameObject)Instantiate (christmasCollectables [r], christmas.GetChild (i).position, Quaternion.identity);
				plane.objects[manager.level].Push (obj);
			}

			rand = Random.Range (0, n/k * s); // determine if to add a star depending on the 1/(n/k*s) probability
			if (rand == 1) {
				var cStar = current.transform.FindChild ("Star");
				int i = Random.Range(0, cStar.childCount);
				var obj = (GameObject)Instantiate (star, cStar.GetChild (i).position, Quaternion.identity);
				plane.objects[manager.level].Push (obj);
			}

			var cScenery = current.transform.FindChild ("ChristmasScenery");
			rand = Random.Range (0, n/k); // determine if to add rocks depending on the 1/(n/k) probability
			if (rand == 1) {
				var cRocks = cScenery.GetChild (0).transform;
				rand = Random.Range (0, cRocks.childCount);
				uniquePos = generateNRandom (cRocks.childCount, rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, christmasRocks.Length);
					var obj = (GameObject)Instantiate (christmasRocks [r], cRocks.GetChild (i).position, Quaternion.identity);
					plane.objects[manager.level].Push (obj);
				}
			}

			rand = Random.Range (0, s); // determine if to add trees base on the 1/s probability
			if (rand == 1) { 
				var cTrees = cScenery.GetChild (1).transform;
				rand = Random.Range (0, cTrees.childCount);
				uniquePos = generateNRandom (cTrees.childCount, rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, christmasTrees.Length);
					var obj = (GameObject)Instantiate (christmasTrees [r], cTrees.GetChild (i).position, Quaternion.identity);
					plane.objects[manager.level].Push (obj);
				}
			}
		}
	}

	// generates n unique random numbers between 0 and max (exclusive)
	private HashSet<int> generateNRandom(int max, int n) {
		HashSet<int> s = new HashSet<int> ();
		s.Add (Random.Range (0, max));
		while (s.Count < n) {
			s.Add (Random.Range (0, n));
		}
		return s;
	}
}
