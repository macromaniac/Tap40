using UnityEngine;
using System.Collections;

public class PersistantGameData : MonoBehaviour {

	public Character p1Character;
	Player player1;
	void Start () {
	}
	
	void Update () {
	}

	private static PersistantGameData pgd = null;
	public static PersistantGameData Get() {
		if (pgd == null)
			pgd = GameObject.Find("PersistantGameData").GetComponent<PersistantGameData>();
		return pgd;
	}
}
