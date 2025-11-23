using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class Subject : MonoBehaviour
{
    // currentAmmo, ammoReserve, magazineCapacity, weaponName, isReloading
    public static Action<int, int, int, string, bool, bool> updateAmmo;

    // currentHealth, MaxHealth
    public static Action<float, float> updateHealth;

    // deathScreen, death
    public static Action onPlayerDeath;

    public static Action updateMission;



    // collections of all the observers
    //private List<IObserver> observers = new List<IObserver>();

    //public void AddObserver(IObserver observer)
    //{
    //    observers.Add(observer);
    //}

    //public void RemoveObserver(IObserver observer)
    //{
    //    observers.Remove(observer);
    //}

    //protected void NotifyObservers(UpdateEvents updateEvent)
    //{
    //    observers.ForEach((observer) => { observer.OnNotify(); });
    //}

}
