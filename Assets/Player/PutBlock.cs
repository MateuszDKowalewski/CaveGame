using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutBlock : MonoBehaviour {
	[SerializeField] GameObject m_BlockToPut;

	private PlayerEQ m_Items;

	private GenerateMap m_Maps;

	private Vector3 m_MousePos;
	private Vector3 m_BlockPos;

	void Start () {
		GameObject manager = GameObject.Find ("LevelManager");
		GameObject player = GameObject.Find ("Player");
		m_Maps = manager.GetComponent<GenerateMap> ();
		m_Items = player.GetComponent<PlayerEQ> ();	
	}

	void Update () {
		if ((Input.GetMouseButtonDown (1)) && (m_Items.m_ItemsInEq[m_Items.m_putBlockID - 1] > 0)) {
			m_MousePos = Input.mousePosition;
			m_MousePos = Camera.main.ScreenToWorldPoint (m_MousePos);
			m_BlockPos = new Vector3 ((int) (m_MousePos.x + 0.5f), (int) (m_MousePos.y + 0.5f), 0);
			putBlock (m_BlockPos.x, m_BlockPos.y, (int)m_BlockPos.x, (int)m_BlockPos.y, m_Items.m_putBlockID);
		}
	}

	void putBlock(float x, float y, int i, int j, int ID){
		if (m_Maps.m_BlockMap [i, j] == 0) {
			m_Maps.m_ObjectMap [i, j] = Instantiate (m_Maps.m_GameBricks [ID - 1], new Vector3 (x, y, 0), Quaternion.identity, m_Maps.m_StonesConteiner.transform);
			m_Maps.m_BlockMap [i, j] = ID;
			if (m_Maps.m_BlockMap [i + 1, j] != 0) { 
				deletColider (i + 1, j);
			}
			if (m_Maps.m_BlockMap [i - 1, j] != 0) {
				deletColider (i - 1, j);
			}
			if (m_Maps.m_BlockMap [i, j + 1] != 0) {
				deletColider (i, j + 1);
			}
			if (m_Maps.m_BlockMap [i, j - 1] != 0) {
				deletColider (i, j - 1);
			}
			m_Items.m_ItemsInEq [m_Items.m_putBlockID - 1]--;
		}
	}

	void deletColider(int x, int y){
		bool need = true;

		if (m_Maps.m_BlockMap [x + 1, y] == 0) {
			need = false;
		}
		if (m_Maps.m_BlockMap [x - 1, y] == 0) {
			need = false;
		}
		if (m_Maps.m_BlockMap [x, y + 1] == 0) {
			need = false;
		}
		if (m_Maps.m_BlockMap [x, y - 1] == 0) {
			need = false;
		}

		if (need) {
			m_Maps.m_ObjectMap [x, y].GetComponent<BoxCollider2D> ().enabled = false;
		}
	}
}