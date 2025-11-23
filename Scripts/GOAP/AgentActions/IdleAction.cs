using JetBrains.Annotations;
using UnityEngine;

public class IdleAction : IActionStrategy
{
    public bool canPerform => true;

    public bool complete { get; private set; }

    public IdleAction() 
    {
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
