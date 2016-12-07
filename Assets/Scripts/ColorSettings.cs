using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettings : MonoBehaviour {

	public Material RedCol, BlueCol;

	private Animator Anim;
	private bool showingColorPicker;

	// Use this for initialization
	void Start () {

		Anim = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleColorPicker () {

		showingColorPicker = !showingColorPicker;

		Anim.SetBool ("Show", showingColorPicker);

	}

	public void SaveColorPrefs () {

		Debug.Log ("~~~~~~Saving color preferences...");


		PlayerPrefs.SetFloat ("RedCol.R", RedCol.color.r);
		PlayerPrefs.SetFloat ("RedCol.G", RedCol.color.g);
		PlayerPrefs.SetFloat ("RedCol.B", RedCol.color.b);

		PlayerPrefs.SetFloat ("BlueCol.R", BlueCol.color.r);
		PlayerPrefs.SetFloat ("BlueCol.G", BlueCol.color.g);
		PlayerPrefs.SetFloat ("BlueCol.B", BlueCol.color.b);


		Debug.Log ("Red RGB [R: " + Mathf.Round(RedCol.color.r * 100) + 
		           "\t[G: " + Mathf.Round (RedCol.color.g * 100) + 
		           "\t[B: " + Mathf.Round (RedCol.color.b * 100) + 
		           "\t\tBlue RGB [R: " + Mathf.Round (BlueCol.color.r * 100) + 
		           "\t[G: " + Mathf.Round (BlueCol.color.g * 100) + 
		           "\t[B: " + Mathf.Round (BlueCol.color.b * 100)
		          );


		Debug.Log ("Saving preferences to storage...");
		try {
			PlayerPrefs.Save ();
			Debug.Log ("~~~~~~Done!");
		} catch {
			Debug.LogError ("!!!COULD NOT SAVE PREFERENCES TO DISK!!!");
		}

	}

	public void SaveSliderPrefs (string SliderColor, float Value) {

		PlayerPrefs.SetFloat (SliderColor, Value);
		PlayerPrefs.Save ();

	}

	public float GetSliderPrefs (string SliderColor) {

		return PlayerPrefs.GetFloat (SliderColor, 1);

	}

}
