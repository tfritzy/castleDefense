using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour {

	void loadGame(){
		Application.LoadLevel ("Game");
	}
		

	void ResetGame(){
		GameControl.control.reset ();
	}

}
