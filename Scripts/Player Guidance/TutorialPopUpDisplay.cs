using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPopUpDisplay : MonoBehaviour
{
    [SerializeField] private TutorialPopUpPanel panel;

    private void OnEnable()
    {
        TutorialPopUpTrigger.showTutorialPopUp += OnShowTutorialPopUp;

    }

    private void OnDisable()
    {
        TutorialPopUpTrigger.showTutorialPopUp -= OnShowTutorialPopUp;
    }

    private void OnShowTutorialPopUp(bool show, int popUpIndex)
    {
        panel.SlideIn(show, popUpIndex);
    }

    public void OnShowMissionAccomplishedPopUp()
    {
        panel.ShowMissionAccomplishedPopUp();
    }

}
