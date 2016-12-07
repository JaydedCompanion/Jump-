using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public bool useRandomDelay;
	public float MinDelay;
	public float MaxDelay;

	public float Delay;

	public float MinHeight;
	public float MaxHeight;

	public GameObject [] StaticObjects;
	public GameObject [] DynamicObjects;

	private JumpManager JMan;
	private TunnelsManager TMan;

	private bool WasStatic;
	private float DelayTimer;
	private float OriginalSpeed;
	private float RandomizedValue;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ()){
			JMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ();
			OriginalSpeed = JMan.UniversalMovementSpeed;
		} else if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ()){
			TMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<TunnelsManager> ();
		}
		
	}
	
	// Update is called once per frame

	void Update () {

		if ((JMan && JMan.Paused) || (TMan && TMan.Paused))
			return;

		DelayTimer += Time.deltaTime;

		if ((!useRandomDelay && DelayTimer >= Delay) || (useRandomDelay && DelayTimer >= Delay + RandomizedValue)) {

			if (WasStatic || StaticObjects.Length <= 0) {

				Vector2 pos = new Vector2 (
					transform.position.x, Random.Range (MinHeight, MaxHeight)
				);

				SpawnObject (DynamicObjects [Random.Range (0, DynamicObjects.Length)], pos);

			} else {

				GameObject Spike = SpawnObject (StaticObjects [Random.Range (0, StaticObjects.Length)], transform.position) as GameObject;

				Spike.transform.localScale = new Vector3 (
					(JMan.UniversalMovementSpeed/OriginalSpeed) * 1.5f, 
					(JMan.UniversalMovementSpeed / OriginalSpeed), 
					1
				);

			}

			RandomizedValue = Random.Range (MinDelay, MaxDelay);

			WasStatic = !WasStatic;

			DelayTimer = 0;

		}
	
	}

	GameObject SpawnObject (GameObject ObjectToSpawn, Vector2 SpawnAt) {

		GameObject spawnedObject = Instantiate (ObjectToSpawn, SpawnAt, Quaternion.Euler (Vector3.zero));

		return spawnedObject;

	}

}
