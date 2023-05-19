using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropdownSearchBar : MonoBehaviour
{
	[field: SerializeField] public TMP_Dropdown Dropdown { get; private set; }
	[SerializeField] private Transform content;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void FilterOptions(string filter)
	{
		List<TMP_Dropdown.OptionData> options = Dropdown.options;
		for (int i = 0; i < options.Count; i++)
		{
			// Show if option contains the filtered word (case insensitive).
			TMP_Dropdown.OptionData option = options[i];
			bool show = option.text.ToLowerInvariant().Contains(filter.ToLowerInvariant());
			content.GetChild(i+1).gameObject.SetActive(show);
		}
	}
}
