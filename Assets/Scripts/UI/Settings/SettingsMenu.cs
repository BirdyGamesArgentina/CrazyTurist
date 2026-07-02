using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : PageSelector<SettingPanel>
{
    [SerializeField] Assets.SimpleLocalization.LocalizedTextPro mainText = null;

    [SerializeField] Assets.SimpleLocalization.LocalizedTextPro prevText = null;
    [SerializeField] Assets.SimpleLocalization.LocalizedTextPro nextText = null;
    [SerializeField] Animator prevAnim = null;
    [SerializeField] Animator nextAnim = null;

    protected override void OnOpen()
    {
    }

    protected override void OnClose()
    {
        myPanels[currentIndex].panel.Close();
    }

    protected override void OnSetNewPanel(int lastIndex, int _currentIndex)
    {
        if (lastIndex > -1)
        {
            myPanels[lastIndex].panel.Close();

            if ((lastIndex < _currentIndex && !(lastIndex == 0 && _currentIndex == myPanels.Length -1)) || (lastIndex > _currentIndex && (lastIndex == myPanels.Length - 1 && _currentIndex == 0)))
            {
                nextAnim.Play("StartRight");
            }
            else
            {
                prevAnim.Play("StartLeft");
            }
        }

        myPanels[_currentIndex].panel.Open();


        mainText.SetNewKey(myPanels[_currentIndex].panelID);
        prevText.SetNewKey(myPanels[PrevPage()].panelID);
        nextText.SetNewKey(myPanels[NextPage()].panelID);
    }
}

[System.Serializable]
public struct SettingPanel
{
    public string panelID;
    public UIPanelTransition panel;
}
