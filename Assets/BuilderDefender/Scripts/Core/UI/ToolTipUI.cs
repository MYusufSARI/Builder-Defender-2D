using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
    public static ToolTipUI Instance { get; private set; }

    [Header(" Elements ")]
    [SerializeField] private RectTransform canvasRectTransform;
    private RectTransform backgroundRectTransform;
    private RectTransform rectTransform;
    private TextMeshProUGUI textMeshPro;


    [Header(" Consts ")]
    private const string TEXT = "ToolText";
    private const string BACKGROUND = "ToolBackground";


    private void Awake()
    {
        Instance = this;

        rectTransform = GetComponent<RectTransform>();

        textMeshPro = transform.Find(TEXT).GetComponent<TextMeshProUGUI>();

        backgroundRectTransform = transform.Find(BACKGROUND).GetComponent<RectTransform>();

        Hide();
    }


    private void Update()
    {
        Vector2 anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;

        if (anchoredPosition.x + backgroundRectTransform.rect.width > canvasRectTransform.rect.width)
        {
            anchoredPosition.x = canvasRectTransform.rect.width - backgroundRectTransform.rect.width;
        }

        if (anchoredPosition.y + backgroundRectTransform.rect.height > canvasRectTransform.rect.height)
        {
            anchoredPosition.y = canvasRectTransform.rect.height - backgroundRectTransform.rect.height;
        }

        rectTransform.anchoredPosition = anchoredPosition;
    }


    private void SetText(string toolTipText)
    {
        textMeshPro.SetText(toolTipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + padding;
    }


    public void Show(string toolTipText)
    {
        gameObject.SetActive(true);

        SetText(toolTipText);
    }


    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
