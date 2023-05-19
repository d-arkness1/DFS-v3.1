using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Category : MonoBehaviour
{
	public ModuleCreator moduleCreator;
	public Transform connectedTransform;
	[SerializeField] private LineRenderer connectionLine;
	[SerializeField] private TextMeshProUGUI titleTMP;
	[SerializeField] private Image iconImage;
	[SerializeField] private Tooltip tooltip;
	[field: SerializeField] public Button Button { get; private set; }

	private List<Module> modules = new List<Module>();

	private static Module[] allModules = new Module[0];
	private static List<Category> allCategories = new List<Category>();

	public UnityAction<Category> onClickAction;
	public UnityEvent<Module> onModuleClick;

	void Awake()
	{
		allCategories.Add(this);
		Button.onClick.AddListener(() =>
		{
			onClickAction(this);
		});
	}

	// Update is called once per frame
	void Update()
	{
		if (connectedTransform)
			connectionLine.SetPosition(1, connectedTransform.position - transform.position);
		else if (modules.Count > 0)
		{
			Vector3 linePos = modules[modules.Count - 1].transform.position - transform.position;
			linePos.x = 0;
			linePos.y += 0.4f;
			connectionLine.SetPosition(1, linePos);
		}
	}

	public void SetTitle(string title)
	{
		titleTMP.text = title;
	}

	public void SetIcon(Sprite icon)
	{
		iconImage.sprite = icon;
	}

	public void AddModule(Module module)
	{
		module.onClickAction = (module) => onModuleClick.Invoke(module);
		modules.Add(module);
		UpdateAllModules();
	}

	public void RemoveModule(Module module)
	{
		modules.Remove(module);
		Destroy(module.gameObject);
		UpdateModules();
		UpdateAllModules();
	}

	public void UpdateModules()
    {
		foreach (Module module in modules)
			module.InitializeModule();
    }

	public Module[] GetModules()
	{
		return modules.ToArray();
	}

	public static Module[] GetAllModules()
	{
		return allModules;
	}

	public static void UpdateAllModules()
	{
		List<Module> moduleObjects = new List<Module>();
		foreach (Category category in allCategories)
		{
			moduleObjects.AddRange(category.GetModules());
		}
		allModules = moduleObjects.ToArray();
	}

	public void SetToolTip(string text)
	{
		tooltip.Text = text;
	}
}
