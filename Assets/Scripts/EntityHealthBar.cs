using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityHealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBarFull;

    [SerializeField]
    private GameObject healthBarEmpty;

    [SerializeField]
    private Vector2 healthBarLocation;

    private GameObject barFullObj;
    public GameObject BarFullObj => barFullObj;

    private GameObject barEmptyObj;
    public GameObject BarEmptyObj => barEmptyObj;

    Transform parent;
    private RectTransform fullRect;
    private RectTransform emptyRect;
    private RectTransform canvasRect;
    private Image barFullImg;
    private EntityHealth entityHealth;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        // Get parent transform
        parent = this.gameObject.transform;
        canvasRect = FindFirstObjectByType<Canvas>().GetComponent<RectTransform>();
        entityHealth = GetComponent<EntityHealth>();

        // Create game objects and set parents
        barEmptyObj = Instantiate(healthBarEmpty);
        barEmptyObj.transform.SetParent(canvasRect, true);

        barFullObj = Instantiate(healthBarFull);
        barFullObj.transform.SetParent(canvasRect, true);
        barFullImg = barFullObj.GetComponent<Image>();

        // Get rects
        fullRect = barFullObj.GetComponent<RectTransform>();
        emptyRect = barEmptyObj.GetComponent<RectTransform>();

        UpdatePosition(fullRect);
        UpdatePosition(emptyRect);
    }

    private void UpdatePosition(RectTransform uiElement)
    {
        if (uiElement == null)
            Initialize();

        Vector2 worldPoint = new Vector2(parent.position.x, parent.position.y) + healthBarLocation;
        // Convert world space to canvas space
        Vector2 ViewportPosition = FindFirstObjectByType<Camera>().WorldToViewportPoint(worldPoint);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)));

        // Set UI positions
        uiElement.anchoredPosition = WorldObject_ScreenPosition;
    }

    private void UpdateFill()
    {
        float fillAmt = (float)entityHealth.Health / (float)entityHealth.MaxHealth;
        barFullImg.fillAmount = fillAmt;
    }

    private void Update()
    {
        UpdateFill();

        if (fullRect == null || emptyRect == null)
        {
            Initialize();
        }

        UpdatePosition(fullRect);
        UpdatePosition(emptyRect);
    }
}
