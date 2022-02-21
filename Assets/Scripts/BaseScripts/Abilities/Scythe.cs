using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float scytheRotationSpeed;
    [SerializeField] float dmg;
    [SerializeField] float maxDistance;
    [SerializeField] Transform player;
    public Transform Player { get { return player; } set { player = value; } }
    private void Update()
    {
        RotateAroundPlayer();
        Rotate();
    }
    void RotateAroundPlayer()
    {
        if(Time.timeScale == 1)
        {
            transform.RotateAround(player.position , player.up, rotationSpeed * Time.deltaTime);
        }   
    }
    void Rotate()
    {
        transform.Rotate(Vector3.up * scytheRotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDmg(dmg);
        }
    }
}
