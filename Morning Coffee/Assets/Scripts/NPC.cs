using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject shield;
    public bool hasShield = false;
    public LayerMask layerMask;
    public LayerMask meteorMask;
    public int randX;
    public int randY;
    public Animator anim;
    public float walkTimeOG;
    public float walkTime = 5f;
    public float walkTimer;
    public float squatTimeOG;
    public float squatTime = 1f;
    public float squatTimer;
    public Vector3 walkDir;
    public bool walking = false;
    public bool squatting = false;
    public GameObject skinContainer;
    public GameObject[] skins;
    public GameObject[] messages;
    public int messageIndex;
    public bool showingMessage = false;



    void Start()
    {
        anim = GetComponent<Animator>();
        walkTimeOG = walkTime = Random.Range(.6f, 3f);

        squatTimeOG = squatTime = walkTime * .05f;
        walkTimer = walkTime;
        squatTimer = squatTime;
    }

    void OnEnable()
    {
        skins[Random.Range(0, skins.Length)].SetActive(true);
    }

    void Update()
    {

        RaycastHit meteorDetect;
        Physics.Raycast(transform.position, Vector3.up * 25, out meteorDetect, 25, meteorMask);
        if (meteorDetect.collider != null)
        {
            if (!showingMessage)
            {
                messageIndex = Random.Range(0, messages.Length);
                messages[messageIndex].SetActive(true);
                showingMessage = true;
                //walkTimer = 0;    (REACTING)

            }

        }
        else
        {
            messages[messageIndex].SetActive(false);
            showingMessage = false;

        }


        WalkTick();
        SquatTick();
    }

    public void ActivateSheild()
    {
        hasShield = true;
        shield.SetActive(true);
        anim.SetTrigger("growShield");
        
        //anim.SetTrigger("");
    }

    public void DeactivateSheild()
    {
        hasShield = false;
        shield.SetActive(false);
    }

    public void WalkTick()
    {
        walkTimer -= Time.deltaTime;
        if (walkTimer <= 0)
        {
            //START SQUAT
            squatting = true;

            randX = Random.Range(-1, 2);

            if (randX != 0)
            {
                randY = 0;
                skinContainer.transform.localRotation = Quaternion.Euler(0, 90 * randX, 0);
            }
            else
            {
                randY = Random.Range(-1, 2);
                if (randY > 0)
                {
                    skinContainer.transform.localRotation = Quaternion.Euler(0, 0, 0);
                }
                else if (randY < 0)
                {
                    skinContainer.transform.localRotation = Quaternion.Euler(0, 180 * randY, 0);
                }
            }
            anim.SetBool("squatting", true);
            walkTimer = walkTime;
        }
    }
    public void SquatTick()
    {
        if (squatting)
        {
            squatTimer -= Time.deltaTime;

            if (squatTimer <= 0)
            {
                squatTimer = squatTime;
                squatting = false;
                anim.SetBool("squatting", false);

                RaycastHit hit;
                Physics.Raycast(transform.position + Vector3.up * .1f, new Vector3(randX, 0, randY), out hit, 1.5f, layerMask);
                Debug.DrawRay(transform.position, new Vector3(randX, 0, randY) * 1.5f, Color.red, 1f);
                if (hit.collider == null)
                {
                    Walk(randX, randY);

                }
            }
        }
    }

    public void Walk(int _x, int _y)
    {

        walkDir = new Vector3(randX, 0, randY);



        transform.Translate(walkDir);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "target")
        {


        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "target")
        {

        }
    }
}
