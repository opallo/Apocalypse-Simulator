using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawn : MonoBehaviour {

	public Material[] mats;
	Renderer rend;
	void Start(){
		rend = GetComponent<Renderer>();
		int rand = Random.Range(0, mats.Length);

		rend.sharedMaterial = mats[rand];
	}
}
