using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L_Player : MonoBehaviour
{

    int key = 0;    // 押したキーの方向
    float runSpeed = 0.8f;  // 速度を掛けてあげる
    float runForce = 1.5f;  // 走っている間の速度
    float runThreshold = 2.2f;  // 速度制限
    float jumpForce = 70.0f;    // ジャンプ力
    float jumpThreshold = 1.0f;　// ジャンプ力制限

    string state;           // 状態
    bool isGround = true;   // 地面にいる状態判定
    float stateEffect = 1;  // 状態による横移動速度変更

    private Animator _anim; // プレイヤーアニメ
    private Rigidbody2D _rb; // 2D物理剛体

    // Use this for initialization
    void Start()
    {
        // プレイヤーから設定したコンポーネントを取得
        _anim = GetComponent<Animator>();

        _rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        GetInputKey();  // 入力情報取得
        ChangeState();  // プレイヤーの状態切り替え
        ChangeAnim();   // アニメ切り替え
        Move();         // 移動
    }

    /// <summary>
    /// 入力情報取得
    /// </summary>
    void GetInputKey()
    {
        // 入力していないときはキーを正位置に戻す
        key = 0;
        // 左キー移動
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            key = -1;
        }
        // 右キー移動
        if (Input.GetKey(KeyCode.RightArrow))
        {
            key = 1;
        }
    }

    /// <summary>
    /// プレイヤーの状態切り替え
    /// </summary>
    void ChangeState()
    {
        if (Mathf.Abs(_rb.velocity.y) > jumpThreshold)
        {
            isGround = false;
        }

        if (isGround)
        {
            // 左右キー入力があれば走る
            if (key != 0)
            {
                state = "RUN";
            }
            // 入力がなければ立ち
            else
            {
                state = "IDLE";
            }
        }
        else
        {
            // 上昇中
            if (_rb.velocity.y > 0)
            {
                state = "JUMP";
            }
            // 下降中
            else if (_rb.velocity.y < 0)
            {
                state = "FALL";
            }
        }

        // キーの入力方向と走っている状態で向きを変更する
        switch (state)
        {
            case "RUN":
                transform.localScale = new Vector3(key, 1, 1);
                break;
        }
    }

    /// <summary>
    /// アニメの切り替え
    /// </summary>
    void ChangeAnim()
    {
        // 特定のアニメを有効化してアニメを変化させる
        switch (state)
        {
            case "JUMP":
                stateEffect = 0.5f; // ジャンプ中は移動速度が遅くなる
                break;
            case "FALL":
                stateEffect = 0.5f; // ジャンプ中は移動速度が遅くなる
                break;
            case "RUN": // 移動中だったら
                _anim.SetBool("isRun", true);
                _anim.SetBool("isIdle", false);
                stateEffect = 1.0f; // 地面にいた際は移動速度が速くなる
                break;
            case "IDLE":    // 立ち状態
                _anim.SetBool("isIdle", true);
                _anim.SetBool("isRun", false);
                stateEffect = 1.0f; // 地面にいた際は移動速度が速くなる
                break;
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    void Move()
    {
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _rb.AddForce(transform.up * jumpForce);
                isGround = false;
            }
        }
        float speedX = Mathf.Abs(this._rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            _rb.AddForce(transform.right * key * runForce * stateEffect);
        }
        else
        {
            // Time.deltaTime = 一秒にかかる1フレームの時間(決まっていない)
            // 現在の速度 = 移動スピード * 1フレームの時間 * キー入力での方向
            transform.position += new Vector3(runSpeed * Time.deltaTime * key, 0.0f, 0.0f);
        }
    }


    // 着地判定
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            if (!isGround)
                isGround = true;
        }
    }
}
