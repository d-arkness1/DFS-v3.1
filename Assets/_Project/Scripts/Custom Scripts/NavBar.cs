using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavBar : MonoBehaviour
{
	[SerializeField] private ScrollRect _scrollRect;

	private RectTransform _content;
	private bool isFirst = true;

	// Start is called before the first frame update
	void Start()
	{
		_content = _scrollRect.content;
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	public void CreateNavButton(RectTransform target, Sprite icon = null)
	{
		Button button = new GameObject(target.name, typeof(Button), typeof(Image)).GetComponent<Button>();
		button.transform.SetParent(transform, false);

		button.image = button.GetComponent<Image>();
		button.image.sprite = icon;

		button.onClick.AddListener(() => SnapTo(target));

		if (isFirst)
		{
			isFirst = false;
			GetComponent<CanvasGroup>().alpha = 1;
		}
	}

	public void SnapTo(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();
		_scrollRect.StopMovement(); 

		_content.anchoredPosition =
			(Vector2)_scrollRect.transform.InverseTransformPoint(_content.position)
			- (Vector2)_scrollRect.transform.InverseTransformPoint(target.position);
	}
}
