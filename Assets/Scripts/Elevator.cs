using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private Transform[] targetTransforms;

    [SerializeField] private UnityEvent triEvent;

    public void Move(int target)
    {
       StartCoroutine(nameof(MoveToTarget), target);
    }
    private IEnumerator MoveToTarget(int targetIndex)
    {
        Transform targetTransform = targetTransforms[targetIndex];

        while (transform.position != targetTransform.position)
        {
            var step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, step);
            yield return null;
        }
        Debug.Log("Reached Destination!");
    }
}