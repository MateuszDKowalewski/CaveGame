using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	private GameObject m_Player;

	private GenerateMap m_ScreenSize;

	private Vector3 m_CameraPos;

	private float cameraHeight;
	private float cameraWidth;
	private float m_MaxPosX;
	private float m_MinPosX;
	private float m_MaxPosY;
	private float m_MinPosY;

	void Start () {
		m_Player = GameObject.Find ("Player");
		GameObject manager = GameObject.Find ("LevelManager");
		m_ScreenSize = manager.GetComponent<GenerateMap> ();
	}

	void Update () {
		cameraHeight = Camera.main.orthographicSize;
		cameraWidth = Camera.main.orthographicSize * Camera.main.aspect;
		m_MaxPosX = m_ScreenSize.m_Width + 0.5f - cameraWidth;
		m_MinPosX = -1.5f + cameraWidth;
		m_MaxPosY = m_ScreenSize.m_Height + 0.5f - cameraHeight;
		m_MinPosY = -1.5f + cameraHeight;

		m_CameraPos = new Vector3 (	Mathf.Clamp (m_Player.transform.position.x, m_MinPosX, m_MaxPosX),
									Mathf.Clamp (m_Player.transform.position.y, m_MinPosY, m_MaxPosY),
									-10f);
		transform.position = m_CameraPos;
	}
}