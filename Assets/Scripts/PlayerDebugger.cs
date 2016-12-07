using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]

public class PlayerDebugger : MonoBehaviour {

						public bool UseUniformJumpHeight;

	[SerializeField]	private bool Grounded;

	[SerializeField]	private float PeakHeight;
	[SerializeField]	private float BottomHeight;
	[SerializeField]	private float AbsoluteTravelHeight;

	[SerializeField]	private float JumpTravelDistance;

	private float JumpStartDistance;

	private Player myPlayer;

	// Use this for initialization
	void Start () {

		if (!Application.isEditor) {

			Destroy (this);

		}

		myPlayer = GetComponent<Player> ();

		PeakHeight = transform.position.y;
	
	}
	
	// Update is called once per frame
	void Update () {

		if (!Application.isEditor) {

			Destroy (this);

		}

		if (Grounded != myPlayer.Grounded) {

			if (!myPlayer.Grounded) {

				JumpStartDistance = transform.position.x;

			} else {

				JumpTravelDistance = transform.position.x - JumpStartDistance;

			}

		}

		if (transform.position.y < BottomHeight) {

			BottomHeight = transform.position.y;

		}

		if (transform.position.y > PeakHeight) {

			PeakHeight = transform.position.y;

		}

		AbsoluteTravelHeight = PeakHeight - BottomHeight;

		Grounded = myPlayer.Grounded;
	
	}

}
