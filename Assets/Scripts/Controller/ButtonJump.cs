using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonJump : MonoBehaviour {

    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject _Player;

    private Rigidbody2D _rb;    // 剛体

    // 移動速度
    public float jumpPower;

    public static bool landFlg;   // 着地フラグ

    // Use this for initialization
    void Start () {
        // 初期化
        jumpPower = 0.0f;
        _rb = _Player.GetComponent<Rigidbody2D>();
        landFlg = false;
    }
	
	// Update is called once per frame
	void Update () {
        // ジャンプ力が0以下になるまで重力をかける
        if (jumpPower > 0.0f)
        {
            jumpPower -= 4.0f;
        }

        // ジャンプ
        if (jumpPower != 0.0f)
        {
            // プレイヤーの座標を編集できるように変数に設定
            Vector3 pos = _rb.velocity;

            pos.y+= jumpPower;     // ジャンプ速度を付ける

            _rb.velocity = pos;   // 移動開始
        }
    }

    /// <summary>
    /// 左加速度設定
    /// </summary>
    public void OnJump()
    {
        // 着地状態だったらジャンプボタンを押したらジャンプさせる
        if (landFlg)
        {
            landFlg = false;        // ジャンプ中に遷移
            jumpPower = 30.0f;    // ジャンプ力を設定
        }
    }
}
