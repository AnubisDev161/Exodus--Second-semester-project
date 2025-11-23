using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Crosshair : MonoBehaviour, IWorldSwitchListener
{

    [SerializeField] private Sprite hellImage;
    [SerializeField] private Sprite heavenImage;
    [SerializeField] private Animation reloadAnimation;

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.sprite = heavenImage;
    }

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
        GunScript.reloading += StartReloadAnimation;
        WeaponPositioner.isAiming += IsAiming;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
        GunScript.reloading -= StartReloadAnimation;
        WeaponPositioner.isAiming -= IsAiming;
    }

    public void StartReloadAnimation()
    {
        reloadAnimation.Play();
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        if (isInHellWorld)
        {
            image.sprite = hellImage;
        }
        else
        {
            image.sprite = heavenImage;
        }
    }

    public void IsAiming(bool isAiming)
    {
        if (isAiming)
        {
            StartCoroutine(HideCursorWithDelay());
        }
        else
        {
            image.enabled = true;
        }
    }

    private IEnumerator HideCursorWithDelay()
    {
        yield return new WaitForSeconds(0.2f);
        image.enabled = false;
    }
}
