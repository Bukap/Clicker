using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarController : MonoBehaviour
{
    [SerializeField] private Scrollbar scrollbar; // Reference to the scrollbar
    [SerializeField] private RectTransform targetRect; // The RectTransform to move
    [SerializeField] private float min; // Minimum Y position
    [SerializeField] private float max; // Maximum Y position
    [SerializeField] private bool  horizontal; // Maximum Y position

    [SerializeField] private float itemCellWidth = 290;
    [SerializeField] private float itemCellHeight;

    void Start()
    {
        if (scrollbar != null)
        {
            // Add a listener to update position when the scrollbar value changes
            scrollbar.onValueChanged.AddListener(OnScrollbarValueChanged);

            if (horizontal)
            {
                max = itemCellWidth * transform.GetChild(0).childCount;
            }
        }
    }

    

    private void OnScrollbarValueChanged(float value)
    {
        if (targetRect != null)
        {
            if (!horizontal)
            {
                // Interpolate between minY and maxY based on the scrollbar value                
                float newY = Mathf.Lerp(min, max, value);
                targetRect.anchoredPosition = new Vector2(targetRect.anchoredPosition.x, newY);
            }
            else 
            {
                float newX = Mathf.Lerp(min, max, value);
                targetRect.anchoredPosition = new Vector2(-newX, targetRect.anchoredPosition.y );
            }
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

    private async void OnEnable()
    {
        await Task.Delay(100);
        max = itemCellWidth * (transform.GetChild(0).childCount >= 3 ? transform.GetChild(0).childCount - 2 : 0);
        print(transform.GetChild(0).childCount);
        scrollbar.value = 0;
    }
}