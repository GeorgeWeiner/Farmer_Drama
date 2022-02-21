using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scythe : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float dmg;
    [SerializeField] float maxDistance;
    [SerializeField] Transform player;
    public Transform Player { get { return player; } set { player = value; } }
    private void Update()
    {
        RotateAroundPlayer();
    }
    void RotateAroundPlayer()
    {
        if(Time.timeScale == 1)
        {
            transform.RotateAround(new Vector3(player.position.x,0,player.position.z), Vector3.up, rotationSpeed * Time.deltaTime);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDmg(dmg);
        }
    }
}
