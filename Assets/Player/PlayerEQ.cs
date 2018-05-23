using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEQ : MonoBehaviour {
	public int []m_ItemsInEq;

	public int m_putBlockID;

	void Start () {
		m_ItemsInEq = new int[8];
		for (int i = 0; i < 8; i++) {
			m_ItemsInEq [i] = 0;
		}
		m_putBlockID = 7;
		m_ItemsInEq [6] = 10;
		m_ItemsInEq [7] = 10;
	}

	void Update () {
		if ((Input.GetAxis ("Mouse ScrollWheel")) != 0) {
			if (m_putBlockID == 7) {
				m_putBlockID = 8;
			} else {
				m_putBlockID = 7;
			}
		}
	}

	void  OnGUI(){
		int w = Screen.width;
		int h = Screen.height;

		//Left bar
		Rect leftRect = new Rect (0, 0, 100, h);
		GUIStyle leftStyle = new GUIStyle ();
		leftStyle.alignment = TextAnchor.UpperLeft;
		leftStyle.fontSize = h / 25;
		leftStyle.normal.textColor = new Color (0.8f, 0.8f, 0.2f);
		string leftText =	"\n" +
		                  string.Format (" Stone {0}\n", m_ItemsInEq [0]) +
		                  string.Format (" Iron {0}\n", m_ItemsInEq [1]) +
		                  string.Format (" Purple {0}\n", m_ItemsInEq [2]) +
		                  string.Format (" Yellow {0}\n", m_ItemsInEq [3]) +
		                  string.Format (" Orange {0}\n", m_ItemsInEq [4]) +
		                  string.Format (" Green {0}\n", m_ItemsInEq [5]);

		GUI.Label (leftRect, leftText, leftStyle);


		//Right bar
		Rect rightRect = new Rect (w - 100, 0, 100, h);
		GUIStyle rightStyle = new GUIStyle ();
		rightStyle.alignment = TextAnchor.UpperRight;
		rightStyle.fontSize = h / 25;
		rightStyle.normal.textColor = new Color (0.8f, 0.8f, 0.2f);
		string rightText =	"\n" +
		                  	string.Format ("Stone {0} \n", m_ItemsInEq [6]) +
							string.Format ("Iron {0} \n", m_ItemsInEq [7]);

		GUI.Label (rightRect, rightText, rightStyle);

		//Bottom bar
		Rect bottomRect = new Rect(w - 100, h - 100, 100, 100);
		GUIStyle bottomStyle = new GUIStyle ();
		bottomStyle.alignment = TextAnchor.LowerRight;
		bottomStyle.fontSize = h / 25;
		bottomStyle.normal.textColor = new Color (0.8f, 0.8f, 0.2f);
		string bottomText;
		switch (m_putBlockID) {
			case 7:
			bottomText = "Stone Block\n";
			break;

			case 8:
			bottomText = "Iron Block\n";
			break;

			default:
				bottomText = "Error";
			break;
		}
		GUI.Label (bottomRect, bottomText, bottomStyle);
	}

	public void addItem(int ID, int n){
		m_ItemsInEq [ID - 1] += n;
	}
}