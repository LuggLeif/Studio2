using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceInteraction : MonoBehaviour
{
    private Transform openConstMenu;

    private void Start()
    {
        openConstMenu = transform.GetChild(1);
    }

    /*[SerializeField] private Transform buildings;

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
    }*/

    public void ConstMenus(int menu)
    {
        CloseUI();
        openConstMenu.GetComponent<Image>().raycastTarget = false;  //Open button
        
        openConstMenu.GetChild(3).gameObject.SetActive(true);   //Close button
        openConstMenu.GetChild(3).GetComponent<Image>().raycastTarget = true;

        openConstMenu.GetChild(menu).gameObject.SetActive(true);   //The menu
        openConstMenu.GetChild(menu).GetComponent<Image>().raycastTarget = true;

        for (int i = 0; i < openConstMenu.GetChild(0).childCount; i++)
        {
            openConstMenu.GetChild(menu).GetChild(i).GetComponent<Image>().raycastTarget = true;   //Children
        }
        
        transform.GetChild(0).gameObject.SetActive(true);  //Exit button
        transform.GetChild(0).GetComponent<Image>().raycastTarget = true;
    }

    public void CloseUI()
    {
        transform.GetChild(0).gameObject.SetActive(false);  //Exit button
        transform.GetChild(0).GetComponent<Image>().raycastTarget = false;
        
        for (int i = 0; i < openConstMenu.childCount; i++)
        {
            openConstMenu.GetChild(i).gameObject.SetActive(false);  //Menus and close button
            openConstMenu.GetChild(i).GetComponent<Image>().raycastTarget = false;
            
            if (openConstMenu.GetChild(i).GetChild(0))
            {
                for (int l = 0; l < openConstMenu.GetChild(i).childCount; i++)
                {
                    openConstMenu.GetChild(i).GetChild(l).GetComponent<Image>().raycastTarget = false;  //Children
                }
            }
        }
        
        openConstMenu.GetComponent<Image>().raycastTarget = true;   //Open button
    }
}
