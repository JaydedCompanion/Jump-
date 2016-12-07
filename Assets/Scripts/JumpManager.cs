using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JumpManager : MonoBehaviour {

	public GameObject Fader;

	public bool Paused;

	public float IncrementSpeed;

	public float UniversalMovementSpeed;
	public float UniversalBaseJumpForce;

	public float Score;

	public List<Player> Players = new List<Player> ();

	public List<bool> Jumping;
	public List<float> JumpForce;
	public List<float> Scores;

	private Fader FaderInstance;
	private Text ScoreDisplay;
	private Camera Cam;
	private float PrevScore;
	private float DeltaScore;

	private float RestartDelay = 4;
	private float RestartTimer;

	// Use this for initialization
	void Start () {

		Application.targetFrameRate = 60;

		if (FindObjectOfType<Fader> () != null) {

			FaderInstance = FindObjectOfType<Fader> ();

		} else {

			FaderInstance = Instantiate (Fader).GetComponent<Fader>();

		}

		FaderInstance.GetComponent<Canvas> ().worldCamera = Camera.main;

		ScoreDisplay = FindObjectOfType<Text> ();

		Cam = Camera.main;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown ("Cancel")) {

			TogglePause ();

		}

		if (!Paused) {

			UniversalMovementSpeed += Time.deltaTime * IncrementSpeed * 0.01f;

			Time.timeScale = 2;

		} else {

			Time.timeScale = 0;
		
		}

		if (Players.Count == 1 && Application.isEditor) {

			Jumping [0] = Input.GetKey(KeyCode.Space);

		}

		if (Players.Count == 1 && Application.isMobilePlatform) {

			if (Input.touchCount >= 1) {

				Jumping [0] = true;

			} else {

				Jumping [0] = false;
			
			}

		}

		for (int i = 0; i < Jumping.Count; i++) {

			if (Jumping[i]) {

				JumpForce [i] = 0.65f; //+= Time.deltaTime * 0.5f;

			}

		}

		bool IsDone;

		if (Players.Count <= 0) {

			RestartTimer += Time.unscaledDeltaTime;

			if (RestartTimer >= RestartDelay* 0.6f) {

				FaderInstance.FadeOut ();

				if (RestartTimer >= RestartDelay) {

					RestartScene ();

				}
			
			}

		}
	
	}

	public int Join (Player me) {

		Players.Add (me);
		Jumping.Add (false);
		JumpForce.Add (0);
		Scores.Add (0);

		return Players.IndexOf (me);

	}

	public void Leave (Player me) {

		int Index = Players.IndexOf (me);

		Players.RemoveAt (Index);
		Jumping.RemoveAt (Index);
		JumpForce.RemoveAt (Index);

	}

	public void RestartScene () {

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

	public void TogglePause () {

		Paused = !Paused;

	}

}
