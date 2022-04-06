using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : PlayerController
{
	public int killPoint=2;
   public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	
	float timeToFire = 0;
	Transform firePoint;

	// Use this for initialization
	void Awake () {
		firePoint = transform.Find ("weapon");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (fireRate == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				Shoot();
				//SetPointText;
				//SetHealthText;
			}
		}
		else {
			if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
				//SetPointText;
				//SetHealthText;
			}
		}
	}
	
	public void Shoot () {
		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition-firePointPosition, 100, whatToHit);
		Debug.DrawLine (firePointPosition, (mousePosition-firePointPosition)*100, Color.red);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			if (hit.collider.gameObject.tag == "Enemy")
			{
				Destroy (hit.collider.gameObject);
				Points(killPoint);
				//SetPointText;
				//SetHealthText;
			}
			
		}
	}


}
