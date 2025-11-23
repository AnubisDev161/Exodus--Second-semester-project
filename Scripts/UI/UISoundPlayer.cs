using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundPlayer : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,  IPointerExitHandler
{
    [SerializeField] private SoundType pressed = SoundType.UI_CLICK;
    [SerializeField] private SoundType hover = SoundType.UI_HOVER;
    [SerializeField] private float pressedVolume = 0.5f;
    [SerializeField] private float hoverVolume = 0.5f;

    private float hoverSoundCooldown = 0.1f;
    private float lastHoverTime = -1f;
    private bool hasHovered = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.PlaySound(pressed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!hasHovered && Time.unscaledTime - lastHoverTime > hoverSoundCooldown)
        {
            SoundManager.PlaySound(hover);
            lastHoverTime = Time.unscaledTime;
            hasHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hasHovered = false;
    }
    
    
}
