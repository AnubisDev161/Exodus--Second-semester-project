using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class EnemyBody : MonoBehaviour
{
    [SerializeField] private UnityEvent deathAnimationFinished;
    [SerializeField] private UnityEvent triggerAttackMethod;
    [SerializeField] private UnityEvent attackAnimationFinished;
    [SerializeField] private UnityEvent screamAnimationFinished;
    [SerializeField] private UnityEvent dropPickUp;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
   // [SerializeField] private GameObject teeth;
    [SerializeField] private float dissolveDuration;

   // [SerializeField] private List<MeshRenderer> teethMeshRenderer;


    private bool isDissolving;
    private int shaderProperty;
    private float dissolveAnimTime;


    void Start()
    {
        shaderProperty = Shader.PropertyToID("_AnimationTime");
    }

    public void DeathAnimatioFinished()
    {
        deathAnimationFinished?.Invoke();
    }

    public void OnAttackAnimationTriggerAttackMethod()
    {
        triggerAttackMethod?.Invoke();
    }

    public void AttackAnimationFinished()
    {
        attackAnimationFinished?.Invoke();
    }

    public void ScreamAnimationFinished()
    {
        screamAnimationFinished?.Invoke();
    }

    public void OnDropPickUp()
    {
        dropPickUp?.Invoke();
    }

    public void EnableBody(bool enable)
    {
        skinnedMeshRenderer.enabled = enable;
        //teeth.SetActive(enable);
    }

    public void StartDissolveAnim()
    {
        isDissolving = true;
    }

    private void Update()
    {
        if (isDissolving)
        {
            dissolveAnimTime += Time.deltaTime / dissolveDuration;

            skinnedMeshRenderer.material.SetFloat(shaderProperty, dissolveAnimTime);

            //foreach (var renderer in teethMeshRenderer)
            //{
            //    renderer.material.SetFloat(shaderProperty, dissolveAnimTime);
            //}
        }
    }
}
