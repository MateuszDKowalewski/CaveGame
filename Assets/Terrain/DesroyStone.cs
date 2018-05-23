using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesroyStone : MonoBehaviour {
	private PlayerEQ m_PlayerEQ;
	private GenerateMap m_Map;

	[SerializeField] int m_BlockID = 1;
	private int m_xPos;
	private int m_yPos;

	void Start (){
		GameObject manager = GameObject.Find ("LevelManager");
		GameObject player = GameObject.Find ("Player");
		m_Map = manager.GetComponent<GenerateMap> ();
		m_PlayerEQ = player.GetComponent<PlayerEQ> ();
		m_xPos = (int) transform.position.x;
		m_yPos = (int) transform.position.y;
	}

	void OnTriggerStay2D (Collider2D colider) {
		if ((colider.gameObject.tag == "Player") && (Input.GetMouseButton(0))) {
			destroyBlock ();
		}
	}

	void destroyBlock (){
		if ((m_xPos + 1 < m_Map.m_Width) && (m_Map.m_BlockMap [m_xPos + 1, m_yPos] != 0)) {
			m_Map.m_ObjectMap [m_xPos + 1, m_yPos].GetComponent<BoxCollider2D> ().enabled = true;
		}
		if ((m_xPos - 1 > -1) && (m_Map.m_BlockMap [m_xPos - 1, m_yPos] != 0)) {
			m_Map.m_ObjectMap [m_xPos - 1, m_yPos].GetComponent<BoxCollider2D> ().enabled = true;
		}
		if ((m_yPos + 1 < m_Map.m_Width) && (m_Map.m_BlockMap [m_xPos, m_yPos + 1] != 0)) {
			m_Map.m_ObjectMap [m_xPos, m_yPos + 1].GetComponent<BoxCollider2D> ().enabled = true;
		}
		if ((m_yPos - 1 > -1) && (m_Map.m_BlockMap [m_xPos, m_yPos - 1] != 0)) {
			m_Map.m_ObjectMap [m_xPos, m_yPos - 1].GetComponent<BoxCollider2D> ().enabled = true;
		}
			
		m_PlayerEQ.addItem (m_BlockID, 1);
		m_Map.m_ObjectMap [m_xPos, m_yPos] = null;
		m_Map.m_BlockMap [m_xPos, m_yPos] = 0;
		Destroy (gameObject);
	}
}