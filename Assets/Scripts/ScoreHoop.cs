using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreHoop : MonoBehaviour {

	public GameObject ScorePlus;

	public Material Red;
	public Material Blue;

	public AudioClip ScoreUp;
	public AudioClip ScoreDown;

	public float ScoreValue;
	public bool PlayerCrashed;

	private bool Snitch;
	private bool AlreadySnitched;
	private ScoreHoop Parent;

	// Use this for initialization
	void Start () {

		if (transform.parent!= null) {

			Snitch = true;

			Parent = transform.parent.GetComponent<ScoreHoop> ();
		
		}
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void OnTriggerStay2D (Collider2D Coll)
	{

		if (Coll.tag == "PlayerColl" && Snitch) {

			PlayerCrashed = true;
			Parent.PlayerCrashed = true;

		}

	}

	public void OnTriggerExit2D (Collider2D Coll) {

		if (Coll.tag == "PlayerColl" && Coll.transform.root.GetComponentInChildren<Player> () != null && !Coll.transform.root.GetComponentInChildren<Player> ().isDead){
			
			if (!Snitch && !PlayerCrashed) {

				GameObject scorePlus = Instantiate (ScorePlus, transform.position, Quaternion.Euler (Vector3.zero)) as GameObject;

				scorePlus.GetComponentInChildren<Text> ().text = "+" + ScoreValue;
				scorePlus.GetComponentInChildren<Text> ().color = Blue.color;

				Coll.transform.root.GetComponentInChildren<Player> ().Score += ScoreValue;

				AudioSource.PlayClipAtPoint (ScoreUp, Camera.main.transform.position);

			}

			if (Snitch && PlayerCrashed && !AlreadySnitched) {

				AlreadySnitched = true;

				GameObject scorePlus = Instantiate (Parent.ScorePlus, Parent.transform.position, Quaternion.Euler (Vector3.zero)) as GameObject;
				scorePlus.GetComponentInChildren<Text> ().color = Color.grey;

				scorePlus.GetComponentInChildren<Text> ().text = "-" + (Parent.ScoreValue * 2);
				scorePlus.GetComponentInChildren<Text> ().color = Parent.Red.color;

				Coll.transform.root.GetComponentInChildren<Player> ().Score -= Parent.ScoreValue * 2;

				AudioSource.PlayClipAtPoint (Parent.ScoreDown, Camera.main.transform.position);

			}

		}

	}

}
