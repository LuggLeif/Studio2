using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField] private Global Global;
    [SerializeField] private TextMeshProUGUI HoverText;
    [SerializeField] private InterfaceInteraction Buildings;
    [SerializeField] private LayerMask UseLayers;
    
    private float MaxUseDistance = 30f;

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
            }
            else if (hit.collider.CompareTag("Building"))  // Empty plot
            {  
                HoverText.SetText("Here stands the " + hit.transform.name + ".");
                HoverText.gameObject.SetActive(true);
            }
        }
        else
            HoverText.gameObject.SetActive(false);
    }
}