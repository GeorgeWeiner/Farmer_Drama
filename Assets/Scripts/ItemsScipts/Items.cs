using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Items : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    protected void Update()
    {
        RotateItem();
    }
    /// <summary>
    /// Checks if the object collides with something
    /// </summary>
    /// <param name="_other"></param>
    protected virtual void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.tag == "Player")
        {
            SetNewTempStats(_other);
        }
    }
    /// <summary>
    /// Sets stats for player
    /// </summary>
    /// <param name="_collsion"></param>
    protected abstract void SetNewTempStats(Collider _collsion);

    /// <summary>
    /// How long the buff lasts
    /// </summary>
    /// <param name="_timer"></param>
    /// <param name="_collision"></param>
    /// <returns></returns>
    protected virtual IEnumerator BuffDuration(float _timer, Collider _collision)
    {
        Component[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (var renderer in meshRenderers)
        {
            Destroy(renderer);
        }
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(_timer);
    }
    void RotateItem()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
