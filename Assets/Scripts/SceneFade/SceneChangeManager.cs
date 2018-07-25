using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour {

    // enum Mainでの定数を他のスクリプト・他のシーンに持っていくときに使う
    public static int MainMenu_Selected = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // シーンを遷移する
    public static void SceneChange(string NextScene)
    {
        SceneManager.LoadScene(NextScene, LoadSceneMode.Single);    // 次のシーンへ遷移
    }
}
