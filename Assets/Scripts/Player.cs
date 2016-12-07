using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public GameObject Trajectory;
	public GameObject DeathParticle;
	public GameObject HighScoreTrail;
	public GameObject TrailEraser;
	public ParticleSystem Grindr;
	public PhysicsMaterial2D DeathMat;
	public AudioClip Jump;
	public AudioClip Land;
	public AudioClip Crash;
	public bool onJump;
	public bool isDead;
	public float Score;

	private JumpManager Man;

	public Rigidbody2D RB;
	private Animator Anim;
	private GameObject ExistingTrajectory;
	[SerializeField]
	private int PlayerID;
	[SerializeField]
	public bool Grounded;
	private bool onJump_Prev;
	private bool Debugging;
	private float Speed;
	private float JumpForce;

	private float ScoreOffset;
	private float DeltaScore;
	private float PrevScore;

	PlayerDebugger Debugger;

	// Use this for initialization
	void Start () {
		
		Man = GameObject.FindGameObjectWithTag("Manager").GetComponent<JumpManager>();
		PlayerID = Man.Join (this);

		RB = GetComponent<Rigidbody2D> ();

		Anim = GetComponentInChildren<Animator> ();

		Speed = Man.UniversalMovementSpeed;

		ScoreOffset = transform.position.x;

		Score -= ScoreOffset;

		try {

			Debugger = GetComponent<PlayerDebugger> ();

			Debugging = true;
		
		} catch {

			Debugging = false;

		}
		
	}
	
	// Update is called once per frame

	void Update () {

		if (Application.isEditor) {

			Speed = Man.UniversalMovementSpeed;

		}

		if (isDead) {

			if (Grounded) {

				Grindr.emissionRate = RB.velocity.x * RB.velocity.x;

			} else {

				Grindr.emissionRate = 0;
			
			}

		}

		if (!Man.Paused && !isDead){

			onJump = Man.Jumping [PlayerID];

			Vector2 vel = RB.velocity;
			vel.x = Speed * 0.035f;
			//vel.x = Speed * Time.deltaTime;

			if (onJump_Prev && !onJump) {

				if (Grounded){

					AudioSource.PlayClipAtPoint (Jump, Camera.main.transform.position);

					if (!Debugger.UseUniformJumpHeight) {

						vel.y = (Man.UniversalBaseJumpForce * Man.JumpForce [PlayerID]);

					} else {

						vel.y = Man.UniversalBaseJumpForce;

					}

				}

				Man.JumpForce [PlayerID] = 0;

			}

			RB.velocity = vel;

			//===Score Management Code===//

			DeltaScore = transform.position.x - PrevScore;

			Score += DeltaScore / 10;
			PrevScore = transform.position.x;

			Man.Scores [PlayerID] = Score;

			//===Jump Trajectory Code===//

			if (onJump && ExistingTrajectory == null) {

				ExistingTrajectory = Instantiate (Trajectory, transform.GetChild(0).position, Quaternion.Euler (Vector3.zero)) as GameObject;

				ExistingTrajectory.transform.parent = transform.GetChild (0);
				ExistingTrajectory.transform.localPosition = Vector3.zero;

			}

			if (onJump && ExistingTrajectory != null) {

				TrajectoryLR TLR = ExistingTrajectory.GetComponent<TrajectoryLR> ();

				//ExistingTrajectory.transform.localScale = Vector3.one * Man.JumpForce [PlayerID];
				TLR.Height = Man.JumpForce [PlayerID];
				TLR.xDist = RB.velocity.x;
				TLR.BelongsToID = PlayerID;
				TLR.canJump = Grounded;

			} else if (!onJump && ExistingTrajectory != null) {

				ExistingTrajectory.transform.parent = null;
				ExistingTrajectory.GetComponent<TrajectoryLR> ().ChangeCol ();
				ExistingTrajectory.GetComponent<TrajectoryLR> ().enabled = false;

				GameObject EraserInstance = Instantiate (TrailEraser, ExistingTrajectory.transform.position + (Vector3.back * 0.05f), Quaternion.Euler (0, 0, 0)) as GameObject;
				EraserInstance.GetComponent<Rigidbody2D> ().velocity = vel;
				Physics2D.IgnoreCollision (GetComponent<Collider2D> (), EraserInstance.GetComponent<Collider2D> ());
				Physics2D.IgnoreCollision (GetComponentsInChildren<Collider2D> ()[1], EraserInstance.GetComponent<Collider2D> ());

				ExistingTrajectory = null;

			}

			if (Grounded) {

				Anim.speed = Mathf.Lerp (Anim.speed, 1f, 0.25f);

			} else {

				Anim.speed = Mathf.Lerp (Anim.speed, 0.25f, 0.05f);

			}

			Man.Jumping [PlayerID] = onJump;
			onJump_Prev = onJump;
			
		}
	
	}

	void OnCollisionEnter2D (Collision2D Coll) {

		if (Coll.collider.tag == "Hazard" && !isDead) {

			Die ();

		}

		if (Coll.collider.tag == "Ground") {

			if (!Grounded) {

				AudioSource.PlayClipAtPoint (Land, Camera.main.transform.position);
			
			}

			Grounded = true;

		}

	}

	void OnCollisionExit2D (Collision2D Coll) {

		if (Coll.collider.tag == "Ground") {

			Grounded = false;

		}

	}

	void Die () {

		AudioSource.PlayClipAtPoint (Crash, Camera.main.transform.position);

		Instantiate (DeathParticle, transform.GetChild(0).position, Quaternion.Euler (Vector3.zero));

		Vector2 vel = RB.velocity;
		vel.x = Speed * 0.035f;
		vel.y = Man.UniversalBaseJumpForce * 0.6f;

		if (ExistingTrajectory != null) {

			ExistingTrajectory.transform.parent = null;

			ExistingTrajectory = null;

		}

		GetComponent<Collider2D> ().sharedMaterial = DeathMat;

		RB.velocity = vel;

		Anim.SetTrigger ("Die");

		//Remove the player from the JumpJumpJumpJumpJumpJumpJumpManager's data

		Man.Leave (this);

		//Remove the debugger, as it depends on this script and will stop player's destruction unless debugger is destroyed first

		if (Debugging) {

			Destroy (Debugger);

		}

		//Disable the majority of the script

		isDead = true;

	}

}
