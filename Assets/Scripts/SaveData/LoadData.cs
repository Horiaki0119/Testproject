using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadData : MonoBehaviour {

    // ステージデータ
    [SerializeField]
    public class StageData
    {
        public int stage;   // 現在のステージ

        // コンストラクタ
        // このクラスを生成した際に実行されるメソッド
        public StageData()
        {
            // 新規作成時の初期化値設定
            stage = 0;
        }
    }

    public bool DontDestroyEnabled = true;

    //============================================
    //  グローバル変数
    //============================================
    public static StageData _s_data;    // ステージデータ
    public static GameObject _loadManager;

    // Use this for initialization
    void Start () {
        // コンポーネントとして割り当てられた一回のみ入る
        // 一つのシーンで一度入った場合違うシーンでは入らなくなる
        if (!_loadManager)
        {
            _loadManager = gameObject;
            //=== セーブデータのロード ===//
            //=== ただしセーブデータがなければ新規で作成 ===// 
            _s_data = SaveData.GetClass<StageData>("s_Data", new StageData());
            //============================//


            if (DontDestroyEnabled)
            {
                // Sceneを遷移してもオブジェクトが消えないようにする
                DontDestroyOnLoad(this);
            }
        }
    }


    /// <summary>
    /// クリアしたステージの番号の保存
    /// </summary>
    /// <param name="Number">ステージ番号</param>
    public static void saveStage(int Number)
    {
        _s_data.stage = Number; // 番号設定

        //=== セーブデータのセーブ ===//
        // ステージデータをセーブデータに格納
        SaveData.SetClass<StageData>("s_Data", _s_data);
        
        SaveData.Save();    // 最終的にここで一度保存する
        //============================//
    }
}
