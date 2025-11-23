using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animation))]
public class TutorialPopUpPanel : MonoBehaviour
{

    [SerializeField] private Image[] popUpPanels;
    private Animation animationPlayer;

    private void Awake()
    {
        animationPlayer = GetComponent<Animation>();
    }

    public void SlideIn(bool slideIn, int popUpIndex)
    {
        EnableDesiredPopUp(popUpIndex);

        if(slideIn)
        {
            // check if animation is still running, in case wait until current animation is finished and then play the next anim
            animationPlayer.Play("Tutorial_PopUp_SlideIn");
        }
        else
        {
            animationPlayer.Play("Tutorial_PopUp_SlideOut");
        }
    }

    public void ShowMissionAccomplishedPopUp()
    {
        SlideIn(true, popUpPanels.Length -1); 
    }

    private void EnableDesiredPopUp(int popUpIndex)
    {
        foreach (var panel in popUpPanels)
        {
            panel.gameObject.SetActive(false);
        }

        popUpPanels[popUpIndex].gameObject.SetActive(true);
    }
}
