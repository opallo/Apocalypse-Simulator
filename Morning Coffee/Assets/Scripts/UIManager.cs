using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Camera camera;
    public PlayerController player;
    public GameObject[] UISkins;

    GameObject currentSelection;
    


    void Update()
    {
        RaycastHit hit;

        Ray ray = camera.ScreenPointToRay(camera.ViewportToScreenPoint(new Vector3(.5f, .5f, 0)));

        if (Physics.Raycast(ray, out hit))
        {

            if (hit.transform.gameObject.tag == "skin") //CHECK FOR UNLOCK STATUS HERE TOO
            {
                if(hit.transform.gameObject != null && hit.transform.gameObject != currentSelection){
                    
                    currentSelection = hit.transform.gameObject;
                    Debug.Log(currentSelection.gameObject.ToString());

                    foreach(GameObject g in player.skins){
                        g.SetActive(false);
                    }

                    player.skins[currentSelection.GetComponent<SkinInfo>().index].SetActive(true);

                    foreach(GameObject g in UISkins){
                        g.GetComponent<SkinInfo>().Deselect();
                    }

                    currentSelection.GetComponent<SkinInfo>().Select();

                }
            }
        }
    }
}
