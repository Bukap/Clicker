using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class VFXManager : MonoBehaviour
{
    private RectTransform safeArea;



    void Awake()
    {
        safeArea = GameObject.Find("SafeArea").GetComponent<RectTransform>();
    }


    void Update()
    {
        
    }

    public void OnScreenEffect(GameObject effect)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            safeArea,
            Input.GetTouch(0).position,
            Camera.main, 
            out Vector2 localPoint
        );

        GameObject position = Instantiate(effect, safeArea);
        position.transform.localPosition =  localPoint;
    }


}
