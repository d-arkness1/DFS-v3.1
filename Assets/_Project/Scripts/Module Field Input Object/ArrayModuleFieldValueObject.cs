using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrayModuleFieldValueObject : ModuleFieldValueObject
{
    [SerializeField] private TMP_Dropdown dropdown;

    public override int GetValue()
    {
        return dropdown.value;
    }

    public override void SetValue(int value)
    {
        dropdown.value = value;
    }

    public void SetDropdownValues(ModuleFieldOption[] values)
    {
        List<TMP_Dropdown.OptionData> optionsList = new List<TMP_Dropdown.OptionData>();
        foreach (ModuleFieldOption value in values)
        {
            optionsList.Add(new TMP_Dropdown.OptionData(value.name, value.itemImage));
        }
        dropdown.options = optionsList;
    }
}
