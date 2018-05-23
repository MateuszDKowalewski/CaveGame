using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour {

	[SerializeField] GameObject m_Player;
	public GameObject m_StonesConteiner;
	public GameObject[,] m_ObjectMap;
	public GameObject []m_GameBricks;

	//Game gricks ID
	/*
	 	0- AirBlock
		1- Stone
		2- IronOre
		3- Purple
		4- Yellow
		5- Orange
		6- Green
		7- PutStone
		8- IronBlock
		9- Bedrock
	*/

	[SerializeField] int m_ChanceToSpawnBrick = 50;
	[SerializeField] int m_Loop = 10;
	public int m_Width = 100;
	public int m_Height = 100;

	public int [,]m_BlockMap;

	void Start () {
		//Set Up
		m_StonesConteiner = new GameObject ("StonesContainer");
		m_BlockMap = new int [m_Width, m_Height];
		m_ObjectMap = new GameObject[m_Width, m_Height];


		//Generate
		int m_Random;
		for (int i = 0; i < m_Width; i++) {
			for (int j = 0; j < m_Height; j++) {
				if ((i == 0) || (j == 0) || (i == m_Width - 1) || (j == m_Height - 1)) {
					m_BlockMap [i, j] = 1;
				} else {
					m_ObjectMap [i, j] = null;
					m_Random = Random.Range (0, 100);
					if (m_ChanceToSpawnBrick < m_Random) {
						m_BlockMap [i, j] = 0;
					} else {
						m_BlockMap [i, j] = 1;
					}
				}
			}
		}
		spawnsPlaces ();
		symulate ();
		generateSpawns ();


		//Render
		generateOres ();
		printMap ();
		printBedrock ();
		printPlayer ();
	}

	void spawnsPlaces(){
		for (int i = 0; i < 13; i++) {
			for (int j = 0; j < 13; j++) {
				m_BlockMap [i, j] = 1;
				m_BlockMap [m_Width - 1 - i, m_Height - 1 - j] = 1;
			}
		}
	}

	void generateSpawns(){
		for (int i = 2; i < 10; i++) {
			for (int j = 2; j < 10; j++) {
				m_BlockMap [i, j] = 0;
				m_BlockMap [m_Width - 1 - i, m_Height - 1 - j] = 0;
			}
		}
		for (int i = 0; i < 4; i++) {
			m_BlockMap [1, 4 + i] = 0;
			m_BlockMap [10, 4 + i] = 0;
			m_BlockMap [4 + i, 1] = 0;
			m_BlockMap [4 + i, 10] = 0;

			m_BlockMap [m_Width - 2, m_Height - (5 + i)] = 0;
			m_BlockMap [m_Width - 11, m_Height - (5 + i)] = 0;
			m_BlockMap [m_Width - (5 + i), m_Height - 2] = 0;
			m_BlockMap [m_Width - (5 + i), m_Height - 11] = 0;
		}
	}

	void printBedrock(){
		for (int i = 0; i < m_Width; i++) {
			Instantiate(m_GameBricks[8], new Vector3(i, -1, 0), Quaternion.identity, m_StonesConteiner.transform);
			Instantiate(m_GameBricks[8], new Vector3(i, m_Height, 0), Quaternion.identity, m_StonesConteiner.transform);
		}
		for (int i = 0; i < m_Height; i++) {
			Instantiate(m_GameBricks[8], new Vector3(-1, i, 0), Quaternion.identity, m_StonesConteiner.transform);
			Instantiate(m_GameBricks[8], new Vector3(m_Width, i, 0), Quaternion.identity, m_StonesConteiner.transform);
		}
		Instantiate(m_GameBricks[8], new Vector3(-1, -1, 0), Quaternion.identity, m_StonesConteiner.transform);
		Instantiate(m_GameBricks[8], new Vector3(m_Width, -1, 0), Quaternion.identity, m_StonesConteiner.transform);
		Instantiate(m_GameBricks[8], new Vector3(m_Width, m_Height, 0), Quaternion.identity, m_StonesConteiner.transform);
		Instantiate(m_GameBricks[8], new Vector3(-1, m_Height, 0), Quaternion.identity, m_StonesConteiner.transform);
	}

	void printPlayer(){
		int x;
		int y;
		do {
			x = Random.Range (0, m_Width);
			y = Random.Range (0, m_Height);
		} while (m_BlockMap [x, y] != 0);
		GameObject player = Instantiate (m_Player, new Vector3 (x, y, -9), Quaternion.identity);
		player.name = "Player";
	}

	void colliderSetUp(){
		bool collider;
		for (int i = 0; i < m_Width; i++) {
			for (int j = 0; j < m_Height; j++) {
				if(m_BlockMap[i, j] != 0){
					collider = false;

					if ((i - 1 > 0) && (m_BlockMap [i - 1, j] == 0)) {
						collider = true;
					} else if ((j - 1 > 0) && (m_BlockMap [i, j - 1] == 0)) {
						collider = true;
					} else if ((i + 1 < m_Width) && (m_BlockMap [i + 1, j] == 0)) {
						collider = true;
					} else if ((j + 1 < m_Height) && (m_BlockMap [i, j + 1] == 0)) {
						collider = true;
					}
					m_ObjectMap [i, j].GetComponent<BoxCollider2D> ().enabled = collider;
				}
			}
		}
	}

	void symulate(){
		for (int k = 0; k < m_Loop; k++) {
			int[,] copy = new int[m_Width, m_Height];
			int nbs;
			for (int i = 0; i < m_Width; i++) {
				for (int j = 0; j < m_Height; j++) {
					nbs = add (i, j);
					if (m_BlockMap [i, j] != 0) {
						if (nbs < 4) {
							copy [i, j] = 0;
						} else {
							copy [i, j] = 1;
						}
					} else {
						if (nbs > 4) {
							copy [i, j] = 1;
						} else {
							copy [i, j] = 0;
						}
					}
				}
			}

			for (int i = 0; i < m_Width; i++) {
				for (int j = 0; j < m_Height; j++) {
					m_BlockMap [i, j] = copy [i, j];
				}
			}
		}
	}

	int add(int x, int y){
		int nbs = 0;
		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				if((i != 0) || (j != 0)){
					if((x + i > 0) && (x + i < m_Width) && (y + j > 0) && (y + j < m_Width)){
						if (m_BlockMap [x + i, y + j] != 0) {
							nbs++;
						}
					} else {
						nbs++;
					}
				}
			}
		}
		return nbs;
	}

	void printMap(){
		for (int i = 0; i < m_Width; i++) {
			for (int j = 0; j < m_Height; j++) {
				if (m_BlockMap [i, j] != 0) {
					putBlock (i, j, i, j, m_BlockMap[i, j]);
				}
			}
		}
		colliderSetUp ();
	}

	void putBlock(float x, float y, int i, int j, int ID){
		m_ObjectMap[i, j] = Instantiate(m_GameBricks[ID - 1], new Vector3(x, y, 0), Quaternion.identity, m_StonesConteiner.transform);
		m_ObjectMap [i, j].GetComponent<BoxCollider2D> ().enabled = false;
	}

	void generateOres (){
		int x;
		int y;
		int size;
		for(int i = 2; i < 7; i++){
			for(int j = Random.Range(25, 41); j > 0; j--){
				size = Random.Range (3, 7);
				do{
					x = Random.Range (0, m_Width);
					y = Random.Range (0, m_Width);
				} while (m_BlockMap[x, y] != 1);
				generateOre (x, y, size, size, i); 
			}
		}
	}

	void generateOre(int x, int y, int size, int range, int ID){
		int random = Random.Range (0, size);
		int x1;
		int y1;
		if (random < range){
			m_BlockMap[x, y] = ID;
			x1 = x + 1;
			y1 = y;
			if ((x1 < m_Width) && (m_BlockMap[x1, y1] == 1)){
				generateOre (x1, y1, size, range - 1, ID);
			}
			x1 = x - 1;
			y1 = y;
			if ((x1 >= 0) && (m_BlockMap[x1, y1] == 1)){
				generateOre (x1, y1, size, range - 1, ID);
			}
			x1 = x;
			y1 = y + 1;
			if ((y1 < m_Height) && (m_BlockMap[x1, y1] == 1)){
				generateOre (x1, y1, size, range - 1, ID);
			}
			x1 = x;
			y1 = y - 1;
			if ((y1 > 0) && (m_BlockMap[x1, y1] == 1)){
				generateOre (x1, y1, size, range - 1, ID);
			}
		}
	}
}