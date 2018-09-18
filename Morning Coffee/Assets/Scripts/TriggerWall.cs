using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerWall : MonoBehaviour {

	public Spawner spawner;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "player"){
			//TODO: SPAWN NEW BIOME
			spawner.biomeNumber +=1;
			spawner.SpawnBiome();
			transform.position += new Vector3(0,-1,-6);
			
			spawner.meteorTime *= spawner.meteorRate;

		}
	}

}
