using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreAnimator : MonoBehaviour {

	public int PlayerID;
	public float SpacingUnits;
	public bool Go;
	public Material ColMat;
	public Transform Score;

	private Animator Anim;
	private JumpManager Man;
	private RectTransform transform;
	private Text [] DigitBoxes;

	// Use this for initialization
	void Start () {

		Anim = GetComponent<Animator> ();
		Man = FindObjectOfType<JumpManager> ();
		transform = GetComponent<RectTransform> ();

		DigitBoxes = Score.GetComponentsInChildren<Text> ();
		System.Array.Reverse (DigitBoxes);

		for (int i = 0; i < Score.childCount; i++) {

			DigitBoxes [i].color = ColMat.color;

		}

		Score.transform.parent.GetComponent<Image> ().color = ColMat.color;
		Score.transform.parent.GetChild (1).GetComponent<Text> ().color = ColMat.color;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Go) {

			Vector2 pos = transform.anchoredPosition;

			pos.y = Mathf.Lerp (pos.y, -SpacingUnits * (PlayerID), 0.05f);

			if (Mathf.RoundToInt (pos.y * 100) == Mathf.RoundToInt((-SpacingUnits * PlayerID) * 100)) {

				Anim.SetTrigger ("pos");

			}

			transform.anchoredPosition = pos;

		}

		string Items = (Mathf.RoundToInt (Man.Scores [PlayerID])).ToString().PadLeft (DigitBoxes.Length, '0');
		char [] Digits = Items.ToCharArray();

		for (int i = 0; i < Score.childCount; i ++) {

			DigitBoxes [i].text = Digits [i].ToString();

		}
	
	}

	void Leave () {



	}

}
