using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Animator Anim;

	// Use this for initialization
	void Start () {

		Anim = GetComponentInChildren<Animator> ();
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public void FadeIn () {

		if (!Anim) Start ();

		Anim.SetBool ("Fade", false);

	}

	public void FadeOut () {

		if (!Anim) Start ();

		Anim.SetBool ("Fade", true);

	}

}
