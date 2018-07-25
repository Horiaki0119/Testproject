using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 2Dのプレイヤーコントロールクラス
/// </summary>
public class Player2D : MonoBehaviour {

    // オブジェクトに付随するコンポーネント
    Rigidbody2D rb;             // 物理剛体(物理空間でどんな動きをするか(重力、摩擦、反射)
    Animator animator;          // オブジェクトのアニメーション管理

    float jumpForce = 70f;       // ジャンプ時に加える力
    float jumpThreshold = 1.0f;  // ジャンプ中か判定するための閾値
    float runForce = 1.5f;       // 走り始めに加える力
    float runSpeed = 0.5f;       // 走っている間の速度
    float runThreshold = 2.2f;   // 速度切り替え判定のための閾値
    float XSize;                 // localScaleを変更した際の保存先
    bool isGround = true;        // 地面と接地しているか管理するフラグ
    int key = 0;                 // 左右の入力管理


    string state;                // プレイヤーの状態管理
    string prevState;            // 前の状態を保存
    float stateEffect = 1;       // 状態に応じて横移動速度を変えるための係数

    public Text DebugText;       // 状態を確認するためのデバッグ文字

    float sensitivity = 0.1f;   // マウスの移動感度

    /// <summary>
    /// 一度目のオブジェクトのアクティブ時だけに処理
    /// </summary>
    void Start()
    {
        // コンポーネントの取得
        this.rb = GetComponent<Rigidbody2D>();
        this.animator = GetComponent<Animator>();
        XSize = this.transform.localScale.x;
    }

    /// <summary>
    /// アクティブ時での更新処理
    /// </summary>
    void Update()
    {
        GetInputKey();          // ① 入力を取得
        ChangeState();          // ② 状態を変更する
        ChangeAnimation();      // ③ 状態に応じてアニメーションを変更する
        Move();                 // ④ 入力に応じて移動する
    }

    /// <summary>
    ///  入力管理
    /// </summary>
    void GetInputKey()
    {
        key = 0;
        // 右方向入力
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))// || Input.GetAxis("Horizontal") > 0)
        {
            key = 1;
        }
        // 左方向入力
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))// || Input.GetAxis("Horizontal") < 0)
        {
            key = -1;
        }
        // マウス左クリック
        if (Input.GetMouseButton(0))
        {
            key = -1;
        }
        // マウス右クリック
        if (Input.GetMouseButton(1))
        {
            key = 1;
        }
       
    }

    /// <summary>
    /// 
    /// </summary>
    void ChangeState()
    {
        // 空中にいるかどうかの判定。上下の速度(rigidbody.velocity)が一定の値を超えている場合、空中とみなす
        if (Mathf.Abs(rb.velocity.y) > jumpThreshold)
        {
            isGround = false;
        }

        // 接地している場合
        if (isGround)
        {
            // 走行中
            if (key != 0)
            {
                state = "RUN";
                //待機状態
            }
            else
            {
                state = "IDLE";
            }
            // 空中にいる場合
        }
        else
        {
            // 上昇中
            if (rb.velocity.y > 0)
            {
                state = "JUMP";
                // 下降中
            }
            else if (rb.velocity.y < 0)
            {
                state = "FALL";
            }
        }
    }

    void ChangeAnimation()
    {
        // 状態が変わった場合のみアニメーションを変更する
        // Animatorで設定されているアニメのフラグを切り替えてアニメの再生を管理する
        if (prevState != state)
        {
            switch (state)
            {
                case "JUMP":
                    animator.SetBool("isJump", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isRun", false);
                    animator.SetBool("isIdle", false);
                    stateEffect = 0.5f;
                    break;
                case "FALL":
                    animator.SetBool("isFall", true);
                    animator.SetBool("isJump", false);
                    animator.SetBool("isRun", false);
                    animator.SetBool("isIdle", false);
                    stateEffect = 0.5f;
                    break;
                case "RUN":
                    animator.SetBool("isRun", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isJump", false);
                    animator.SetBool("isIdle", false);
                    stateEffect = 1f;
                    //GetComponent<SpriteRenderer> ().flipX = true;
                    break;
                default:    // ジャンプ、落下、走るアニメ以外の状態は立ち(アイドル)状態にする
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isFall", false);
                    animator.SetBool("isRun", false);
                    animator.SetBool("isJump", false);
                    stateEffect = 1f;
                    break;
            }
            // 状態の変更を判定するために状態を保存しておく
            prevState = state;
        }
        switch (state)
        {
            case "RUN":
                transform.localScale = new Vector3(key, 1, 1); // 向きに応じてキャラクターのspriteを反転
                //DebugText.text = transform.localScale.x.ToString();
                break;
        }
    }

    void Move()
    {
        // 設置している時にSpaceキー押下でジャンプ
        if (isGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))// || Input.GetButtonDown("Jump"))
            {
                this.rb.AddForce(transform.up * this.jumpForce);
                isGround = false;
            }
            
            // マウスの移動量を取得
            float mouse_move_x = Input.GetAxis("Mouse X") * sensitivity;
            float mouse_move_y = Input.GetAxis("Mouse Y") * sensitivity;

            // 上方向にマウスをフリックしたら
            if (mouse_move_y > 0.1f)
            {
                this.rb.AddForce(transform.up * this.jumpForce);
                isGround = false;
            }
        }

        // 左右の移動。一定の速度に達するまではAddforceで力を加え、それ以降はtransform.positionを直接書き換えて同一速度で移動する
        float speedX = Mathf.Abs(this.rb.velocity.x);
        if (speedX < this.runThreshold)
        {
            this.rb.AddForce(transform.right * key * this.runForce * stateEffect); //未入力の場合は key の値が0になるため移動しない
        }
        else
        {
            this.transform.position += new Vector3(runSpeed * Time.deltaTime * key * stateEffect, 0, 0);
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
        // 衝突した面の向いている方向が上向きなら床として判定
        else if( col.contacts[0].normal.y >= 1.0f)
        {
            if (!isGround)
                isGround = true;
        }
    }

    // 着地中判定
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Floor")
        {
            if (!isGround)
                isGround = true;
        }
        // 衝突した面の向いている方向が上向きなら床として判定
        else if (col.contacts[0].normal.y >= 1.0f)
        {
            if (!isGround)
                isGround = true;
        }
    }
}
