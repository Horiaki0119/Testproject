using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

//====================================
//  定数
//====================================
//=== 列挙型
// ステージ番号
enum STAGE_NUMBER
{
    STAGE_1 = 0x00,     // ステージ１
    STAGE_2,            // ステージ２
    STAGE_MAX
}

// 列挙型に文字列を関連付けする拡張メソッドクラス
// ステージ番号にシーンの名前を関連付ける
static class StageExt
{
    // ステージ番号によるシーン名の取得
    public static string StageSceneName(this STAGE_NUMBER number)
    {
        // シーン名定義
        string[] names =
        {
                "1Stage",
                "2Stage"
        };
        return names[(int)number];  // シーン名を返す
    }
}


public class GotoNextScene : MonoBehaviour
{
    public static string NextScene;       // 次のシーン

    [SerializeField]
    private string sceneName = "Top";


    // Update is called once per frame
    void Update()
    {
        if( SceneFadeManager._fade_sequence == 1)
        {
            NextScene = sceneName;
            // フェードアウト開始
            //SceneFadeManager._fade_sequence = 2;
        }
        // 次のシーンへ遷移する
        if (SceneFadeManager._fade_sequence == 4)
        {
            SceneChangeManager.SceneChange(NextScene);
            SceneFadeManager._fade_sequence = 5;
        }
    }

    // タッチしたらフェードアウトを呼ぶ
    public void onTouchGotoFadeOut(string Next)
    {
        // フェードインが終わっていたら
        if (SceneFadeManager._fade_sequence == 1)
        {
            // 次のシーンへ
            NextScene = Next;
            // フェードアウト開始
            SceneFadeManager._fade_sequence = 2;
        }
    }

    // タッチしたらフェードアウトを呼ぶ
    public void onNextStageFadeOut()
    {
        // フェードインが終わっていたら
        if (SceneFadeManager._fade_sequence == 1)
        {
            //== セーブデータからステージを呼び出す
            STAGE_NUMBER Next = (STAGE_NUMBER)LoadData._s_data.stage;

            // 次のシーンへ
            NextScene = StageExt.StageSceneName(Next);
            //=====================================

            // フェードアウト開始
            SceneFadeManager._fade_sequence = 2;
        }
    }
}