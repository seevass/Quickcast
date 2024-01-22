using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;

    private void Update()
    {
        transform.position = cameraTransform.position;
    }
}

