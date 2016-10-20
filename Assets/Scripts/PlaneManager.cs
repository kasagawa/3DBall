using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneManager : MonoBehaviour {

	// amount of planes to create at the start of the game
	private static int planesToCreate = 20;

	// the game manager
	private GameManager manager;

	// the current plane/tile to spawn from
	public GameObject current;

	// materials for each level
	public Material[] materials;

	// the available planes/tiles to spawn
	public GameObject[] planes;

	public GameObject[] easterCollectables;

	public GameObject[] halloweenCollectables;

	public GameObject[] halloweenObstacles;

	public GameObject[] christmasCollectables;

	public GameObject[] christmasTrees;

	public GameObject[] christmasRocks;

	public GameObject fence;

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

		CreatePlanes (planesToCreate);

		for (int i = 0; i < planesToCreate / 2; i++) {
			SpawnPlane ();
		}
	}

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
			CreatePlanes (planesToCreate / 10);
		}

		// generating random number between 0 and the amount of available plane prefabs
		int rand = Random.Range (0, planes.Length);

		GameObject tmp = leftPlanes.Peek();
		if (rand == 0) {
			tmp = leftPlanes.Pop ();
			if (current != null) 
				current.transform.GetComponent<Plane>().leftAttach = true;
		} else if (rand == 1) {
			tmp = topPlanes.Pop ();
			if (current != null)
				current.transform.GetComponent<Plane>().leftAttach = false;
		}

		tmp.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = materials[manager.level];
		tmp.SetActive (true);
		tmp.transform.position = current.transform.GetChild (0).transform.GetChild (rand).position;

		current = tmp;

		SpawnObjects ();
	}

	// spawns objects on the current plane
	public void SpawnObjects() {
		if (manager.level == 0) {
			var easter = current.transform.FindChild ("CollectablesSpaces"); // get the easter collectible spaces

			var rand = Random.Range (0, easter.childCount) / 2; // generate random amount of collectibles to generate

			var uniquePos = generateNRandom (rand); // generates rand unique random positions
			foreach (int i in uniquePos) {
				var r = Random.Range (0, easterCollectables.Length);
				Instantiate (easterCollectables [r], easter.GetChild (i).position, Quaternion.identity); 
			}
		} else if (manager.level == 1) {
			var halloween = current.transform.FindChild ("CollectablesSpaces"); // get the halloween collectible spaces

			var rand = Random.Range (0, halloween.childCount) / 2;

			var uniquePos = generateNRandom (rand);
			foreach (int i in uniquePos) {
				var r = Random.Range (0, halloweenCollectables.Length);
				Instantiate (halloweenCollectables [r], halloween.GetChild (i).position, Quaternion.identity);
			}

			rand = Random.Range (0, 2); // randomly determine if to add a fence
			if (rand == 1) {
				var fenceSpace = current.transform.FindChild ("FenceSpace");
				Instantiate (fence, fenceSpace.GetChild (0).position, Quaternion.identity);
			}

			rand = Random.Range (0, 2); // randomly determine if to add obsticles
			if (rand == 1) {
				var hObsticles = current.transform.FindChild ("HalloweenObsticles");
				rand = Random.Range (0, halloweenObstacles.Length);
				uniquePos = generateNRandom (rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, halloweenCollectables.Length);
					Instantiate (halloweenObstacles [r], hObsticles.GetChild (i).position, Quaternion.identity);
				}
			}
		} else if (manager.level == 2) {
			var christmas = current.transform.FindChild ("CollectablesSpaces"); // get the christmas collectible spaces

			var rand = Random.Range (0, christmas.childCount) / 2;

			var uniquePos = generateNRandom (rand);
			foreach (int i in uniquePos) {
				var r = Random.Range (0, christmasCollectables.Length);
				Instantiate (christmasCollectables [r], christmas.GetChild (i).position, Quaternion.identity);
			}

			var cScenery = current.transform.FindChild ("ChristmasScenery");
			rand = Random.Range (0, 2); // determine if to add rocks
			if (rand == 1) {
				var cRocks = cScenery.GetChild (0).transform;
				rand = Random.Range (0, cRocks.childCount);
				uniquePos = generateNRandom (rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, christmasRocks.Length);
					Instantiate (christmasRocks [r], cRocks.GetChild (i).position, Quaternion.identity);
				}
			}

			rand = Random.Range (0, 2); // determine if to add trees
			if (rand == 1) {
				var cTrees = cScenery.GetChild(1).transform;
				rand = Random.Range (0, cTrees.childCount);
				uniquePos = generateNRandom (rand);
				foreach (int i in uniquePos) {
					var r = Random.Range (0, christmasTrees.Length);
					Instantiate (christmasTrees [r], cTrees.GetChild (i).position, Quaternion.identity);
				}
			}

		}

	}

	// generates n unique random numbers
	private HashSet<int> generateNRandom(int n) {
		HashSet<int> s = new HashSet<int> ();
		s.Add (Random.Range (0, n));
		while (s.Count < n) {
			s.Add (Random.Range (0, n));
		}
		return s;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
