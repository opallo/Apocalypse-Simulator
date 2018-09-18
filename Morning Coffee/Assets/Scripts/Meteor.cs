using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject meteorChunk;
    public float speed = -.5f;
    public bool falling = true;
    public bool active = true;
    public CameraShake camShake;

    void OnEnable()
    {
        camShake = FindObjectOfType<CameraShake>();
    }

    void Update()
    {
        if (falling)
        {
            transform.Translate(0, speed, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ground")
        {
            falling = false;
            StartCoroutine(camShake.Shake(.05f, .05f));

            for (int i = 0; i < 20; i++)
            {
                Instantiate(meteorChunk, transform.position + Vector3.up * .5f, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
            }

            active = false;
            Destroy(gameObject, 1f);
        }
        if (other.tag == "NPC")
        {
            if (active)
            {
                if (other.gameObject.GetComponentInParent<NPC>().hasShield)
                {
                    //other.gameObject.GetComponentInParent<NPC>().DeactivateSheild();
                    for (int i = 0; i < 20; i++)
                    {
                        Instantiate(meteorChunk, transform.position + Vector3.up * .5f, Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)));
                    }

                    //other.gameObject.GetComponentInParent<NPC>().DeactivateSheild();
                    Destroy(gameObject);

                }
                else
                {
                    Destroy(other.transform.gameObject);
                }

            }
        }
        if (other.tag == "player")
        {
            if (active)
            {
                
                    FindObjectOfType<ScoreManager>().RestartGame();
                

            }
        }
    }

}
