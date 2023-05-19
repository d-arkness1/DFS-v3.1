using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CategoryOptions : MonoBehaviour
{
	public Category Target { get; private set; }
	public Vector3 targetOffset = new Vector3(0, 20, 0);

	public UnityEvent<Category> onRemoveCategoryClick;
	public UnityEvent<Category> onAddModuleClick;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void SetTarget(Category category)
	{
		// Hide if same button was pressed
		if (category == Target)
		{
			ClearTarget();
			return;
		}
		
		Target = category;

		// Set new position
		transform.SetParent(category.transform);
		transform.localPosition = targetOffset;

		gameObject.SetActive(true);
	}

	public void ClearTarget()
	{
		Target = null;
		transform.SetParent(null);
		gameObject.SetActive(false);
	}

	public void RemoveCategory()
	{
		transform.SetParent(null);
		onRemoveCategoryClick.Invoke(Target);
		ClearTarget();
	}

	public void AddModule()
	{
		onAddModuleClick.Invoke(Target);
		ClearTarget();
	}
}
