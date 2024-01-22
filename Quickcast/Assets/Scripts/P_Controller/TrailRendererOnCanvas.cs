using UnityEngine;
using UnityEngine.UI;

public class TrailRendererOnCanvas : MonoBehaviour
{
    Vector2 pos;

    void Update()
    {
        // Move the trail renderer to follow the mouse
        if (Input.GetMouseButton(1))
        {
            
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = mousePosition;
        }
    }
}
