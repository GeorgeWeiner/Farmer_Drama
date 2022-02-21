using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] Image UpgradeImg;

    [SerializeField] [TextArea(10, 10)] protected string description;
    public string Description => description;

    private void Awake()
    {
        UpgradeImg.gameObject.AddComponent<EventTrigger>();
        AddEvent(EventTriggerType.PointerClick, delegate { UpgradeFunction(UpgradeImg.gameObject); }, UpgradeImg.gameObject);
    }


    protected virtual void UpgradeFunction(GameObject upgradeImage)
    {
        SpawnManager.instance.DeactivateUpgradeUI();
    }


    protected void AddEvent(EventTriggerType type, UnityAction<BaseEventData> action, GameObject button)
    {
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    } 

}
