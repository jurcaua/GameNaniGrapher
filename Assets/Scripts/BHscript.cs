using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BHscript : MonoBehaviour {

	public LookData ld;
	public GameObject ldr;

	public GameObject bb;
	public GameObject other;

	public GameObject texty;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void Initialise() {
		ldr.gameObject.SetActive (true);


		int i = 1;
		foreach (LookSession ls in ld.list) {
			GameObject t = Instantiate (texty, ldr.transform.GetChild(0));
			t.GetComponent<Text> ().text = string.Format ("\t\tLookSession " + i + "\t\tStarted at: {0} \t\tEnded at {1} \t\tLasted {2} seconds",
				ls.start.ToString("F2"), ls.end.ToString("F2"), ls.duration.ToString("F2"));
			i++;
		}
		bb.gameObject.SetActive (false);
		other.gameObject.SetActive (false);
	}
}
