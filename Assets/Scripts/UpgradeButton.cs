using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    UpgradeUi upgrade_ui;

    void Start()
    {
        upgrade_ui = transform.parent.parent.GetComponent<UpgradeUi>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgrade_ui.HandleTooltip(transform.GetSiblingIndex());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgrade_ui.HandleTooltip(-1);
    }
    
}
