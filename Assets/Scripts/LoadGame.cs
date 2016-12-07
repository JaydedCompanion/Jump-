using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour {

	[Tooltip ("Specify the scenes to use when loading a game")]
	public List<string> Games = new List<string> ();
	public GameObject FadeCanvas;

	private float timer;
	private bool TriggerLoad;
	private Fader fadeCanvasInstance;

	void Start () {

		fadeCanvasInstance = Instantiate (FadeCanvas).GetComponent<Fader>();

		fadeCanvasInstance.FadeIn ();

	}

	void Awake () {

		DontDestroyOnLoad (this);

	}

	public void Update () {

		if (TriggerLoad) {

			timer += Time.unscaledDeltaTime;

			if (timer >= 1) {

				if (Games.Count > 2) {

					int i = 0;

					while (i == Games.IndexOf (SceneManager.GetActiveScene ().name)) {

						i = Random.Range (0, Games.Count);

					}

					SceneManager.LoadScene (Games [i]);

				} else {

					SceneManager.LoadScene (Games [0]);

				}

				timer = 0;
				TriggerLoad = false;

			}
		
		}

	}

	public void LoadRandomGame () {

		TriggerLoad = true;

		fadeCanvasInstance.FadeOut ();

		PlayerPrefs.SetInt ("DismissedGuide", 0);
		PlayerPrefs.Save ();

	}

}
