using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Movement : NetworkBehaviour {

	// The Speed of the Object
	public int speed = 10;

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

	public override void OnStartLocalPlayer() {
	}
}
