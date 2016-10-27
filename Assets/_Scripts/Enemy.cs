using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public float speed = 10f;
	public float fireRate = 0.3f;
	public float health = 10;
	public int score = 100;

	public bool ______________;

	public Bounds bounds; 
	public Vector3 boundsCenterOffset;

	
	// Update is called once per frame
	void Update () {
		Move ();
	}
	public virtual void Move(){
		Vector3 tempPos = pos;
		tempPos.y -=speed * Time.deltaTime;
		pos = tempPos;
}
	public Vector3 pos{
		get{
			return (this.transform.position);
		}
		set {
			this.transform.position = value;
		}
	}
}
