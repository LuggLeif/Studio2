using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Global global;
    [SerializeField] private GameObject intHUD, libraryUI;
    private bool uiToggle = false;

    private Transform cleopatra, caesar, merchant;
    private GameObject summary, choices;

    private int dialogueCounter = 0;

    private void Start()
    {
        InitializeScript();
    }
    
    private void InitializeScript()
    {
        cleopatra = transform.GetChild(0);
        caesar = transform.GetChild(1);
        merchant = transform.GetChild(2);
        
        summary = transform.GetChild(3).gameObject;
        choices = transform.GetChild(4).gameObject;
    }

    private void Update()
    {
        if (global.busy)
            return;

        if (Input.GetKeyDown(KeyCode.Mouse0) && (dialogueCounter < 12))
        {
            switch (dialogueCounter)
            {
                case 0:
                    caesar.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    caesar.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 1:
                    caesar.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    caesar.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 2:
                    caesar.GetChild(0).GetChild(3).gameObject.SetActive(false);
                    caesar.GetChild(0).gameObject.SetActive(false);
                    
                    cleopatra.GetChild(0).gameObject.SetActive(true);
                    cleopatra.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    cleopatra.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 3:
                    cleopatra.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    cleopatra.GetChild(0).GetChild(2).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 4:
                    cleopatra.GetChild(0).GetChild(2).gameObject.SetActive(false);
                    cleopatra.GetChild(0).GetChild(3).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 5:
                    cleopatra.GetChild(0).GetChild(3).gameObject.SetActive(false);
                    cleopatra.GetChild(0).gameObject.SetActive(false);
                    
                    caesar.GetChild(0).gameObject.SetActive(true);
                    caesar.GetChild(0).GetChild(4).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 6:
                    caesar.GetChild(0).GetChild(4).gameObject.SetActive(false);
                    caesar.GetChild(0).gameObject.SetActive(false);
                    
                    cleopatra.GetChild(0).gameObject.SetActive(true);
                    cleopatra.GetChild(0).GetChild(4).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 7:
                    cleopatra.GetChild(0).GetChild(4).gameObject.SetActive(false);
                    cleopatra.GetChild(0).GetChild(5).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 8:
                    cleopatra.GetChild(0).GetChild(5).gameObject.SetActive(false);
                    cleopatra.GetChild(0).GetChild(6).gameObject.SetActive(true);
                    dialogueCounter++;;
                    break;
                case 9:
                    cleopatra.GetChild(0).GetChild(6).gameObject.SetActive(false);
                    cleopatra.GetChild(0).gameObject.SetActive(false);
                    
                    caesar.GetChild(0).gameObject.SetActive(true);
                    caesar.GetChild(0).GetChild(5).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 10:
                    caesar.GetChild(0).GetChild(5).gameObject.SetActive(false);
                    caesar.GetChild(0).gameObject.SetActive(false);
                    
                    cleopatra.GetChild(0).gameObject.SetActive(true);
                    cleopatra.GetChild(0).GetChild(7).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
                case 11:
                    caesar.gameObject.SetActive(false);
                    cleopatra.GetChild(0).gameObject.SetActive(false);
                    cleopatra.GetChild(0).GetChild(7).gameObject.SetActive(false);
                    
                    merchant.gameObject.SetActive(true);
                    merchant.GetChild(0).gameObject.SetActive(true);
                    merchant.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    merchant.GetChild(0).GetChild(1).gameObject.SetActive(true);
                    
                    summary.SetActive(true);
                    summary.transform.GetChild(0).gameObject.SetActive(true);
                    
                    choices.SetActive(true);
                    choices.transform.GetChild(0).gameObject.SetActive(true);
                    choices.transform.GetChild(1).gameObject.SetActive(true);
                    dialogueCounter++;
                    break;
            }
        }
    }

    public void UpgradeLibrary()
    {
        InitializeScript();
        NarrativeUI();
        global.disabled = true;
        
        cleopatra.gameObject.SetActive(true);
        
        caesar.gameObject.SetActive(true);
        caesar.GetChild(0).gameObject.SetActive(true);
        caesar.GetChild(0).GetChild(0).gameObject.SetActive(true);
        caesar.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }
    
    private void NarrativeUI()
    {
        uiToggle = !uiToggle;
        intHUD.SetActive(uiToggle);
        intHUD.GetComponent<RawImage>().raycastTarget = uiToggle;
        libraryUI.SetActive(false);
        libraryUI.GetComponent<Image>().raycastTarget = uiToggle;
    }

    private void ExitDialogue()
    {
        dialogueCounter = 0;
        
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach (Transform grandChild in child)
            {
                grandChild.gameObject.SetActive(false);

                foreach (Transform greatGrandChild in grandChild)
                {
                    greatGrandChild.gameObject.SetActive(false);
                }
            }
        }
    }
}
