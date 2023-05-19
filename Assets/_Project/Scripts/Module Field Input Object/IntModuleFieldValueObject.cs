using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IntModuleFieldValueObject : ModuleFieldValueObject
{
    [SerializeField] private TMP_InputField inputField;

    public override int GetValue()
    {
        int value;
        int.TryParse(inputField.text, out value);
        return value;
    }

    public override void SetValue(int value)
    {
        inputField.text = value.ToString();
    }
}

