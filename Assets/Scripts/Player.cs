﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity {

	public float moveSpeed = 5;


	public Crosshairs crosshairs;

	Camera viewCamera;
	PlayerController controller;
	GunController gunController;

	// Use this for initialization

	protected override void Start () {
		base.Start ();

	}

	void Awake() {
		controller = GetComponent<PlayerController> ();
		gunController = GetComponent<GunController> ();
		viewCamera = Camera.main;
		FindObjectOfType<Spawner> ().OnNewWave += OnNewWave;
	}

	void OnNewWave(int WaveNumber) {
		health = startingHealth;
		gunController.EquipGun (WaveNumber - 1);
	}

	// Update is called once per frame
	void Update () {
		// Movement input
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		Vector3 movement = Camera.main.transform.TransformDirection(moveVelocity);
		controller.Move(movement);

		/*
		// Look input
		Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.up * gunController.GunHeight);
		float rayDistance;

		if (groundPlane.Raycast (ray, out rayDistance)) {
			Vector3 point = ray.GetPoint (rayDistance);
			// Debug.DrawLine (ray.origin, point, Color.red);
			controller.LookAt(point);
			crosshairs.transform.position = point;
			crosshairs.DetectTargets (ray);
			if ((new Vector2 (point.x, point.z) - new Vector2 (transform.position.x, transform.position.z)).sqrMagnitude > 1) {
				gunController.Aim (point);
			}
		}
		*/

		// Weapon input
		//if (Input.GetMouseButton (0)) {
		if (Input.GetButtonDown ("Fire1")) {
			if (!dead) {
				gunController.OnTriggerHold ();
			}
		}
		//if (Input.GetMouseButtonUp (0)) {
		if (Input.GetButtonUp ("Fire1")) {
			if (!dead) {
				gunController.OnTriggerRelease ();
			}
		}

		//if (Input.GetKeyDown (KeyCode.R)) {
		if (Input.GetButtonDown ("Fire2")) {
			if (!dead) {
				gunController.Reload ();
			}
		}

		if (transform.position.y < -10) {
			TakeDamage (health);
		}
	}

	public override void Die() {
		AudioManager.instance.PlaySound ("Player Death", transform.position);
		base.Die ();
	}




}