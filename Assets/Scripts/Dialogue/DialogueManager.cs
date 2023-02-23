using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject intHUD, libraryUI;
    private bool uiToggle = false;

    private Transform cleopatra, caesar, merchant;
    private GameObject summary, choices;

    private void Start()
    {
        cleopatra = transform.GetChild(0);
        caesar = transform.GetChild(1);
        merchant = transform.GetChild(2);
        
        summary = transform.GetChild(3).gameObject;
        choices = transform.GetChild(4).gameObject;
    }

    public void UpgradeLibrary()
    {
        NarrativeUI();
        
    }
    
    private void NarrativeUI()
    {
        uiToggle = !uiToggle;
        intHUD.SetActive(uiToggle);
        intHUD.GetComponent<RawImage>().raycastTarget = uiToggle;
        libraryUI.SetActive(false);
        libraryUI.GetComponent<Image>().raycastTarget = uiToggle;
    }
}
