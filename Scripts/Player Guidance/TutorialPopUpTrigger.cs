using System;
using UnityEngine;

public class TutorialPopUpTrigger : MonoBehaviour
{
    [SerializeField] private int popUpIndex;
    public static Action<bool, int> showTutorialPopUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var player))
        {
            showTutorialPopUp?.Invoke(true, popUpIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent<Player>(out var player))
        {
            showTutorialPopUp?.Invoke(false, popUpIndex);
        }
    }
}
