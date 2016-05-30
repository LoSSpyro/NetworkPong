using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Movement : NetworkBehaviour {

	// The Speed of the Object
	public int speed = 10;
	private Text score;
	private int points;

	void Start() {
		if (transform.position.x < 0) {
			score = GameObject.FindGameObjectWithTag ("playerScore").GetComponent<Text> ();
			this.tag = "Player";
		} else {
			score = GameObject.FindGameObjectWithTag ("enemyScore").GetComponent<Text> ();
			this.tag = "Enemy";
		}
	}

	// Update is called once per frame
	void Update () {
		// Spieler Input
		if (!isLocalPlayer)
			return;
		
		float h = Input.GetAxisRaw("Vertical");
		transform.Translate (Vector3.up * speed * Time.deltaTime * h);

		// Check Obere grenze
		Vector3 pos = transform.position;
		pos.y = Mathf.Clamp(transform.position.y, -13f, 13f);
		transform.position = pos;
	}

	public void addScore() {
		points++;
		int currPoints = points;
		score.text = "Points: " + points;
		string currtext = score.text;
		CmdChangePoints (currPoints, currtext);
	}

	[Command]
	void CmdChangePoints(int newPoints, string text) {
		points = newPoints;
		score.text = text;
		RpcSyncScore (newPoints, text);
	}

	[ClientRpc]
	void RpcSyncScore (int newPoints, string text) {
			points = newPoints;
			score.text = text;

	}
}
