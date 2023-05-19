using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataObject : MonoBehaviour
{
	[SerializeField] private float speed = 0.0001f;

	[SerializeField] private Image image;

	private bool initialized;
	private List<Transform> path;
	private Module end;
	private bool destroy;

	private float destroyDistance;

    private void Awake()
    {
		int moduleRadius = 200;
		int dataRadius = 25;
		destroyDistance = (moduleRadius - dataRadius) / 100f;
	}

	// Update is called once per frame
	void Update()
	{
		if (initialized)
		{
			if (destroy)
				return;

			if (end == null || path[0] == null)
            {
				destroy = true;
				Destroy(gameObject);
				return;
            }

			// Move towards the path
			if (path.Count > 1)
			{
				if (Vector3.Distance(transform.position, path[0].position) > 0.01f)
					transform.position = Vector3.MoveTowards(transform.position, path[0].position, speed * Time.deltaTime);
				else
					path.RemoveAt(0);
			}
			// Move towards module's side
			else if (path.Count == 1)
			{
				if (Mathf.Abs(path[0].position.x - transform.position.x) > destroyDistance)
					transform.position = Vector3.MoveTowards(transform.position, path[0].position, speed * Time.deltaTime);
				else
				{
					end.AddData();
					Destroy(gameObject);
				}
			}
		}
	}

	public void Transfer(Module start, Module end)
	{
		this.end = end;
		path = new List<Transform>()
		{
			start.endPoint,
			end.endPoint,
			end.transform
		};

		transform.position = Vector3.MoveTowards(start.transform.position, path[0].position, destroyDistance);

		initialized = true;
	}

	public void SetSprite(Sprite sprite)
	{
		image.sprite = sprite;
	}
}
