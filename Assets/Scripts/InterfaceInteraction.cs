using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceInteraction : MonoBehaviour
{
    [SerializeField] private CreateBuildings makeEm;
    [SerializeField] private Transform buildings;
    private Transform openConstMenu, exitButton;
    private GameObject currentBuilding;

    private void Start()
    {
        openConstMenu = transform.GetChild(0).GetChild(1);
        exitButton = transform.GetChild(0).GetChild(0);
    }

    /*private Transform currentBuilding;
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
    }*/

    public void ConstMenus(int menu)
    {
        makeEm.CancelBuild(currentBuilding);
        CloseUI();
        
        openConstMenu.GetComponent<Image>().raycastTarget = false;  //Open button
        openConstMenu.GetComponent<Button>().enabled = false;
        openConstMenu.GetChild(4).gameObject.SetActive(true);
        
        openConstMenu.GetChild(3).gameObject.SetActive(true);   //Close button
        openConstMenu.GetChild(3).GetComponent<Image>().raycastTarget = true;

        openConstMenu.GetChild(menu).gameObject.SetActive(true);   //The menu
        openConstMenu.GetChild(menu).GetComponent<Image>().raycastTarget = true;

        foreach (Transform child in openConstMenu.GetChild(menu))
        {
            child.GetComponent<Image>().raycastTarget = true;   //Children
        }

        exitButton.gameObject.SetActive(true);  //Exit button
        exitButton.GetComponent<Image>().raycastTarget = true;
    }

    public void CloseUI()
    {
        exitButton.gameObject.SetActive(false);  //Exit button
        exitButton.GetComponent<Image>().raycastTarget = false;
        
        foreach (RectTransform menu in openConstMenu)
        {
            menu.gameObject.SetActive(false);  //Menus and close button
            menu.GetComponent<Image>().raycastTarget = false;
            
            foreach (RectTransform button in menu)
                button.GetComponent<Image>().raycastTarget = false;  //Children
        }
        
        openConstMenu.GetChild(4).gameObject.SetActive(false);  //Open button
        openConstMenu.GetComponent<Button>().enabled = true;
        openConstMenu.GetComponent<Image>().raycastTarget = true;
    }

    public void SpawnBuilding(int selection)
    {
        CloseUI();
        int category;
        if (selection < 3)
            category = 0;
        else
        {
            selection -= 3;
            category = 1;
        }
        currentBuilding = Instantiate(buildings.GetChild(category).GetChild(selection).gameObject, null);
        currentBuilding.transform.name = buildings.GetChild(category).GetChild(selection).transform.name;
        makeEm.BuildingSpecs(currentBuilding.transform, selection);
    }
}
