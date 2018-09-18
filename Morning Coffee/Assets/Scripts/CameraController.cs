using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public Vector3 velocity;
	public float smoothSpeed = .15f;
	void LateUpdate(){
		if(target != null){
            Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
        transform.position = smoothedPosition;
	}

}
}
