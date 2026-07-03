using UnityEngine;

public class DifficultDropdownSetting : DropdownSettings
{
    protected override void Awake()
    {
        dropdown.SetItems(new string[] { "Casual", "Normal" }, 1);
        base.Awake();
    }

    protected override void ChangeValue(int index)
    {

    }
}
