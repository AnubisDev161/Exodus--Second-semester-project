using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CrystalIcon : MonoBehaviour
{
    [SerializeField] private Sprite crackedImage;
    [SerializeField] private Sprite inChamberImage;

    private Sprite defaultSprite;
    public Image image { get; private set; }
    private void Awake()
    {
        image = GetComponent<Image>();
        defaultSprite = image.sprite;
    }

    public void SetImageToCracked(bool cracked)
    {
        if(cracked)
        {
            image.sprite = crackedImage;
        }
        else
        {
            image.sprite = inChamberImage;
        }
    }
}
