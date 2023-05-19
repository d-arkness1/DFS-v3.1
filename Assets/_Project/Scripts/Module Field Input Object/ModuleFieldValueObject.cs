using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class ModuleFieldValueObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void SetFieldName(string name)
    {
        label.text = name;
    }

    public abstract int GetValue();
    public abstract void SetValue(int value);
}
