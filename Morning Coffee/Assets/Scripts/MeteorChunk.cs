using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorChunk : MonoBehaviour
{

    void OnEnable()
    {
        GetComponent<Rigidbody>().AddForce(Random.Range(-100, 100), Random.Range(100, 250), Random.Range(-100, 100));
		Destroy(gameObject,1.5f);
    }

	void Update(){
		transform.Rotate(Random.Range(-5, 5),Random.Range(-5, 5),Random.Range(-5, 5));
	}
}
