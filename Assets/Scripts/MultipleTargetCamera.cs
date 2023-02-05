using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTargetCamera : MonoBehaviour
{
    public GameObject CMvcam1;
    public List<Transform> targets;

    public Vector2 offset;

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        Vector2 centerPoint = GetCenterPoint();

        Vector2 newPosition = centerPoint + offset;

        transform.position = newPosition;
    }

    Vector2 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].position, Vector2.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }

}

