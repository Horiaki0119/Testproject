using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonVelocityMove : MonoBehaviour {

    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject _Player;

    private Rigidbody2D _rb;    // 剛体

    // 移動速度
    private Vector3 moveSpeed;

    // Use this for initialization
    void Start()
    {
        moveSpeed = Vector3.zero;   // 移動速度初期化

        _rb = _Player.GetComponent<Rigidbody2D>();  // 剛体呼び出し
    }

    // Update is called once per frame
    void Update()
    {
        if (moveSpeed != Vector3.zero)
        {
            // プレイヤーの座標を編集できるように変数に設定
            Vector3 pos = _rb.velocity;

            pos.x = moveSpeed.x;        // 加速移動
            //pos = moveSpeed;     // 等速直線運動

            _rb.velocity = pos;   // 移動開始
        }else
        {
            Vector3 pos = _rb.velocity;

            moveSpeed.x = 0.0f;        // 加速移動
            pos.x = moveSpeed.x;     // 等速直線運動

            _rb.velocity = pos;   // 移動開始
        }
    }

    /// <summary>
    /// 左移動
    /// </summary>
    public void OnRightMove()
    {
        // プレイヤーの座標を編集できるように変数に設定
        Vector3 pos = _rb.velocity;

        pos.x = 0.1f;  // X軸方向に進める

        _rb.velocity = pos;   // 移動開始
    }

    /// <summary>
    /// 左加速度設定
    /// </summary>
    public void OnLeftMoveSpeed()
    {
        moveSpeed.x = -0.1f;    // 左に加速させる
    }

    /// <summary>
    /// 右加速度設定
    /// </summary>
    public void OnRightMoveSpeed()
    {
        moveSpeed.x = 0.1f;    // 右に加速させる
    }

    /// <summary>
    /// 速度停止
    /// </summary>
    public void OnMoveStop()
    {
        moveSpeed.x = 0.0f;     // 移動を止める
    }
}
