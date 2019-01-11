using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridScript : MonoBehaviour {

	// Components

	Camera mainCam;

	// Linked Game Objects

	public GameObject circle;
	public GameObject cross;

	// Grid Variables

	public Vector2[] gridPoints = new Vector2[9];

	// Click Variables

	Vector2 clickPos;

	// Symbol Variables

	bool circlePlaced;

	// Enumerators

	IEnumerator checkClicks;

	#region Debug ______________________________________________________________

	private void OnDrawGizmos () {
		for (int i = 0; i < gridPoints.Length; i++) {
			Gizmos.DrawCube (gridPoints[i], new Vector3 (1, 1, 1));
		}
	}

	#endregion

	private void Start () {

		// Components

		mainCam = Camera.main.GetComponent<Camera> ();

		// Enumerators

		checkClicks = CheckClicks ();
		StartCoroutine (checkClicks);
	}

	IEnumerator CheckClicks () {
		
		while (enabled) {
			if (Input.GetMouseButtonDown (0)) {
				RecordClickPos ();
				PlaceIcon ();
			}
			yield return null;
		}
	}

	void RecordClickPos () {
		clickPos = mainCam.ScreenToWorldPoint (Input.mousePosition);
	}

	void PlaceIcon () {
		
		Vector2 targetPoint = new Vector2 ();
		Vector2 closestPoint = new Vector2 ();

		float shortestDist = 0;

		for (int i = 0; i < gridPoints.Length; i++) {
			if (Vector2.Distance (clickPos, gridPoints[i]) < shortestDist) {
				
				shortestDist = Vector2.Distance (clickPos, gridPoints[i]);
				closestPoint = gridPoints[i];

			} else if (shortestDist == 0) {
				
				shortestDist = Vector2.Distance (clickPos, gridPoints[i]);
				closestPoint = gridPoints[i];
			}
		}

		GameObject go = Instantiate (DecideSymbol (circlePlaced), transform);
		go.transform.position = closestPoint;
	}

	GameObject DecideSymbol (bool cPlaced) {
		
		GameObject go = new GameObject ();

		switch (cPlaced) {
			case true:
				go = cross;
				circlePlaced = false;
				break;
			case false:
				go = circle;
				circlePlaced = true;
				break;
		}

		return go;
	}

	#region Positions __________________________________________________________

	Vector2 TopLeft () { return gridPoints[0]; }

	Vector2 TopMid () { return gridPoints[1]; }

	Vector2 TopRight () { return gridPoints[2]; }

	Vector2 MidLeft () { return gridPoints[3]; }

	Vector2 Mid () { return gridPoints[4]; }

	Vector2 MidRight () { return gridPoints[5]; }

	Vector2 BtmLeft () { return gridPoints[6]; }

	Vector2 BtmMid () { return gridPoints[7]; }

	Vector2 BtmRight () { return gridPoints[8]; }

	#endregion

	//private void Start () {
	//	if (transform.childCount < gridPoints.Length) SpawnEmptyChildren ();
	//}

	//private void Update () {
	//	for (int i = 0; i < transform.childCount; i++) {
	//		ChildPosToGridPoint (i);
	//	}
	//}

	//void SpawnEmptyChildren () {
	//	for (int i = 0; i < gridPoints.Length; i++) {
	//		GameObject go = new GameObject ();
	//		go.transform.parent = transform;
	//	}
	//}

	//void ChildPosToGridPoint (int i) {
	//	gridPoints[i] = transform.GetChild (i).position;
	//}
}
