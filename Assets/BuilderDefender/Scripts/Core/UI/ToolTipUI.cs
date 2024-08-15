using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTipUI : MonoBehaviour
{
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
        rectTransform = GetComponent<RectTransform>();

        textMeshPro = transform.Find(TEXT).GetComponent<TextMeshProUGUI>();

        backgroundRectTransform = transform.Find(BACKGROUND).GetComponent<RectTransform>();

        SetText("Hi There!");
    }


    private void Update()
    {
        rectTransform.anchoredPosition = Input.mousePosition / canvasRectTransform.localScale.x;
    }


    private void SetText(string toolTipText)
    {
        textMeshPro.SetText(toolTipText);
        textMeshPro.ForceMeshUpdate();

        Vector2 textSize = textMeshPro.GetRenderedValues(false);
        Vector2 padding = new Vector2(8, 8);

        backgroundRectTransform.sizeDelta = textSize + padding;
    }
}
