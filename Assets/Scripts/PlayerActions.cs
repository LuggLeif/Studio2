using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HoverText;
    [SerializeField] private float MaxUseDistance = 30f;
    [SerializeField] private LayerMask UseLayers;
    [SerializeField] private BuildingSelection Buildings;
    [SerializeField] private Global Global;

    private void Update()
    {
        if (Global.Busy)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool isOverUI = UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();

        if ((Physics.Raycast(ray, out hit, MaxUseDistance, UseLayers)) && !isOverUI)
        {
            if (hit.collider.CompareTag("Build"))  // Empty plot
            {  
                HoverText.SetText("This plot is empty.");
                HoverText.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                    Buildings.OpenPlotMenu(hit.transform);
            }
            else if (hit.collider.CompareTag("Building"))  // Empty plot
            {  
                HoverText.SetText("Here stands the " + hit.transform.name + ".");
                HoverText.gameObject.SetActive(true);
                if (Input.GetMouseButtonDown(0))
                    Buildings.OpenBuildingMenu(hit.transform);
            }
        }
        else
            HoverText.gameObject.SetActive(false);
    }
}