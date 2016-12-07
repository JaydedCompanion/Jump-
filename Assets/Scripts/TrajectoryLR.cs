using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
[ExecuteInEditMode]
public class TrajectoryLR : MonoBehaviour {

	public int Resolution;
	public float xDist;
	public float Height;

	public Transform Dot;
	public Material Blue;
	public Material Red;
	public int BelongsToID;
	public bool canJump;
	private Player p;

	private Color col;
	private TrajectoryLR parent;
	private LineRenderer LR;
	private JumpManager Man;

	// Use this for initialization
	void Start () {

		parent = GetComponentInParent<TrajectoryLR> ();

		LR = GetComponent<LineRenderer> ();
		LR.enabled = false;

		Man = FindObjectOfType<JumpManager> ();

		p = FindObjectOfType<Player> ();
	
	}

	void LateUpdate (){

		transform.rotation = Quaternion.Euler (0, 0, 0);

	}
	
	// Update is called once per frame

	void Update () {

		ChangeCol ();

		if (parent != null) {

			Resolution = parent.Resolution;

			xDist = parent.xDist;

			Height = parent.Height;

			BelongsToID = parent.BelongsToID;

		}
		
		int i = 0;

		Vector3 [] Points = new Vector3 [Resolution];

		while (i < Resolution) {

			float x = ((float) i / Resolution);
			float xRange = 2 * (-Man.JumpForce [BelongsToID] * Man.UniversalBaseJumpForce) / Physics.gravity.y;

			x *= xRange;

			Points [i] = new Vector3 (

				x * Man.UniversalMovementSpeed * 0.035f,

				//Insert matermatical equation without 'y=' below:
				//-(Mathf.Pow (x - (xDist/2), 2)) + Height
				Man.JumpForce[BelongsToID] * Man.UniversalBaseJumpForce * x + 0.5f * Physics.gravity.y * Mathf.Pow (x, 2)
				//Mathf.Sin (x)

			);

			i++;

			if (i >= Resolution) {

				Dot.transform.localPosition = Points [i - 1];

			}

		}

		LR.numPositions = Resolution;

		LR.SetPositions (Points);

		LR.enabled = true;
	
	}

	public void ChangeCol () {

		if (transform.parent != null) {

			col = LR.material.color;

			Vector3 HSV = new Vector3 ();
			Color.RGBToHSV (col, out HSV.x, out HSV.y, out HSV.z);
			HSV.z = (canJump) ? 1f : 0.5f;

			col = Color.HSVToRGB (HSV.x, HSV.y, HSV.z);
		} else {

			col = Blue.color;

		}

		LR.material.color = col;
		Dot.GetComponent<SpriteRenderer> ().material.color = col;
		transform.GetChild (0).GetComponent<SpriteRenderer> ().material.color = col;
	
	}

}