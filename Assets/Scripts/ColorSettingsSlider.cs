using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSettingsSlider : MonoBehaviour {

	public string prefs_SliderTag;

	public bool doBrightnessValue;


	public Color ColA, ColB, ColC;
	public Color col;

	[Space (20)]
	public Material Mat;

	private ColorSettings colSettings;
	private Slider ColorPickerSlider;
	private Color PrevCol;
	private bool HasSavedPrefs;

	// Use this for initialization
	void Start () {
		
		ColorPickerSlider = GetComponentInChildren<Slider> ();

		colSettings = transform.parent.parent.parent.GetComponent<ColorSettings> ();

		ColorPickerSlider.value = colSettings.GetSliderPrefs (prefs_SliderTag);
		
	}
	
	// Update is called once per frame
	void Update () {

		if (col == PrevCol) {

			if (!HasSavedPrefs) {

				SavePrefs ();

				HasSavedPrefs = true;

			}

		} else {

			HasSavedPrefs = false;

		}

		PrevCol = col;

		if (doBrightnessValue) {

			Vector3 HSV = new Vector3 ();

			Color.RGBToHSV (Mat.color, out HSV.x, out HSV.y, out HSV.z);

			HSV.z = ColorPickerSlider.value;

			Mat.color = Color.HSVToRGB (HSV.x, HSV.y, HSV.z);

		} else {

			if (ColorPickerSlider.value <= 0.5f) {

				col = Color.Lerp (ColA, ColB, ColorPickerSlider.value * 2);

			} else {

				col = Color.Lerp (ColB, ColC, (ColorPickerSlider.value * 2) - 1f);

			}

			Mat.color = col;

		}
		
	}

	public void SavePrefs () {

		colSettings.SaveColorPrefs ();

		colSettings.SaveSliderPrefs (prefs_SliderTag, ColorPickerSlider.value);
	
	}

}
