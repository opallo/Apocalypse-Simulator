using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorTarget : MonoBehaviour {

	public float growRate;
	void Update(){
		transform.localScale += new Vector3(growRate,0,growRate);
		if (transform.lossyScale.x > 1f){
			Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "meteor"){
			Destroy(gameObject);
		}
	}
}
