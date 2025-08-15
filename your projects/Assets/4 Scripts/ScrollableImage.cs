using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScrollableImage : MonoBehaviour
{
    [SerializeField]
    RawImage scrollableImage;
    [SerializeField]
    float scrollSpeed;
    [SerializeField]
    Vector2 scrollDirection;
    Rect rect;

    void Start()
    {
        rect = scrollableImage.uvRect;
    }

    void Update()
    {
        rect.x += scrollDirection.x * scrollSpeed * Time.deltaTime;
        rect.y += scrollDirection.y * scrollSpeed * Time.deltaTime;
        scrollableImage.uvRect = rect;
        
    }
}
