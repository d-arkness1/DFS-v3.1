using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class CategoryPanelOption : MonoBehaviour
{
	[field: SerializeField] public Button Button { get; private set; }
	[field: SerializeField] public Image Image { get; private set; }
	[field: SerializeField] public TextMeshProUGUI Text { get; private set; }

	public UnityAction<ModuleFieldGroup> onClickAction;

	public void SetCategory(ModuleFieldGroup category)
	{
		// Update visuals to represent the assigned category
		Image.sprite = category.icon;
		Text.text = category.name;
		
		// Set onClick listner
		Button.onClick.RemoveAllListeners();
		Button.onClick.AddListener(() =>
		{
			onClickAction?.Invoke(category);
		});
	}
}
