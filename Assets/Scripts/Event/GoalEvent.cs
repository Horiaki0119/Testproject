using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalEvent : MonoBehaviour {

    public static string NextScene;       // 次のシーン

    [SerializeField]
    private string sceneName = "Top";

    [SerializeField]
    private int stage;  // ステージ番号
	
	// Update is called once per frame
	void Update () {
        if (SceneFadeManager._fade_sequence == 1)
        {
            NextScene = sceneName;
            // フェードアウト開始
            //SceneFadeManager._fade_sequence = 2;
        }
        // 次のシーンへ遷移する
        if (SceneFadeManager._fade_sequence == 4)
        {
            // ステージを保存する
            LoadData.saveStage(stage);

            SceneChangeManager.SceneChange(NextScene);
            SceneFadeManager._fade_sequence = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // フェードインが終わっていたら
        if (SceneFadeManager._fade_sequence == 1)
        {
            // フェードアウト開始
            SceneFadeManager._fade_sequence = 2;
        }
    }
}
