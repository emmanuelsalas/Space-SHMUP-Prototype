﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Required to use lists or dictionaries

public class Main : MonoBehaviour {
	static public Main S;

	public GameObject[] prefabEnemies;
	public float enemySpawnPerSecond = 0.5f;
	public float enemySpawnPadding = 1.5f;


	public bool __________;

	public float enemySpawnRate;

	void Awake (){
		S = this;
		//Set utils.camBounds
		Utils.SetCameraBounds(this.GetComponent<Camera>());
		enemySpawnRate = 1f/enemySpawnPerSecond;
		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	public void SpawnEnemy(){
		int ndx = Random.Range (0, prefabEnemies.Length);
		GameObject go = Instantiate (prefabEnemies[ndx]) as GameObject;
		Vector3 pos = Vector3.zero;
		float xMin = Utils.camBounds.min.x + enemySpawnPadding;
		float xMax = Utils.camBounds.max.x - enemySpawnPadding;
		pos.x = Random.Range (xMin, xMax);
		pos.y = Utils.camBounds.max.y + enemySpawnPadding;
		go.transform.position = pos;

		Invoke ("SpawnEnemy", enemySpawnRate);
	}
}
