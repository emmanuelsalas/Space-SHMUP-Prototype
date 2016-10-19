using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour {
	static public Hero S; //Singleton

	//These fields control the movement of the ship
	public float speed = 30;
	public float rollMult = -45;
	public float pitchMult = 30;

	//Ship staus information
	public float shieldLevel = 1;

	public bool ______________;

	public Bounds bounds;

	void Awake() {
		S = this; //Set the singleton
		bounds=Utils.CombineBoundsOfChildren(this.gameObject);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Pull in information from the Input class
		float xAxis = Input.GetAxis("Horizontal");
		float yAxis = Input.GetAxis ("Vertical");

		//Change transform.position based on the axes
		Vector3 pos = transform.position;
		pos.x += xAxis * speed * Time.deltaTime;
		pos.y += yAxis * speed * Time.deltaTime;
		transform.position = pos;

		//Rotate the ship to make it feel more dynamic
		transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

	}
}
