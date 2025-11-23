using UnityEngine;

[RequireComponent (typeof(Canvas))]
public class HUDEnabler : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        TutorialHandler.enableHUD += OnEnableHUD;
    }

    private void OnDisable()
    {
        TutorialHandler.enableHUD -= OnEnableHUD;
    }
    
    public void OnEnableHUD(bool enable)
    {
        canvas.gameObject.SetActive(enable);

    }
}
