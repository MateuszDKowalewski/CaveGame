using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	private Rigidbody2D m_PlayerRigidbody;

	private Vector2 m_PlayerVellocity;

	[SerializeField] float m_PalyerSpeed = 5;

	void Start () {
		m_PlayerRigidbody = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		playerMove ();
		playerRotate ();
	}

	void playerMove(){
		m_PlayerVellocity = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		m_PlayerVellocity.Normalize ();
		m_PlayerRigidbody.velocity = m_PlayerVellocity * m_PalyerSpeed;
	}

	void playerRotate(){
		Vector3 mousePos = Input.mousePosition;
		mousePos = Camera.main.ScreenToWorldPoint (mousePos);
		mousePos.z = transform.transform.position.z;
		mousePos -= transform.position;
		float rotation = Mathf.Atan2 (mousePos.y, mousePos.x) * Mathf.Rad2Deg  - 90;
		transform.rotation = Quaternion.Euler (new Vector3 (0, 0, rotation));
	}
}