using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CreateBuildings : MonoBehaviour
{
    [SerializeField] private Global global;
    private LayerMask useLayer = 1 << 7, plotLayer;

    private float maxUseDistance = 100f;
    private bool plotting, upgrading;
    private RaycastHit hit;
    private Transform tempBuild, finalBuild, buildShow, hoverPlot, upgHover, upgButton;
    
    [SerializeField] private GameObject dlcScreen;

    private void Start()
    {
        plotting = upgrading = false;
        upgButton = transform.GetChild(3);
    }
    
    private void Update()
    {
        if (global.busy || global.disabled)
            return;

        if (!upgrading && Input.GetKeyDown(KeyCode.LeftControl))
            UpgradeToggle();
        else if (upgrading && (Input.GetKeyUp(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.Mouse1)))
            UpgradeToggle();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOverUI = EventSystem.current.IsPointerOverGameObject();

        if (!isOverUI)
        {
            if (plotting && Physics.Raycast(ray, out hit, maxUseDistance, plotLayer) && tempBuild)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                    CancelBuild();
                if (hit.transform.CompareTag("Plot") && Input.GetKeyDown(KeyCode.Mouse0))
                    PlaceBuilding();
                
                if (!plotting || hit.point == tempBuild.position)
                    return;
                
                if (hit.transform.CompareTag("Plot"))
                {
                    hoverPlot = hit.transform;
                    tempBuild.position = new Vector3(hoverPlot.position.x, tempBuild.position.y, hoverPlot.position.z);
                    tempBuild.rotation = hoverPlot.rotation;
                    ChangeMats(true);
                }
                else
                {
                    tempBuild.position = hit.point;
                    ChangeMats(false);
                }
            }
            else if ((Physics.Raycast(ray, out hit, maxUseDistance, useLayer)))
            {
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (upgrading && hit.collider.CompareTag("Building")) // Building
                    {
                        upgHover = hit.transform;
                        for (int i = 0; i < upgHover.childCount; i++)
                        {
                            if (upgHover.GetChild(i) && (i + 1 != upgHover.childCount)) // Checking if can upgrade
                            {
                                if (upgHover.GetChild(i).gameObject.activeSelf && upgHover.GetChild(i + 1))
                                {
                                    upgHover.GetChild(i).gameObject.SetActive(false);
                                    upgHover.GetChild(i + 1).gameObject.SetActive(true);
                                    return;
                                }
                            }
                        }
                    }
                    else if (hit.collider.CompareTag("EasterEgg"))
                        dlcScreen.SetActive(true);
                }
            }
        }
    }

    public void BuildingSpecs(string curBuilding, string curName, int layerSize)
    {
        finalBuild = Instantiate(Resources.Load<GameObject>(curBuilding), null).transform;   // End building
        finalBuild.name = curName;
        
        plotLayer = (1 << layerSize + 8) | (1 << 6);    // Mouse hit layers
        
        tempBuild = Instantiate(finalBuild, null);  // Temp building
        buildShow = tempBuild.transform.GetChild(tempBuild.transform.childCount - 2);   // Temp build lvl1
        plotting = true;
        buildShow.gameObject.SetActive(true);
    }
    private void PlaceBuilding()
    {
        plotting = false;
        hoverPlot.GetComponent<BoxCollider>().enabled = false;
        hoverPlot.GetComponent<MeshRenderer>().enabled = false;
        
        if (hoverPlot.parent.CompareTag("Plot"))
        {
            foreach (Transform plot in hoverPlot.parent)
            {
                if (plot.gameObject.layer != hoverPlot.gameObject.layer)
                    plot.gameObject.SetActive(false);
            }
        }
        
        finalBuild.position = new Vector3(hoverPlot.position.x, tempBuild.position.y, hoverPlot.position.z);
        finalBuild.rotation = hoverPlot.rotation;
        
        finalBuild.GetChild(finalBuild.childCount - 2).gameObject.SetActive(true);
        finalBuild.parent = hoverPlot;
        finalBuild.tag = "Building";
        finalBuild.gameObject.layer = LayerMask.NameToLayer("Usable");
        CancelBuild();
    }
    
    public void CancelBuild()
    {
        if (finalBuild && !finalBuild.CompareTag("Building"))
            Destroy(finalBuild.gameObject);
        if (tempBuild)
            Destroy(tempBuild.gameObject);
    }

    public void UpgradeToggle()
    {
        upgrading = !upgrading;
        
        upgButton.GetComponent<Button>().enabled = !upgrading;
        upgButton.GetComponent<Image>().raycastTarget = !upgrading;
        upgButton.GetChild(1).gameObject.SetActive(upgrading);
    }

    private void ChangeMats(bool inPlot)
    {
        Material placeMat = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
        
        foreach (Transform child in buildShow)
        {
            if (child.GetComponent<Renderer>() != null)
                child.GetComponent<Renderer>().material = placeMat;
            else
            {
                foreach (Transform grandChild in child)
                {
                    if (grandChild.GetComponent<Renderer>() != null)
                        grandChild.GetComponent<Renderer>().material = placeMat;
                    else
                    {
                        foreach (Transform greatGrandChild in grandChild)
                        {
                            if (greatGrandChild.GetComponent<Renderer>() != null)
                                greatGrandChild.GetComponent<Renderer>().material = placeMat;
                            else
                            {
                                foreach (Transform greatGreatGrandChild in greatGrandChild)
                                {
                                    if (greatGreatGrandChild.GetComponent<Renderer>() != null)
                                        greatGreatGrandChild.GetComponent<Renderer>().material = placeMat;
                                    else
                                    {
                                        foreach (Transform triGreatGrandChild in greatGreatGrandChild)
                                        {
                                            if (triGreatGrandChild.GetComponent<Renderer>() != null)
                                                triGreatGrandChild.GetComponent<Renderer>().material = placeMat;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}