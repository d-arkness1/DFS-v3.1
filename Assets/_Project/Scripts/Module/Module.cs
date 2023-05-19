using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Module : MonoBehaviour
{
    [Header("Data Transfer")]
    [SerializeField] private int dataTransferRate = 1;
    private int dataCount;
    public Sprite dataSprite;
    [SerializeField] private DataObject dataObjectPrefab;
    [SerializeField] private Transform iconsParent;
    [SerializeField] private Image iconPrefab;
    [SerializeField] private TextMeshProUGUI dataCountTMP;
    private Coroutine transferDataRoutine;
    DataObject dataObjectTemplate;

    [Space]
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI nameTMP;
    [SerializeField] private Image iconImage;
    [SerializeField] private Tooltip tooltip;
    [SerializeField] private LineRenderer connectionLine;
    public Transform endPoint;

    public Sprite icon;
    [HideInInspector] public int identifierSelected;
    public ModuleFieldValue[] fieldValues;

    public UnityAction<Module> onClickAction;

    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(() => onClickAction.Invoke(this));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeModule()
    {
        StartCoroutine(InitializeLinePositionRoutine());
    }

    private IEnumerator InitializeLinePositionRoutine()
    {
        // Allow layout management to adjust module position first
        yield return new WaitForEndOfFrame();

        endPoint.transform.localPosition = new Vector3(-transform.localPosition.x, 0, 0);
        connectionLine.SetPosition(1, new Vector3(-transform.localPosition.x / 100, 0, 0));
    }

    public void TransferData(Module source)
    {
        if (transferDataRoutine != null)
        {
            StopCoroutine(transferDataRoutine);
            Destroy(dataObjectTemplate.gameObject);
        }

        transferDataRoutine = StartCoroutine(TransferDataRoutine(source));
    }

    private IEnumerator TransferDataRoutine(Module source)
    {
        // Create template
        dataObjectTemplate = Instantiate(dataObjectPrefab, GameManager.Instance.dataTransferParent);
        dataObjectTemplate.SetSprite(source.dataSprite);
        dataObjectTemplate.gameObject.SetActive(false);

        // Create data objects to transfer
        float dataTransferInterval = 1 / dataTransferRate;
        while (true)
        {
            yield return new WaitForSeconds(dataTransferInterval);
            if (!source)
                yield break;

            DataObject dataObject = Instantiate(dataObjectTemplate, GameManager.Instance.dataTransferParent);
            dataObject.gameObject.SetActive(true);
            dataObject.Transfer(source, this);
        }
    }

    public void AddData(int count = 1)
    {
        dataCount += count;
        dataCountTMP.text = dataCount.ToString();
    }

    public void SetIdentifier(string moduleName, Sprite moduleIcon)
    {
        name = moduleName;
        icon = moduleIcon;

        nameTMP.text = moduleName;
        iconImage.sprite = moduleIcon;
    }

    public void SetFieldValues(ModuleFieldValue[] fieldValues)
    {
        // Clear existing icons
        while (iconsParent.childCount > 0)
            DestroyImmediate(iconsParent.GetChild(0).gameObject);

        this.fieldValues = fieldValues;

        foreach (ModuleFieldValue fieldValue in fieldValues)
        {
            if (fieldValue.field is ArrayModuleField)
            {
                // Add the icon of the selected value to the module
                Image icon = Instantiate(iconPrefab, iconsParent);
                ModuleFieldOption[] options = ((ArrayModuleField)fieldValue.field).options;
                if (options.Length > 0)
                {
                    ModuleFieldOption selectedOption = options[fieldValue.value];
                    icon.sprite = selectedOption.itemImage;

                    // Add tooltip
                    string tooltipText = $"<b>{selectedOption.name}</b>";
                    if (selectedOption.description.Length > 0)
                        tooltipText += $"<br>{selectedOption.description}";
                    icon.GetComponent<Tooltip>().Text = tooltipText;
                }
            }
        }
        LayoutRebuilder.MarkLayoutForRebuild((RectTransform)iconsParent.transform);
    }

    public void SetToolTip(string text)
    {
        tooltip.Text = text;
    }
}

[System.Serializable]
public struct ModuleFieldValue
{
    public ModuleField field;
    public int value;
}
