using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] private Camera camera;
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z += 10;
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = CorrectByRect(Camera.main, mouseWorldPos);
    }

    public Vector2 CorrectByRect(Camera camera, Vector2 pointer)
    {
        var size = camera.rect.size;
        var offset = camera.pixelRect.position;
        return new Vector2(pointer.x * size.x + offset.x, pointer.y * size.y + offset.y);
    }
}
