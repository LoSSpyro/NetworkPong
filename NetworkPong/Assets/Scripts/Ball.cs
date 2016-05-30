using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Ball : NetworkBehaviour {

	// Konstante Geschwindigkeit
	public float cSpeed = 10f;
	// Smoothing
	public float sFactor = 10f;

	public Movement player;
	public Movement enemy;

	private Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Movement> ();
		enemy = GameObject.FindGameObjectWithTag ("Enemy").GetComponent<Movement> ();
		rb.AddForce (Random.Range(-cSpeed, cSpeed),Random.Range(-cSpeed, cSpeed),Random.Range(-cSpeed, cSpeed));
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cVel = rb.velocity;
		Vector3 tVel = cVel.normalized * cSpeed;
		rb.velocity = Vector3.Lerp (cVel, tVel, Time.deltaTime * sFactor);
		rb.AddForce (Vector3.right * Mathf.Abs (transform.position.x) * 0.1f * Time.deltaTime);
	}

	void OnTriggerExit() {
		if (transform.position.x < 0) {
			enemy.addScore ();
		} else {
			player.addScore ();
		}
		transform.position = Vector3.zero;
		rb.velocity = Vector3.zero;
		rb.AddForce (Random.Range(0f, cSpeed),Random.Range(0f, cSpeed),Random.Range(0f, cSpeed));
	}
}
