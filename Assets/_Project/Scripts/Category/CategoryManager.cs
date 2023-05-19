using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CategoryManager : MonoBehaviour
{
	[SerializeField] private Transform categoryParent;
	[SerializeField] private Category categoryPrefab;
	[SerializeField] private Transform createCategoryButton;

	private List<Category > categories = new List<Category>();

	public UnityEvent<Category> onCategoryButtonClick;

	public void CreateCategory(ModuleFieldGroup moduleFieldGroup)
	{
		Category category = Instantiate(categoryPrefab, categoryParent);
		category.SetTitle(moduleFieldGroup.name);
		category.moduleCreator.SetModuleFieldGroup(moduleFieldGroup);

		// Connect last created category to this newly created one
		if (categories.Count > 0)
			categories.Last().connectedTransform = category.transform;
		category.connectedTransform = createCategoryButton;

		// Set icon
		category.SetIcon(moduleFieldGroup.icon);

		// Add tooltip
		string tooltipText = $"<b>{moduleFieldGroup.name}</b>";
		if (moduleFieldGroup.description.Length > 0)
			tooltipText += $"<br>{moduleFieldGroup.description}";
		category.SetToolTip(tooltipText);

		// Create navbar entry
		GameManager.Instance.navBar.CreateNavButton(
			category.GetComponent<RectTransform>(),
			moduleFieldGroup.icon
			);

		category.onClickAction = onCategoryButtonClick.Invoke;

		// Add to list
		categories.Add(category);
	}
	
	public void RemoveCategory(Category category)
	{
		// Update connected transforms
		int index = categories.IndexOf(category);
		if (index > 0 && index < categories.Count - 1)
			categories[index - 1].connectedTransform = categories[index + 1].transform;

		categories.Remove(category);
		Destroy(category.gameObject);
	}

	public void ShowModuleCreator(Category category)
	{
		category.moduleCreator.SetEditMode(false);
		category.moduleCreator.ShowPanel();
	}

	public void HideCreatorPanels()
	{
		foreach (Category category in categories)
		{
			category.moduleCreator.HidePanel();
		}
	}
}
