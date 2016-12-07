using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TunnelsManager : MonoBehaviour{
	
	public GameObject Fader;

	public bool Paused;

	public float Score;

	public TunnelsPlayer Player;

	private Fader FaderInstance;
	private Text ScoreDisplay;

	private float RestartDelay = 4;
	private float RestartTimer;

	// Use this for initialization
	void Start ()
	{

		Application.targetFrameRate = 60;

		if (FindObjectOfType<Fader> () != null) {

			FaderInstance = FindObjectOfType<Fader> ();

		} else {

			FaderInstance = Instantiate (Fader).GetComponent<Fader> ();

		}

		FaderInstance.GetComponent<Canvas> ().worldCamera = Camera.main;

		ScoreDisplay = FindObjectOfType<Text> ();

	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButtonDown ("Cancel")) {

			TogglePause ();

		}

		if (!Paused) {

			Time.timeScale = 2;

		} else {

			Time.timeScale = 0;

		}

		bool IsDone;

		if (!Player) {

			RestartTimer += Time.unscaledDeltaTime;

			if (RestartTimer >= RestartDelay * 0.6f) {

				FaderInstance.FadeOut ();

				if (RestartTimer >= RestartDelay) {

					RestartScene ();

				}

			}

		}

	}

	public void RestartScene ()
	{

		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}

	public void TogglePause ()
	{

		Paused = !Paused;

	}

}
