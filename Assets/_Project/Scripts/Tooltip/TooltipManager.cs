using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(RectTransform))]
public class TooltipManager : MonoBehaviour
{
	public static TooltipManager Instance { get; private set; }

	[SerializeField] private Camera _camera;
	[SerializeField] private RectTransform canvasRect;
	private RectTransform rect;
	[SerializeField] private TextMeshProUGUI tooltipTMP;

	[SerializeField] private Vector2 padding;
	[SerializeField] private Vector2 offset;
	[SerializeField] private float maxWidth;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(this);
			return;
		}

		rect = GetComponent<RectTransform>();
		HideTooltip();
	}

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		UpdatePosition();
	}

	public void ShowTooltip(string text)
	{
		if (text.Length == 0) return;

		gameObject.SetActive(true);

		// Update display text
		tooltipTMP.SetText(text);
		tooltipTMP.enableWordWrapping = false;
		tooltipTMP.rectTransform.sizeDelta = -padding * 2;
		tooltipTMP.ForceMeshUpdate(true, true);

		// Set target rect size
		Vector2 textRectSize = tooltipTMP.GetRenderedValues();
		Vector2 targetRectSizeDelta = textRectSize + padding * 2;

		// Resize if target rect width is greater than max width
		if (maxWidth > 0 && targetRectSizeDelta.x > maxWidth)
		{
			// Set width
			targetRectSizeDelta.x = maxWidth;
			rect.sizeDelta = targetRectSizeDelta;

			// Enable text wrapping and recalculate target height
			tooltipTMP.enableWordWrapping = true;
			tooltipTMP.ForceMeshUpdate(true, true);
			targetRectSizeDelta.y = tooltipTMP.GetRenderedValues().y + padding.y * 2;
		}

		// Set final size
		rect.sizeDelta = targetRectSizeDelta;
		rect.ForceUpdateRectTransforms();

		UpdatePosition();
	}

	public void HideTooltip()
	{
		gameObject.SetActive(false);
	}

	public void UpdatePosition()
	{
		Vector2 targetPos;

		Vector3 screenPoint = Input.mousePosition;
		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, screenPoint, _camera, out targetPos);
		targetPos += offset;

		// Keep tooltip inside canvas
		if (targetPos.x + rect.rect.width > canvasRect.rect.width / 2)
		{
			targetPos.x = canvasRect.rect.width / 2 - rect.rect.width;
		}

		rect.anchoredPosition = targetPos;
	}
}
