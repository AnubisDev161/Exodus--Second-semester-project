using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SceneSkipper : MonoBehaviour
{
    [SerializeField] private UnityEvent skipScene;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Image skippingBar;
    [SerializeField] private float fillDuration;
    private float currentFillTime;
    private float currentDecreaseTime;

    private InputActionMap tutorialMap;
    private InputAction skipAction;
    private bool skippedAlreadyPressed;

    private void Awake()
    {
        tutorialMap = inputActionAsset.FindActionMap("Tutorial");
        skipAction = tutorialMap.FindAction("Skip");
    }

    private void OnEnable()
    {
        tutorialMap.Enable();

    }

    private void OnDisable()
    {
        tutorialMap.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (skippedAlreadyPressed) return;

        if(skipAction.IsPressed())
        {
            currentFillTime += Time.deltaTime;
            currentFillTime = Mathf.Clamp(currentFillTime, 0f, 2f);
            skippingBar.fillAmount = (1 / fillDuration) * currentFillTime;
            
            if(skippingBar.fillAmount >= 1)
            {
                skipScene?.Invoke();
                skippedAlreadyPressed = true;
            }
        }
        else
        {
            currentFillTime -= Time.deltaTime;
            currentFillTime = Mathf.Clamp(currentFillTime, 0f, 2f);
            skippingBar.fillAmount = (1 / fillDuration) * currentFillTime;
        }

       
       
    }
}
