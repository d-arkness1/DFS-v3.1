using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ModuleCreator))]
public class ModuleEditor : MonoBehaviour
{
	public ModuleCreator ModuleCreator { get; private set; }
	public Module Module { get; set; }

	private void Awake()
	{
		ModuleCreator = GetComponent<ModuleCreator>();
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void ShowPanel()
    {
		ModuleCreator.identifierDropdown.value = Module.identifierSelected;

		// Set initial field values
		for (int i = 0; i < Module.fieldValues.Length; i++)
		{
			int value = Module.fieldValues[i].value;

			ModuleFieldValueObject field = ModuleCreator.inputFields[i];
			field.SetValue(value);
		}

		ModuleCreator.SetEditMode(true);
		ModuleCreator.ShowPanel();
    }

	public void UpdateModule()
	{
		// Update assigned values to module fields
		List<ModuleFieldValue> fieldValues = new List<ModuleFieldValue>();
		ModuleFieldGroup moduleFieldGroup = ModuleCreator.GetModuleFieldGroup();
		for (int i = 0; i < moduleFieldGroup.fields.Length; i++)
		{
			fieldValues.Add(
				new ModuleFieldValue()
				{
					field = moduleFieldGroup.fields[i],
					value = ModuleCreator.inputFields[i].GetValue()
				}
			);
		}

		// Create the module
		Module.SetFieldValues(fieldValues.ToArray());
		if (moduleFieldGroup.dataSprites.Length > 0)
			Module.dataSprite = moduleFieldGroup.dataSprites[ModuleCreator.dataDropdown.value];

		// Transfer data
		if (ModuleCreator.dataOriginDropdown.value > 0)
			Module.TransferData(Category.GetAllModules()[ModuleCreator.dataOriginDropdown.value - 1]);
	}
}
