using System;
using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;

public class CreateBuildings : MonoBehaviour
{
    [SerializeField] private Global global;
    [SerializeField] private TextMeshProUGUI hoverText;
    private LayerMask useLayer = 1 << 7, plotLayer;

    private float maxUseDistance = 100f;
    private bool plotting = false;
    private RaycastHit hit;
    private Transform tempBuild, finalBuild, buildShow, hoverPlot;

    private void Update()
    {
        if (global.busy || global.disabled)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (!isOverUI)
        {
            if (plotting && Physics.Raycast(ray, out hit, maxUseDistance, plotLayer) && tempBuild)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                    CancelBuild();
                else if (hit.transform.CompareTag("Plot") && Input.GetKeyDown(KeyCode.Mouse0))
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
                if (hit.collider.CompareTag("Building")) // Empty plot
                {
                    hoverText.SetText("Here stands the " + hit.transform.name + ".");
                    hoverText.gameObject.SetActive(true);
                }
            }
            else
                hoverText.gameObject.SetActive(false);
        }
        else
            hoverText.gameObject.SetActive(false);
    }

    public void BuildingSpecs(string curBuilding, string curName, int layerSize)
    {
        finalBuild = Instantiate(Resources.Load<GameObject>(curBuilding), null).transform;   // End building
        finalBuild.name = curName;
        
        plotLayer = (1 << layerSize + 8) | (1 << 6);    // Mouse hit layers
        
        tempBuild = Instantiate(finalBuild, null);  // Temp building
        buildShow = tempBuild.transform.GetChild(tempBuild.transform.childCount - 2);   // Temp build lvl1
        buildShow.gameObject.SetActive(true);
        
        plotting = true;
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
    
    private void ChangeMats(bool inPlot)
    {
        foreach (Transform child in buildShow)
        {
            if (child.GetComponent<Renderer>() != null)
                child.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
            else
            {
                foreach (Transform grandChild in child)
                {
                    if (grandChild.GetComponent<Renderer>() != null)
                        grandChild.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
                    else
                    {
                        foreach (Transform greatGrandChild in grandChild)
                        {
                            if (greatGrandChild.GetComponent<Renderer>() != null)
                                greatGrandChild.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
                            else
                            {
                                foreach (Transform greatGreatGrandChild in greatGrandChild)
                                {
                                    if (greatGreatGrandChild.GetComponent<Renderer>() != null)
                                        greatGreatGrandChild.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
                                    else
                                    {
                                        foreach (Transform triGreatGrandChild in greatGreatGrandChild)
                                        {
                                            if (triGreatGrandChild.GetComponent<Renderer>() != null)
                                                triGreatGrandChild.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
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