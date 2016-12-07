using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuidePanelHandler : MonoBehaviour {

	private JumpManager man;

	private bool Faded;
	private CanvasGroup cGroup;

	// Use this for initialization
	void Start () {

		if (PlayerPrefs.GetInt ("DismissedGuide", 0) == 1) {

			Destroy (gameObject);

		} else {

			man = FindObjectOfType<JumpManager> ();

			man.Paused = true;

			cGroup = GetComponent<CanvasGroup> ();

		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Faded) {
			cGroup.alpha = Mathf.Lerp (cGroup.alpha, 0, Time.unscaledDeltaTime * 5);
			cGroup.blocksRaycasts = false;
		}
		
	}

	public void Dismiss () {

		man.Paused = false;

		Faded = true;

		PlayerPrefs.SetInt ("DismissedGuide", 1);
		PlayerPrefs.Save ();

	}

}
