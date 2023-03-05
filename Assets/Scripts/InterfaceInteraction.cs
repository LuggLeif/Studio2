using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InterfaceInteraction : MonoBehaviour
{
    [SerializeField] private CreateBuildings makeEm;
    private Transform openConstMenu, exitButton, currentMenu;
    private int layerSize;
    private string folMenu, folSize;
    private GameObject currentBuilding;

    private void Start()
    {
        openConstMenu = transform.GetChild(1).GetChild(1);
        exitButton = transform.GetChild(1).GetChild(0);
    }

    public void ConstMenus(int menu)
    {
        makeEm.CancelBuild(currentBuilding);
        CloseUI();
        currentMenu = openConstMenu.GetChild(menu);
        
        openConstMenu.GetComponent<Image>().raycastTarget = false;  //Open button
        openConstMenu.GetComponent<Button>().enabled = false;
        
        openConstMenu.GetChild(3).gameObject.SetActive(true);   //Close button
        openConstMenu.GetChild(3).GetComponent<Image>().raycastTarget = true;

        openConstMenu.GetChild(menu).gameObject.SetActive(true);   //The menu
        openConstMenu.GetChild(menu).GetComponent<Image>().raycastTarget = true;
        openConstMenu.GetChild(4).gameObject.SetActive(true);   //Arrow

        switch (menu)
        {
            case 0:
                folMenu = "Buildings/";
                break;
            case 1:
                folMenu += "Housing";
                break;
            case 2:
                folMenu += "Vendors";
                break;
        }

        foreach (Transform child in openConstMenu.GetChild(menu))
            child.GetComponent<Image>().raycastTarget = true;   //Children

        exitButton.gameObject.SetActive(true);  //Exit button
        exitButton.GetComponent<Image>().raycastTarget = true;
    }

    public void ChooseSize(int category)
    {
        CloseSelection();
        foreach (Transform child in currentMenu.GetChild(category))
        {
            child.gameObject.SetActive(true);
            child.GetComponent<Image>().raycastTarget = true;
        }

        layerSize = category + 1;

        switch (category)
        {
            case 0:
                folSize = "/Small";
                break;
            case 1:
                folSize = "/Large";
                break;
        }
    }
    public void SpawnBuilding(string selection)
    {
        CloseUI();
        
        currentBuilding = Instantiate(Resources.Load<GameObject>(folMenu + folSize + "/" + selection), null);
        currentBuilding.transform.name = selection;
        
        makeEm.BuildingSpecs(currentBuilding.transform, layerSize);
    }

    private void CloseSelection()
    {
        if (!currentMenu)
            return;
        
        foreach (Transform child in currentMenu)
        {
            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);
                grandChild.GetComponent<Image>().raycastTarget = false;
            }
        }
    }
    public void CloseUI()
    {
        CloseSelection();
        exitButton.gameObject.SetActive(false);  //Exit button
        exitButton.GetComponent<Image>().raycastTarget = false;

        foreach (RectTransform menu in openConstMenu)
        {
            menu.gameObject.SetActive(false);  //Menus and close button
            menu.GetComponent<Image>().raycastTarget = false;

            foreach (RectTransform button in menu)
            {
                button.GetComponent<Image>().raycastTarget = false;  //Children
            }
        }
     
        openConstMenu.GetChild(4).gameObject.SetActive(false);  //Open button
        openConstMenu.GetComponent<Button>().enabled = true;
        openConstMenu.GetComponent<Image>().raycastTarget = true;
    }
}