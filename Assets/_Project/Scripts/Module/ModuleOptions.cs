using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ModuleOptions : MonoBehaviour
{
	public Module Target { get; private set; }
	public Vector3 targetOffset = new Vector3(0, 20, 0);

	public UnityEvent<Module> onEditModuleClick;
	public UnityEvent<Module> onRemoveModuleClick;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetTarget(Module module)
	{
		// Hide if same button was pressed
		if (module == Target)
		{
			ClearTarget();
			return;
		}

		Target = module;

		// Set new position
		transform.SetParent(module.transform);
		transform.localPosition = targetOffset;

		gameObject.SetActive(true);
	}

	public void ClearTarget()
	{
		Target = null;
		transform.SetParent(null);
		gameObject.SetActive(false);
	}

	public void RemoveModule()
	{
		transform.SetParent(null);
		onRemoveModuleClick.Invoke(Target);
		ClearTarget();
	}

	public void EditModule()
	{
		onEditModuleClick.Invoke(Target);
		ClearTarget();
	}
}
