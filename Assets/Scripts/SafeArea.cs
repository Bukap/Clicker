using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class SafeArea : MonoBehaviour
{
    private RectTransform rectTransform;
    private Rect lastSafeArea = new Rect(0, 0, 0, 0);

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        ApplySafeArea();
    }

    void ApplySafeArea()
    {
        Rect safeArea = Screen.safeArea;

        // Only apply changes if the safe area has changed
        if (safeArea != lastSafeArea)
        {
            // Convert safe area rectangle to normalized coordinates (0-1)
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            // Apply safe area anchors to the RectTransform
            rectTransform.anchorMin = anchorMin;
            rectTransform.anchorMax = anchorMax;

            // Update last safe area to prevent redundant recalculations
            lastSafeArea = safeArea;
        }
    }

    void Update()
    {
        // Continuously check for safe area updates in case of orientation changes or UI updates
        ApplySafeArea();
    }
}
