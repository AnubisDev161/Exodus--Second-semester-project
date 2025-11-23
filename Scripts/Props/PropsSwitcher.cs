using System;
using System.Collections.Generic;
using UnityEngine;

public class PropsSwitcher : MonoBehaviour, IWorldSwitchListener
{
    [SerializeField] List<Props> props;

    private void OnEnable()
    {
        WorldSwitcher.switchWorld += OnSwitchWorld;
    }

    private void OnDisable()
    {
        WorldSwitcher.switchWorld -= OnSwitchWorld;
    }

    public void OnSwitchWorld(bool isInHellWorld)
    {
        foreach (Props prop in props)
        {

            prop.OnSwitchWorld(isInHellWorld);
        }
    }
}
