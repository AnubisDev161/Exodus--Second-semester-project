using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class HoverSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isInHell;

    public static Action forceWorldSwitch;
    private void Start()
    {
        SwitchMode();
        SwitchMode();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MainMenu.tabOpened) return;
        SwitchMode();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MainMenu.tabOpened) return;
        SwitchMode();
    }

    private void SwitchMode()
    {
        forceWorldSwitch?.Invoke();
    }
}
