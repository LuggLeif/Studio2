using Mono.Cecil.Cil;
using TMPro;
using UnityEngine;

public class CreateBuildings : MonoBehaviour
{
    [SerializeField] private Global Global;
    [SerializeField] private TextMeshProUGUI HoverText;
    private LayerMask UseLayer = 1 << 7, plotLayer;

    private float MaxUseDistance = 30f;
    private bool plotting = false;
    private RaycastHit hit;
    private Transform building, hoverPlot;
    private Material finalMat;

    private void Update()
    {
        if (Global.Busy)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if (!isOverUI)
        {
            if (plotting && Physics.Raycast(ray, out hit, MaxUseDistance, plotLayer))
            {
                if (hit.transform.CompareTag("Plot") && Input.GetKeyDown(KeyCode.Mouse0))
                    PlaceBuilding();
                
                if (!plotting || hit.point == building.position)
                    return;
                
                if (hit.transform.CompareTag("Plot"))
                {
                    hoverPlot = hit.transform;
                    building.position = new Vector3(hoverPlot.position.x, building.position.y, hoverPlot.position.z);
                    building.rotation = hoverPlot.rotation;
                    ChangeMats(true);
                }
                else
                {
                    building.position = hit.point;
                    ChangeMats(false);
                }
            }
            else if ((Physics.Raycast(ray, out hit, MaxUseDistance, UseLayer)))
            {
                if (hit.collider.CompareTag("Plot"))  // Empty plot
                {  
                    HoverText.SetText("This plot is empty.");
                    HoverText.gameObject.SetActive(true);
                }
                else if (hit.collider.CompareTag("Building"))  // Empty plot
                {  
                    HoverText.SetText("Here stands the " + hit.transform.name + ".");
                    HoverText.gameObject.SetActive(true);
                }
            }
        }
        else
            HoverText.gameObject.SetActive(false);
    }

    public void BuildingSpecs(Transform curBuilding, int layerSize)
    {
        building = curBuilding;
        plotLayer = (1 << layerSize + 8) | (1 << 6);
        GetMat();
        plotting = true;
    }
    private void PlaceBuilding()
    {
        plotting = false;
        hoverPlot.GetComponent<BoxCollider>().enabled = false;
        hoverPlot.GetComponent<MeshRenderer>().enabled = false;
        CheckPlots();
        
        building.parent = hoverPlot;
        building.tag = "Building";
        FinalMat();
    }

    private void GetMat()
    {
        if (building.GetComponent<Renderer>() != null)
            finalMat = building.GetComponent<Renderer>().material;
        else if (building.GetChild(0).GetComponent<Renderer>() != null)
            finalMat = building.GetChild(0).GetComponent<Renderer>().material;
        else if (building.GetChild(0).GetChild(0).GetComponent<Renderer>() != null)
            finalMat = building.GetChild(0).GetChild(0).GetComponent<Renderer>().material;
        else if (building.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>() != null)
            finalMat = building.GetChild(0).GetChild(0).GetChild(0).GetComponent<Renderer>().material;
    }
    private void ChangeMats(bool inPlot)
    {
        foreach (Transform child in building)
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
                                foreach (Transform greatGreatGrandChild in grandChild)
                                {
                                    if (greatGreatGrandChild.GetComponent<Renderer>() != null)
                                        greatGreatGrandChild.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/" + (inPlot ? "Place" : "Wrong"));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void FinalMat()
    {
        foreach (Transform child in building)
        {
            if (child.GetComponent<Renderer>() != null)
                child.GetComponent<Renderer>().material = finalMat;
            else
            {
                foreach (Transform grandChild in child)
                {
                    if (grandChild.GetComponent<Renderer>() != null)
                        grandChild.GetComponent<Renderer>().material = finalMat;
                    else
                    {
                        foreach (Transform greatGrandChild in grandChild)
                        {
                            if (greatGrandChild.GetComponent<Renderer>() != null)
                                greatGrandChild.GetComponent<Renderer>().material = finalMat;
                            else
                            {
                                foreach (Transform greatGreatGrandChild in grandChild)
                                {
                                    if (greatGreatGrandChild.GetComponent<Renderer>() != null)
                                        greatGreatGrandChild.GetComponent<Renderer>().material = finalMat;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void CheckPlots()
    {
        if (hoverPlot.parent.CompareTag("Plot"))
        {
            foreach (Transform plot in hoverPlot.parent)
            {
                if (plot.gameObject.layer != hoverPlot.gameObject.layer)
                    plot.gameObject.SetActive(false);
            }
        }
    }
}