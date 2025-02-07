using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar; // Reference to the scrollbar
    [SerializeField] private RectTransform targetRect; // The RectTransform to move
    [SerializeField] private float minY; // Minimum Y position
    [SerializeField] private float maxY; // Maximum Y position

    void Start()
    {
        if (scrollbar != null)
        {
            // Add a listener to update position when the scrollbar value changes
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);
        }
    }

    private void OnScrollbarValueChanged(float value)
    {
        if (targetRect != null)
        {
            // Interpolate between minY and maxY based on the scrollbar value
            float newY = Mathf.Lerp(minY, maxY, value);
            targetRect.anchoredPosition = new Vector2(targetRect.anchoredPosition.x, newY);
        }
    }

    private void OnDestroy()
    {
        // Remove the listener when the script is destroyed
        if (scrollbar != null)
        {
            scrollbar.onValueChanged.RemoveListener(OnScrollbarValueChanged);
        }
    }
}