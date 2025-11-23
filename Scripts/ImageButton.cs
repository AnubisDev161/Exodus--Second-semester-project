using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class ImageButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,  IPointerExitHandler
{
    private Image image;
    private void Start()
    {
        image = GetComponent<Image>();
    }
    
            
    public void OnPointerClick(PointerEventData eventData)
    {
        image.color = Color.black;        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.green;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }
    
    
}
