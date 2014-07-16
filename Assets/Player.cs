using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum Character { Normal, Red }


public class Player : MonoBehaviour {
	public static int totalNumPlayers = 2;

	public int playerNumber;

	public Character character;
	private GameObject baseGFX = null;

	void Start() {
	}

	private GameObject target;

	private void loadResources() {
		if (playerNumber == 0) {
			this.character = PersistantGameData.Get().p1Character;
			baseGFX = Resources.Load<GameObject>(
				CharGFX.charGFXPrefix + character.ToString());
		}

		baseGFX = ResourceMan.MakeGFXFromCharacter(character);
		target = GameObject.Find(baseGFX.name + "/Target");
	}

	public GameObject GetInstantiableTarget() {
		if (baseGFX == null) {
			loadResources();
		}
		return target;
	}

	static private List<Player> playerList = null;
	public static Player Get(int playerNumber) {
		if (playerList == null) {
			playerList = new List<Player>();
			for(int i=0;i<totalNumPlayers;++i){
				string playerName = "Player" + ((int)i+1).ToString();
				Debug.Log(playerName);
				playerList.Add(GameObject.Find(playerName).GetComponent<Player>());
			}
		}
		return playerList[playerNumber];
	}
}
