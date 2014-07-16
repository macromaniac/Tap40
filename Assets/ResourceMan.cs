using System;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMan {

	public static GameObject MakeGFXFromCharacter(Character character) {
		string charString = CharGFX.charGFXPrefix + character.ToString();
		Debug.Log(charString);
		return (GameObject)GameObject.Instantiate(Resources.Load(charString),
			new Vector3(100f,100f,0f), Quaternion.identity);
	}

}
