using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class TouchManager : MonoBehaviour
{
    public bool inMenu;
    Vector2 touchStart;
    Vector2 swipes;
    PlayerController playerController;
    public ScoreManager scoreManager;

    void Start()
    {
        inMenu = false;
        playerController = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        switch (inMenu)
        {
            case true:               

                break;

            case false:
                if (playerController.targetObject != null)
                {
                    Debug.Log("o");
                    //GIVE OBJECT
                    if (!playerController.targetObject.GetComponentInParent<NPC>().hasShield)
                    {
                        playerController.targetObject.GetComponentInParent<NPC>().ActivateSheild();
                        scoreManager.AddPoint();              
                    }
                }

                if (Input.touchCount > 0)
                {

                    Touch touch = Input.GetTouch(0);

                    if (touch.phase == TouchPhase.Began)
                    {
                        touchStart = touch.position;
                        playerController.anim.SetBool("squatting", true);

                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        
                        Vector2 deltaSwipe = touch.position - touchStart;
                        if (Mathf.Abs(deltaSwipe.x) > 6f || Mathf.Abs(deltaSwipe.y) > 6f)   //CHECKS FOR SUBSTANTIAL SWIPE
                        {
                            if (Mathf.Abs(deltaSwipe.x) > Mathf.Abs(deltaSwipe.y))
                            {
                                //HORIZONTAL SWIPE
                                if (deltaSwipe.x < 0)
                                {
                                    swipes = new Vector2(-1, 0);
                                    playerController.skin.localRotation = Quaternion.Euler(0, -90, 0);
                                }
                                else
                                {
                                    swipes = new Vector2(1, 0);
                                    playerController.skin.localRotation = Quaternion.Euler(0, 90, 0);
                                }

                            }
                            else if (Mathf.Abs(deltaSwipe.x) < Mathf.Abs(deltaSwipe.y))
                            {
                                //VERTICAL SWIPE
                                if (deltaSwipe.y < 0)
                                {
                                    swipes = new Vector2(0, -1);
                                    playerController.skin.localRotation = Quaternion.Euler(0, 180, 0);
                                }
                                else
                                {
                                    swipes = new Vector2(0, 1);
                                    playerController.skin.localRotation = Quaternion.Euler(0, 0, 0);
                                }
                            }

                            Debug.Log(swipes.ToString());

                        }
                    }

                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (swipes != Vector2.zero)
                        {
                            playerController.Move(swipes);

                        }

                        /* 
                    else if (playerController.targetObject != null)
                    {

                        Debug.Log("o");
                        //GIVE OBJECT
                        if (!playerController.targetObject.GetComponentInParent<NPC>().hasSheild)
                        {
                            playerController.targetObject.GetComponentInParent<NPC>().ActivateSheild();
                        }
                    }
                        */

                        else
                        {
                            //playerController.Move(new Vector2(0, -1));
                            //playerController.skin.localRotation = Quaternion.Euler(0, 180, 0);
                        }
                        swipes = Vector2.zero;
                        playerController.anim.SetBool("squatting", false);
                    }

                }


                break;

            default:
                break;
        }
       
    }

    public void ActivateMenu(){
        inMenu = true;
    }

    public void DecactivateMenu(){
        inMenu = false;
    }

}
