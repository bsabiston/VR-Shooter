using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickButton : MonoBehaviour {
	
	private bool active;
	Button b;

	void Start () {
		active = false;
		b = GetComponent<Button>();

		EventTrigger trigger = gameObject.AddComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerEnter;
		entry.callback.AddListener( (eventData) => { Activate(); } );
		trigger.triggers.Add(entry);

		entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerExit;
		entry.callback.AddListener( (eventData) => { Deactivate(); } );
		trigger.triggers.Add(entry);

	}

	void Update () {
		if (active == true && Input.GetButtonDown ("Fire1")) {
			b.onClick.Invoke ();
		}
	}

	public void Activate(){
		active = true;
	}

	public void Deactivate(){
		active = false;
	}
}