using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Ball : NetworkBehaviour {

	// Konstante Geschwindigkeit
	public float cSpeed = 10f;
	// Smoothing
	public float sFactor = 10f;

	static int playerScore = 0;
	static int enemyScore = 0;

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.AddForce (Random.Range(0f, cSpeed),Random.Range(0f, cSpeed),Random.Range(0f, cSpeed));
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cVel = rb.velocity;
		Vector3 tVel = cVel.normalized * cSpeed;
		rb.velocity = Vector3.Lerp (cVel, tVel, Time.deltaTime * sFactor);
	}

	void OnTriggerExit() {
		if (transform.position.x < 0)
			enemyScore++;
		else
			playerScore++;
		transform.position = Vector3.zero;
	}
}
