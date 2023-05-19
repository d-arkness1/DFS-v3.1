using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModuleCreator : MonoBehaviour
{
	public ModuleEditor ModuleEditor { get; private set; }

	[SerializeField] private Category category;
	[SerializeField] protected Module modulePrefab;
	[SerializeField] private IntModuleFieldValueObject intFieldPrefab;
	[SerializeField] private ArrayModuleFieldValueObject arrayFieldPrefab;

	[Space]
	[SerializeField] private Transform fieldParent;
	[SerializeField] private GameObject creatorPanel;
	[SerializeField] protected Transform modulesParent;
	public Button createButton;

	[Header("Input Fields")]
	public TMP_Dropdown identifierDropdown;
	public TMP_Dropdown dataDropdown;
	public TMP_Dropdown dataOriginDropdown;

	public ModuleFieldGroup moduleFieldGroup;
	public List<ModuleFieldValueObject> inputFields = new List<ModuleFieldValueObject>();

    private void Awake()
    {
        ModuleEditor = GetComponent<ModuleEditor>();
    }

    private void Start()
	{
		UpdateDataDropdown();
	}

	public void SetModuleFieldGroup(ModuleFieldGroup moduleFieldGroup)
	{
		foreach (ModuleField field in moduleFieldGroup.fields)
		{
			ModuleFieldValueObject moduleFieldObject;
			if (field is ArrayModuleField)
			{
				moduleFieldObject = Instantiate(arrayFieldPrefab, fieldParent);
				((ArrayModuleFieldValueObject)moduleFieldObject).SetDropdownValues(((ArrayModuleField)field).options);
			}
			else
			{
				moduleFieldObject = Instantiate(intFieldPrefab, fieldParent);
			}
			moduleFieldObject.SetFieldName(field.name);
			inputFields.Add(moduleFieldObject);
		}
		this.moduleFieldGroup = moduleFieldGroup;

		UpdateIndentifierDropdown();
	}

	public ModuleFieldGroup GetModuleFieldGroup()
    {
		return moduleFieldGroup;
    }

	public void CreateModule()
	{
		// Initialize identifier
		ModuleFieldOption identifierSelected = moduleFieldGroup.identifiers.options[identifierDropdown.value];
		string moduleName = identifierSelected.name;
		Sprite moduleIcon = identifierSelected.itemImage;

		// Initialize module fields and assigned values
		List<ModuleFieldValue> fieldValues = new List<ModuleFieldValue>();
		for (int i=0; i < moduleFieldGroup.fields.Length; i++)
		{
			fieldValues.Add(
				new ModuleFieldValue()
				{
					field = moduleFieldGroup.fields[i],
					value = inputFields[i].GetValue()
				}
			);
		}

		// Create the module
		Module module = Instantiate(modulePrefab, modulesParent);
		module.identifierSelected = identifierDropdown.value;
		module.SetIdentifier(moduleName, moduleIcon);
		module.SetFieldValues(fieldValues.ToArray());
		module.InitializeModule();
		if (moduleFieldGroup.dataSprites.Length > 0)
			module.dataSprite = moduleFieldGroup.dataSprites[dataDropdown.value];

		// Set tooltip
		module.SetToolTip(identifierSelected.description);

		// Transfer data
		if (dataOriginDropdown.value > 0)
			module.TransferData(Category.GetAllModules()[dataOriginDropdown.value - 1]);

		category.AddModule(module);

		UpdateOriginDropdown();
	}

	public void ShowPanel()
	{
		creatorPanel.SetActive(true);
		UpdateOriginDropdown();
	}

	public void HidePanel()
	{
		creatorPanel.SetActive(false);
	}

	public void UpdateIndentifierDropdown()
	{
		identifierDropdown.ClearOptions();
		foreach (ModuleFieldOption option in moduleFieldGroup.identifiers.options)
		{
			identifierDropdown.options.Add(new TMP_Dropdown.OptionData(option.name, option.itemImage));
		}
		identifierDropdown.value = 0;
		identifierDropdown.RefreshShownValue();
	}

	public void UpdateDataDropdown()
	{
		dataDropdown.ClearOptions();
		foreach (Sprite sprite in moduleFieldGroup.dataSprites)
		{
			dataDropdown.options.Add(new TMP_Dropdown.OptionData(sprite));
		}
		dataDropdown.RefreshShownValue();
	}

	public void UpdateOriginDropdown()
	{
		dataOriginDropdown.ClearOptions();
		dataOriginDropdown.options.Add(new TMP_Dropdown.OptionData("None"));
		foreach (Module module in Category.GetAllModules())
		{
			dataOriginDropdown.options.Add(new TMP_Dropdown.OptionData(module.name, module.icon));
		}
		dataOriginDropdown.RefreshShownValue();
	}

	public void SetEditMode(bool editMode)
	{
		identifierDropdown.interactable = !editMode;
		dataDropdown.gameObject.SetActive(!editMode);
		dataOriginDropdown.gameObject.SetActive(!editMode);

		createButton.onClick.RemoveAllListeners();

		if (editMode)
		{
			createButton.GetComponentInChildren<TextMeshProUGUI>().text = "Edit Module";
			createButton.onClick.AddListener(ModuleEditor.UpdateModule);
		}
		else
		{
			createButton.GetComponentInChildren<TextMeshProUGUI>().text = "Create Module";
			createButton.onClick.AddListener(CreateModule);
		}
	}
}
