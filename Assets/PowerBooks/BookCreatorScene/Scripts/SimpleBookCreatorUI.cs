using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
using TLGFPowerBooks;

public class SimpleBookCreatorUI : MonoBehaviour {

	public SimpleBookCreator sbc;
	public GameObject loadingBar;
	public Image loadingBarPercent;
	public GameObject controlPanel;
	private bool loadingComplete;


	void Update () {
		loadingBarPercent.fillAmount = sbc.GetPercentComplete () / 100f;
		if (sbc.GetBookState () == SimpleBookCreator.BookState.OPEN && !loadingComplete) {
			loadingComplete = true;
			ShowControlUI ();
		}

		if (Input.GetAxis ("Horizontal") > 0) {
			sbc.NextPage ();
		}

		if (Input.GetAxis ("Horizontal") < 0) {
			sbc.PrevPage();
		}
	}

	private void ShowControlUI () {
		loadingBar.SetActive (false);
		controlPanel.SetActive (true);
	}

	public void CreateBookPrefab () {
		#if UNITY_EDITOR
		string prefabName = sbc.prefabName;
		if (prefabName != "") {
			GameObject go = sbc.CreateTextBookContent ();
			PrefabUtility.CreatePrefab ("Assets/PowerBooks/BookCreatorScene/SavedBookContent/" + prefabName + ".prefab", go);
			sbc.PrefabCreatedMessage ();
		} else {
			sbc.PrefabNotCreatedMessage();
		}
		#endif
	}
}
