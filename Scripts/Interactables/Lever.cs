using UnityEngine;

[RequireComponent (typeof(MeshRenderer))]
public class Lever : Interactable
{
    [SerializeField] private Canvas display;

    private MeshRenderer meshRenderer;
    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
       triggered.AddListener(OnTriggered);
    }

    public void InFocus(bool inFocus)
    {
        if (wasTriggered) return;

        if ((inFocus))
        {
            meshRenderer.material.color = Color.blue;
            display.enabled = true;
        }
        else
        {
            meshRenderer.material.color = Color.red;
            display.enabled = false;
        }
    }

    public void OnTriggered()
    {
        meshRenderer.material.color = Color.red;
        display.enabled = false;
    }
}
