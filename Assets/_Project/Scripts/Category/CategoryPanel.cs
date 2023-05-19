using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryPanel : MonoBehaviour
{
    public ModuleFieldGroup[] categories;

    [SerializeField] private Transform categoryPanelOptionParent;
    [SerializeField] private CategoryPanelOption categoryPanelOptionPrefab;

    public UnityEvent<ModuleFieldGroup> onCategoryPanelOptionClick = new UnityEvent<ModuleFieldGroup>();

    private void Awake()
    {
        CreateOptions();
    }

    public void CreateOptions()
    {
        foreach (ModuleFieldGroup category in categories)
        {
            CategoryPanelOption button = Instantiate(categoryPanelOptionPrefab, categoryPanelOptionParent);
            button.SetCategory(category);
            button.onClickAction = onCategoryPanelOptionClick.Invoke;
        }
    }
}
