using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TunnelsPlayer : MonoBehaviour {

	public LayerMask RayLayer;
	public float BoostSpeed;
	public float MinSpeed;
	public float Drag;
	public float xSpeed;
	public float yPos;

	private TunnelsManager Man;
	private RaycastHit RayHit;
	private LineRenderer LR;
	private Camera cam;
	private Vector3 PosToUse;
	public bool Hit;

	// Use this for initialization
	void Start () {

		Man = FindObjectOfType<TunnelsManager> ();

		Man.Player = this;

		LR = GetComponent<LineRenderer> ();
		LR.numPositions = 2;

		cam = FindObjectOfType<Camera> ();
		
	}

	void FixedUpdate () {

	}
	
	// Update is called once per frame
	void Update () {

		if (xSpeed > MinSpeed) {
			xSpeed += Mathf.Clamp (MinSpeed - xSpeed, -Drag, Drag) * Time.deltaTime;
		} else {
			xSpeed = MinSpeed;
		}

		transform.Translate (Vector2.right * xSpeed * Time.deltaTime);

		//----Maintain the raycaster and linerenderer

		Hit = false;

		if (Application.isMobilePlatform) {

			if (Input.touchCount >= 1) {

				Hit = Physics.Raycast (cam.ScreenPointToRay (Input.touches [0].position), out RayHit, 100, RayLayer);

			}

		} else {

			if (Input.GetButton ("Fire1")) {

				Hit = Physics.Raycast (cam.ScreenPointToRay (Input.mousePosition), out RayHit, 100, RayLayer);

			}
		
		}

		LR.SetPosition (0, transform.position);

		if (Hit) {
			PosToUse = RayHit.point;
		} else {
			PosToUse = Vector3.Lerp (PosToUse, transform.position, 4 * Time.unscaledDeltaTime);
		}
		PosToUse.z = transform.position.z;
		LR.SetPosition (1, PosToUse);

		yPos = RayHit.point.y;

		Vector3 pos = transform.position;
		pos.y = Mathf.Lerp(pos.y, yPos, 2 * Time.deltaTime);
		transform.position = pos;
		
	}

	public void Boost () {

		Debug.Log ("Boosting");

		xSpeed += BoostSpeed;

	}

	public void Missed () {

		Debug.Log ("Missed");

		xSpeed *= 0.75f;

	}

}
