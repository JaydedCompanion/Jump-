using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISound : MonoBehaviour {

	public AudioClip Tap;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Play () {

		AudioSource.PlayClipAtPoint (Tap, Camera.main.transform.position);

	}

	public void PlayClip (AudioClip clip) {

		AudioSource.PlayClipAtPoint (clip, Camera.main.transform.position);

	}

}
