﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic; //Required to use lists or dictionaries

public class Main : MonoBehaviour {
	static public Main S;

	public GameObject[] prefabEnemies;
	public float enemySpawnPerSecond = 0.5f;
	public float enemySpawnPadding = 1.5f;
	public WeaponDefinition[] weaponDefinitions;


	public bool __________;

	public WeaponType[] activeWeaponTypes;
	public float enemySpawnRate;

	void Awake (){
		S = this;
		//Set utils.camBounds
		Utils.SetCameraBounds(this.GetComponent<Camera>());
		enemySpawnRate = 1f/enemySpawnPerSecond;
		Invoke ("SpawnEnemy", enemySpawnRate);
	}

	void Start() {
		activeWeaponTypes = new WeaponType[weaponDefinitions.Length]; 
		for ( int i=0; i<weaponDefinitions.Length; i++ ) {
			activeWeaponTypes[i] = weaponDefinitions[i].type; 
		}
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

	public void DelayedRestart( float delay ) {
		// Invoke the Restart() method in delay seconds 
		Invoke("Restart", delay);
	}
	public void Restart() {
		// Reload _Scene_0 to restart the game 
		Application.LoadLevel("_Scene_0");
	}

}
