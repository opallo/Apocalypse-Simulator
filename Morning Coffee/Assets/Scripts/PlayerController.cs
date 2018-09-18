using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public LayerMask collisionMask;
    public CollisionInfo collisions;
    public float rayLength = 1;
    public Vector3 rayOriginOffset = new Vector3(0, 0, 0);
    public Animator anim;
    public Transform skin;
    public Vector3 gravity = new Vector3(0, -1f, 0);
	public GameObject targetObject;
    public GameObject[] skins;
    public float maxClimbHeight;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit forwardHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.forward, out forwardHit, rayLength, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.forward * rayLength, Color.red);
        if (forwardHit.collider != null)
        {
            collisions.forward = true;
        }
        RaycastHit backHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.back, out backHit, rayLength, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.back * rayLength, Color.red);
        if (backHit.collider != null)
        {
            collisions.back = true;
        }
        RaycastHit leftHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.left, out leftHit, rayLength, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.left * rayLength, Color.red);
        if (leftHit.collider != null)
        {
            collisions.left = true;
        }
        RaycastHit rightHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.right, out rightHit, rayLength, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.right * rayLength, Color.red);
        if (rightHit.collider != null)
        {
            collisions.right = true;
        }
        RaycastHit upHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.up, out upHit, rayLength * 2, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.up * rayLength * 2, Color.red);
        if (upHit.collider != null)
        {
            collisions.up = true;
        }
        RaycastHit downHit;
        Physics.Raycast(transform.position + rayOriginOffset, Vector3.down, out downHit, rayLength, collisionMask);
        Debug.DrawRay(transform.position + rayOriginOffset, Vector3.down * rayLength, Color.red);
        if (downHit.collider != null)
        {
            collisions.down = true;

			if(downHit.collider.gameObject.tag == "NPC"){
				targetObject = downHit.collider.gameObject;
			}else{
				targetObject = null;
			}
			
        }

        if (!collisions.down)
        {
            transform.position += gravity;
        }

        collisions.DebugCollisions();
        collisions.Reset();
    }

    public void Move(Vector2 _input)
    {

        //TODO: CHECK IF MOVABLE FIRST!

        if (_input != Vector2.zero)
        {
            RaycastHit hit;
            RaycastHit upHit;
            RaycastHit diagHit;
            Physics.Raycast(transform.position + rayOriginOffset, new Vector3(_input.x, -.5f, _input.y), out hit, rayLength, collisionMask);
            Physics.Raycast(transform.position + rayOriginOffset, Vector3.up, out upHit, rayLength * 2, collisionMask);
            Physics.Raycast(transform.position + rayOriginOffset, new Vector3(_input.x, 2, _input.y), out diagHit, rayLength * 2, collisionMask);

            if (hit.collider != null && upHit.collider == null && diagHit.collider == null && hit.collider.transform.lossyScale.y < maxClimbHeight)
            {
                transform.position += new Vector3(_input.x, hit.collider.transform.lossyScale.y, _input.y);
            }
            else if (hit.collider == null)
            {
                transform.position += new Vector3(_input.x, 0, _input.y);
            }
        }
        else
        {
            
        }

    }

    public struct CollisionInfo
    {
        public bool forward;
        public bool back;
        public bool left;
        public bool right;
        public bool up;
        public bool down;
        public void Reset()
        {
            forward = back = false;
            left = right = false;
            up = down = false;
        }
        public void DebugCollisions()
        {
            //Debug.Log("forward: " + forward + " back: " + back + " left: " + left + " right: " + right + " up: " + up + " down: " + down);
        }
    }
}
