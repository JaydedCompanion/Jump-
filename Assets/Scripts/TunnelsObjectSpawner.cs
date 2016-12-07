using UnityEngine;
using System.Collections;

public class TunnelsObjectSpawner : MonoBehaviour {


	public float MinHeight;
	public float MaxHeight;

	public GameObject Booster;

	private JumpManager JMan;
	private TunnelsManager TMan;
	private GameObject BoosterOriginal;

	private float DelayTimer;
	private bool WasStatic;

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ()){
			JMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ();
		} else if (GameObject.FindGameObjectWithTag ("Manager").GetComponent<JumpManager> ()){
			TMan = GameObject.FindGameObjectWithTag ("Manager").GetComponent<TunnelsManager> ();
		}

		BoosterOriginal = Booster;
		Booster = null;
		
	}
	
	// Update is called once per frame

	void Update () {

		if (TMan && TMan.Paused)
			return;

		if (!Booster) {
			
			Vector2 pos = new Vector2 (
				transform.position.x, Random.Range (MinHeight, MaxHeight)
			);

			Booster = SpawnObject (BoosterOriginal, pos);
			Booster.GetComponent<TunnelsBooster> ().Man = this;

		}
	
	}

	GameObject SpawnObject (GameObject ObjectToSpawn, Vector2 SpawnAt) {

		return Instantiate (ObjectToSpawn, SpawnAt, Quaternion.Euler (Vector3.zero)) as GameObject;

	}

}
