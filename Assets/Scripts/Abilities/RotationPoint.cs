using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPoint : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform player;
    void Update()
    {
        if (player != null)
        {
            RotateAroundPlayer();
        }
    }
    void RotateAroundPlayer()
    {
        if (player != null)
        {
            transform.position = player.transform.position;
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
