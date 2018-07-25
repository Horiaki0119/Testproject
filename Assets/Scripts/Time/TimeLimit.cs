using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour {

    [SerializeField]
    private Text _timeLimitText;    // 時間制限テキスト

    public float timeLimit;   // 残り時間

    [SerializeField]
    private Image time1Image;   // 時間1桁目イメージ
    [SerializeField]
    private Image time10Image;   // 時間2桁目イメージ

    [SerializeField]
    private Sprite[] NumberTex = new Sprite[10];    // 数値テクスチャ(0~9)

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeLimit > 0)
        {
            timeLimit -= Time.deltaTime;
        }else
        {
            // フェードインが終わっていたら
            if (SceneFadeManager._fade_sequence == 1)
            {
                // フェードアウト開始
                SceneFadeManager._fade_sequence = 2;
            }
        }
        if (!time1Image)
        {
            // 時間を取得する
            _timeLimitText.text = timeLimit.ToString("N0") + "秒";
        }
        else
        {
            // 時間を取得する
            _timeLimitText.text = "秒";
            // 時間イメージの切り替え
            time1Image.GetComponent<Image>().sprite = NumberTex[(int)timeLimit % 10];
            time10Image.GetComponent<Image>().sprite = NumberTex[(int)timeLimit / 10];
        }
    }
}
