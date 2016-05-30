using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Network : NetworkManager {

	private int players = 0;
	public GameObject ballPrefab;

	private GameObject ball;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);
		players++;
		if (players == 2)
			ball = (GameObject) Instantiate (spawnPrefabs[0], Vector3.zero, Quaternion.identity);
		if (ball != null)
			NetworkServer.Spawn (ball);
		
		Debug.Log ("CLIENT ADDED");
	}

	public override void OnServerDisconnect (NetworkConnection conn)
	{
		base.OnServerDisconnect (conn);
		if (ball != null)
			Destroy (ball);
		players--;
	}
}
