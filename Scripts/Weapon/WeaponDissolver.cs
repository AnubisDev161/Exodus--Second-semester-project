using UnityEngine;

public class WeaponDissolver : MonoBehaviour
{
    [SerializeField] private float dissolveDuration;
    [SerializeField] private MeshRenderer[] meshRenderers;

    private bool isDissolving;
    private int shaderProperty;
    private float dissolveAnimTime;


    private void OnEnable()
    {
        TutorialHandler.enableWeaponDissolve += OnEnableWeaponDissolve;
    }

    private void OnDisable()
    {
        TutorialHandler.enableWeaponDissolve -= OnEnableWeaponDissolve;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public void StartDissolveAnim()
    {
        isDissolving = true;

    }

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAnimTime += Time.deltaTime / dissolveDuration;

            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.material.SetFloat(shaderProperty, dissolveAnimTime);
            }
        }
    }

    public void OnEnableWeaponDissolve()
    {
        shaderProperty = Shader.PropertyToID("_AnimationTime");

        foreach (var meshRenderer in meshRenderers)
        {
            meshRenderer.material.SetFloat(shaderProperty, 0);
        }
    }
}
