using UnityEngine;
using UnityEngine.Serialization;

public class BuildingSelection : MonoBehaviour
{
    [SerializeField] private GameObject plotMenu;
    [SerializeField] private GameObject buildingMenu;
    [SerializeField] private Transform buildings;

    private Transform currentBuilding;
    private Transform currentPlot;
    private GameObject selectedBuilding;
    

    public void OpenPlotMenu(Transform plot)
    {
        ClosePlotMenu();
        plotMenu.SetActive(true);
        currentPlot = plot;
    }
    public void SelectHouse(int type)
    {
        selectedBuilding = buildings.GetChild(0).GetChild(type).gameObject;
        
        GameObject newBuild = Instantiate(selectedBuilding, currentPlot.position, currentPlot.rotation, null);
        newBuild.transform.parent = currentPlot;

        newBuild.transform.name = selectedBuilding.transform.name;
        currentPlot.GetComponent<BoxCollider>().enabled = false;
        currentPlot.GetComponent<MeshRenderer>().enabled = false;
        plotMenu.SetActive(false);
    }
    public void SelectVendor(int type)
    {
        selectedBuilding = buildings.GetChild(1).GetChild(type).gameObject;
        
        GameObject newBuild = Instantiate(selectedBuilding, currentPlot.position, currentPlot.rotation, null);
        newBuild.transform.parent = currentPlot;

        newBuild.transform.name = selectedBuilding.transform.name;
        currentPlot.GetComponent<BoxCollider>().enabled = false;
        currentPlot.GetComponent<MeshRenderer>().enabled = false;
        plotMenu.SetActive(false);
    }
    public void ClosePlotMenu()
    {
        plotMenu.transform.GetChild(0).gameObject.SetActive(true);
        plotMenu.transform.GetChild(1).gameObject.SetActive(false);
        plotMenu.transform.GetChild(2).gameObject.SetActive(false);
        plotMenu.SetActive(false);
    }
    
    public void OpenBuildingMenu(Transform building)
    {
        CloseBuildingMenu();
        buildingMenu.SetActive(true);
        currentBuilding = building;
    }
    public void CloseBuildingMenu()
    {
        buildingMenu.SetActive(false);
    }
}
