using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BottomBar : MonoBehaviour {

	public GameObject button;
	public DataLoader dl;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Refresh(string s) {

		for (int i = transform.childCount - 1; i >= 0; i--) {
			Destroy (transform.GetChild(i).gameObject);
		}

		foreach (Session ses in DATA.sessions) {
			if (ses.gameName == dl.game && ses.lookData.dictionary.ContainsKey(s)) {
				GameObject a = Instantiate (button, transform);
				a.GetComponentInChildren<Text> ().text = "c";
			}
		}
	}
}
