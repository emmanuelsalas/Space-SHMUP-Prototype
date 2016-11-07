using UnityEngine;
using System.Collections;

public class Enemy_1 : Enemy {
	// Because Enemy_1 extends Enemy, the _____ bool won't work 
	// the same way in the Inspector pane. :/

	// # seconds for a full sine wave 
	public float waveFrequency = 2; 
	// sine wave width in meters

	public float waveWidth = 4;
	public float waveRotY = 45;

	private float x0 = -12345; 
	private float birthTime;

	void Start() {
		x0 = pos.x;

		birthTime = Time.time;
	}

	//Override the move function on enemy
	public override void Move () {
		Vector3 tempPos = pos;
		float age = Time.time - birthTime;
		float theta = Mathf.PI * 2 * age / waveFrequency;
		float sin = Mathf.Sin (theta);
		tempPos.x = x0 + waveWidth * sin;
		pos = tempPos;

		Vector3 rot = new Vector3 (0, sin * waveRotY, 0);
		this.transform.rotation = Quaternion.Euler (rot);

		//base.Move() still handles the movement down in y
		base.Move ();
	}
}