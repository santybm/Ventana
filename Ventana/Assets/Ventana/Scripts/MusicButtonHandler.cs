﻿using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;

public class MusicButtonHandler : MonoBehaviour, IInputClickHandler, IFocusable {
    public AudioClip clickSound;
    private AudioSource source;
    public Material highlightButtonMaterial;
    public Material normalButtonMaterial;

    public void OnFocusEnter() {
        gameObject.GetComponent<Renderer>().material = highlightButtonMaterial;
    }

    public void OnFocusExit() {
        gameObject.GetComponent<Renderer>().material = normalButtonMaterial;
    }

    public void OnInputClicked(InputEventData eventData) {
        Debug.Log("Clicked " + gameObject.name);
        gameObject.SendMessageUpwards("makeAPIRequest", gameObject.name);
        source.PlayOneShot(clickSound, 1F);

    }

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
