using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class MenueButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite hoverImage;
    [SerializeField] private Sprite pressedImage;
    [SerializeField] private Image currentImage;

    public void OnPointerClick(PointerEventData eventData)
    {
        currentImage.sprite = pressedImage;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        currentImage.sprite = hoverImage;
        currentImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        currentImage.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
    }
}
