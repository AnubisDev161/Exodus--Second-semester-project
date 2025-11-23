using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PointOfInterest : MonoBehaviour
{
    [SerializeField] private UnityEvent markNextPOI;
    [SerializeField] private Canvas canvas;

    public void OnMarkThisPOI()
    {
        ShowMarker(true);
    }

    public void OnPOIReached()
    {
        ShowMarker(false);

        // if there is another poi connected to this one, the next will be marked
        // if not, the next POITrigger will call OnMarkThisPOI
        markNextPOI?.Invoke();
    }

    private void ShowMarker(bool show)
    {
        if (show)
        {
            canvas.gameObject.SetActive(true);
        }
        else
        {
            canvas.gameObject.SetActive(false);
        }
    }
}
