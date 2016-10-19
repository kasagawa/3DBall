using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlaneManager : MonoBehaviour {

	// amount of planes to create at the start of the game
	private static int planesToCreate = 100;

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

	// spawns a plane
	public void SpawnPlane() {
		if (leftPlanes.Count == 0 || topPlanes.Count == 0) {
			CreatePlanes (planesToCreate / 10);
		}

		// generating random number between 0 and the amount of available plane prefabs
		int rand = Random.Range (0, planes.Length);

		GameObject tmp = leftPlanes.Peek();
		if (rand == 0) {
			tmp = leftPlanes.Pop ();
		} else if (rand == 1) {
			tmp = topPlanes.Pop ();
		}

		tmp.transform.GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = materials[manager.level];
		tmp.SetActive (true);
		tmp.transform.position = current.transform.GetChild (0).transform.GetChild (rand).position;
		current = tmp;

		//current = (GameObject)Instantiate (planes[rand], current.transform.GetChild(0).transform.GetChild(rand).position, Quaternion.identity);
	}

	// spawns collectible objects on the current plane
	public void SpawnCollectible() {
		if (manager.level == 0) {
			var easter = current.transform.GetChild (1);
			var rand = Random.Range (0, easter.childCount);

//			Random r = new Random ();
			for (int i = 0; i < rand; i++) {
//				Instantiate (easterCollectables[r.Next(easterCollectables.Length)], 
			}
		}



		var fenceSpace = current.transform.GetChild (2);
		var halloween = current.transform.GetChild (3);
		var christmas = current.transform.GetChild (4);
		var cTrees = christmas.transform.GetChild (1);
		var cRocks = christmas.transform.GetChild (0);


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
