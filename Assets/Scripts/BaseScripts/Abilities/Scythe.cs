using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float scytheRotationSpeed;
    [SerializeField] float dmg;
    public float ScytheDmg { get { return dmg; } set { dmg = value; } }
    [SerializeField] float maxDistance;
    public float MaxDistance => maxDistance;
    [SerializeField] Transform player;
    [SerializeField] Transform rotationPoint;
    public Transform RotationPoint { get { return rotationPoint; } set { rotationPoint = value; } }
    public Transform Player { get { return player; } set { player = value; } }
    private void Update()
    {
        if (player != null)
        {
            RotateAroundPlayer();
            Rotate();
        }
    }
    void RotateAroundPlayer()
    {
        if (player != null)
        {
            rotationPoint.position = player.transform.position;
            rotationPoint.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
    void Rotate()
    {
        ////transform.Rotate(Vector3.up * scytheRotationSpeed * Time.deltaTime);
        //transform.LookAt(new Vector3(player.position.x, player.position.y + 1f, player.position.z) , Vector3.up);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDmg(dmg);
        }
    }
}
