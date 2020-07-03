using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;



public class ClickManager : MonoBehaviour
{
    #region Singleton

    public static ClickManager instance;
    void Awake()
    {
        if (instance != null)
        {
            print("More than one instance of ClickManager found");
            return;
        }
        instance = this;
    }

    #endregion

    RaycastHit hit;
    public Vector3 hoveredLocation;
    public Vector3 clickedLocation;
    public GameObject hoveredInteractable;
    public GameObject clickedInteractable;
    public GameObject clickMarker;
    public GameObject infoHoverPanel;

    private void Start()
    {
        clickedLocation = Player.instance.targetLocation;
    }

    void Update()
    {

        UpdateDebugText();

        //Send ray towards screen from mouse
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
        {
            print("hi");
            if (MouseOnInteractable())
            {
                //Turn on info panel
                infoHoverPanel.SetActive(true);
                //Get pick-up text from interactable class
                infoHoverPanel.GetComponentInChildren<Text>().text = hit.transform.gameObject.GetComponent<Interactable>().textOption;
                //Set info panel transform to mouse location

                Vector3 infoHoverLocation = new Vector3(
                    Input.mousePosition.x + 10,
                    Input.mousePosition.y - 20,
                    Input.mousePosition.z);

                infoHoverPanel.transform.position = infoHoverLocation;
                //Set hovered interactable variable to raycast hit
                hoveredInteractable = hit.transform.gameObject;
                if (Input.GetMouseButtonDown(0))
                {
                    clickedInteractable = hit.transform.gameObject;
                }
            }
            else
            {
                infoHoverPanel.GetComponentInChildren<Text>().text = null;
                infoHoverPanel.SetActive(false);
                hoveredInteractable = null;
            }


            if (MouseOnNavMesh())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 clickMarkerLocation = new Vector3(hit.point.x, hit.point.y + 1, hit.point.z);
                    Destroy(GameObject.Find("ClickMarker(Clone)"));
                    Instantiate(clickMarker, clickMarkerLocation, Quaternion.identity);
                    clickedLocation = hit.point;
                }
            }
        }
    }

    //Function to check if the mouse raycast is hitting an interactable object
    bool MouseOnInteractable()
    {
        if (hit.transform.gameObject.layer == 9)
        {
            return true;
        }
        return false;
    }

    //Function to check if the mouse raycast is hitting a navmesh
    bool MouseOnNavMesh()
    {
        //Check if raycast is on Navmesh
        NavMeshPath path = new NavMeshPath();
        if (NavMesh.CalculatePath(Vector3.zero, hit.point, NavMesh.AllAreas, path))
        {
            hoveredLocation = hit.point;
            return true;
        }
        return false;
    }

    #region Debug Tools
    //Debug tools
    public GameObject
        debugText;
    void UpdateDebugText()
    {
        debugText.GetComponent<Text>().text = "Hovered Location: " + hoveredLocation.ToString()
            + "\nClicked Location: " + clickedLocation.ToString();
        if (hoveredInteractable != null)
        {
            debugText.GetComponent<Text>().text += "\nHovered Interactable: " + hoveredInteractable.name;
        }
        else
        {
            debugText.GetComponent<Text>().text += "\nHovered Interactable: None";
        }
        if (clickedInteractable != null)
        {
            debugText.GetComponent<Text>().text += "\nClicked Interactable: " + clickedInteractable.name;
        }
        else
        {
            debugText.GetComponent<Text>().text += "\nClicked Interactable: None";
        }
    }
    #endregion

}
