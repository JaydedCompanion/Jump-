using UnityEngine;
using System.Collections;

public class CamFollow : MonoBehaviour {

	private Transform[] Ground;
	private float Xpos;

	private TunnelsManager TMan;
	private JumpManager JMan;

	// Use this for initialization
	void Start () {
			   if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ()) {
			JMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ();
		} else if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<TunnelsManager> ()) {
			TMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<TunnelsManager> ();
		}

		GameObject [] GroundObjects = GameObject.FindGameObjectsWithTag ("Ground");

		Ground = new Transform [GroundObjects.Length];

		for (int i = 0; i < Ground.Length; i ++) {

			Ground [i] = GroundObjects [i].transform;

		}
		
	}
	
	// Update is called once per frame

	void LateUpdate () {

		Vector3 pos = transform.position;

		if (JMan && JMan.Players.Count >= 1) {
			Xpos = JMan.Players [0].transform.position.x;
		}

		if (TMan && TMan.Player) {
			Xpos = Mathf.Lerp (Xpos, TMan.Player.transform.position.x, 5 * Time.deltaTime);
		}

		pos.x = Xpos;
		transform.position = pos;

		foreach (Transform trans in Ground) {
			trans.position = new Vector2 (pos.x, trans.position.y);
		}

	}

}
