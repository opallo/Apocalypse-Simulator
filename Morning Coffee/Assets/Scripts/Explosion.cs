using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {
	void Update () {
		if(transform.lossyScale.x < .9f){
			Destroy(gameObject);
		}
	}
}
