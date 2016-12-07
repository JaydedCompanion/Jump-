using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelsBooster : MonoBehaviour {

	public Material OppositeColor;

	[HideInInspector]
	public TunnelsObjectSpawner Man;

	private Transform Follow;
	private bool FailSafe;

	private float Speed;

	// Use this for initialization
	void Start () {

		Follow = FindObjectOfType<TunnelsPlayer> ().transform;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Vector2.Distance (transform.position, Follow.position) > 100) {

			Destroy (gameObject);

		} else {

			Speed *= 1.1f;

			transform.Translate (Vector3.right * Time.deltaTime * Speed);

		}

		if (transform.position.x < Follow.position.x && !FailSafe) {

			GetComponentInChildren<MeshRenderer> ().sharedMaterials[1] = OppositeColor;

			if (Man != null) {

				Man.Booster = null;
				Man = null;

				Follow.GetComponent<TunnelsPlayer> ().Missed ();

			}

		}
		
	}

	public void OnTriggerEnter2D (Collider2D Coll) {

		if (Coll.tag == "Player") {

			Coll.GetComponent<TunnelsPlayer> ().Boost ();

			Man.Booster = null;

			FailSafe = true;

		}

	}

	public void OnTriggerExit2D (Collider2D Coll) {

		if (Coll.tag == "Player") {

			Speed = Coll.GetComponent<TunnelsPlayer> ().xSpeed;

		}

	}

}
