using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[ExecuteInEditMode]
public class SetColToMat : MonoBehaviour {

	public Material Mat;

	private Text txt;
	private Image img;
	private SpriteRenderer sr;
	private ParticleSystem ps;

	// Use this for initialization
	void Start () {

		if (GetComponent<Text>() != null) {
			txt = GetComponent<Text> ();
		}

		if (GetComponent<Image> () != null) {
			img = GetComponent<Image> ();
		}

		if (GetComponent<SpriteRenderer> () != null) {
			sr = GetComponent<SpriteRenderer> ();
		}

		if (GetComponent<ParticleSystem> () != null) {
			ps = GetComponent<ParticleSystem> ();
		}
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Mat != null) {

			if (txt != null) {

				txt.color = Mat.color;

			}

			if (img != null) {

				img.color = Mat.color;

			}

			if (sr != null) {

				sr.color = Mat.color;

			}

			if (ps != null) {

				ps.startColor = Mat.color;

			}

		} else {

			Debug.LogWarning ("No material specified");

		}
		
	}

}
