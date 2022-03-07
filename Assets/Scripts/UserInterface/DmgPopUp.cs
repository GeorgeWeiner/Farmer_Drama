using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] TMP_Text text;
    private void Awake()
    {
        canvas.worldCamera = Camera.main;
        canvas.gameObject.SetActive(false);
    }
    public IEnumerator ActivatePopUp(float dmg , GameObject enemy)
    {
        canvas.gameObject.SetActive(true);
        text.text = dmg.ToString();
        canvas.gameObject.GetComponent<RectTransform>().position = enemy.transform.position + Vector3.up * 2;
        yield return new WaitForSeconds(0.5f);
        canvas.gameObject.SetActive(false);
    }

}
