using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//============= プレイヤー管理クラス ============================//
public class Player : MonoBehaviour
{
	//==========================================================//
	//	定数定義
	//==========================================================//
	public float MOVE_SPEED = 5.0f;		// 移動速度
	public float PLANE_VECTOR = 1.0f;	// 面の向きベクトル
	public float GRAVITY_SCALE = 0.15f;	// 重力加速度
	public float BLOCK_GRAVITY = 1.0f;	// 壁に衝突時の追加重力
	
	//==========================================================//
	//	変数定義
	//==========================================================//
    Animator _anim; 		// スプライトアニメ
    SpriteRenderer _spRend; // スプライト描画管理
    Rigidbody _rb;			// 剛体管理
    Vector3 _moveSpeed;		// 移動速度

    bool _JumpFlg = false;	// ジャンプ中フラグ(真ならジャンプ中、それ以外は偽
    bool _FloorFlg = false;	// 床判定フラグ(真なら床に設置状態、それ以外は偽

    Vector3 blockNormal;    // 衝突したブロックの面の方向
    Vector3 wallNormal;     // 衝突した壁の面の方向


	//==========================================================//
	//	関数定義
	//==========================================================//
    // スクリプトを定義したオブジェクトがアクティブになった時に一度だけ通る処理
    // アクティブ(通る)→非アクティブ(通らない)→アクティブ(通らない)
    void Start()
    {
        // 変数の初期化
        _moveSpeed = new Vector3(0.0f, 0.0f, 0.0f);
        blockNormal = new Vector3(0.0f, 0.0f, 0.0f);
        wallNormal = new Vector3(0.0f, 0.0f, 0.0f);
        // スクリプトが設定されているオブジェクトから
        // コンポーネントの取得
        _anim = GetComponent<Animator>();
        _spRend = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody>();
    }

    // スクリプトを定義したオブジェクトがアクティブ時にループして処理する
    // 更新処理
    void Update()
    {
        // 左キーが押された時
        if (Input.GetKey(KeyCode.LeftArrow))
        {
        	// 歩きアニメ、ジャンプアニメ中ではない場合
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
                !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // 歩きアニメ再生
                _anim.Play("Walk", 0, 0.0f);
            }
            // 上下キーを押していない場合はその方向の加速度は0にする
            if (Input.GetKey(KeyCode.UpArrow) == false &&
                Input.GetKey(KeyCode.DownArrow) == false)
            {
                _moveSpeed.z = 0.0f;
            }

            // 壁に衝突していない状態
            if (blockNormal.x < PLANE_VECTOR && wallNormal.x < PLANE_VECTOR)
            {
                _moveSpeed.x = -MOVE_SPEED;	// 左方向に加速
            }
            // 衝突している壁の向きは右
            else
            {
                _moveSpeed.x = 0.0f;	// 移動速度は0にして衝突した方向には進めない状態にする
            }
            _spRend.flipX = true;  // 向きを左向きに

        }
        // 右キーが押された時
        if (Input.GetKey(KeyCode.RightArrow))
        {
        	// 歩きアニメ、ジャンプアニメ中ではない場合
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
                !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // 歩きアニメ再生
                _anim.Play("Walk", 0, 0.0f);
            }
            // 上下キーを押していない場合はその方向の加速度は0にする
            if (Input.GetKey(KeyCode.UpArrow) == false &&
                Input.GetKey(KeyCode.DownArrow) == false)
            {
                _moveSpeed.z = 0.0f;
            }
            // 壁に衝突していない状態
            if (blockNormal.x > -PLANE_VECTOR && wallNormal.x > -PLANE_VECTOR)
            {
                _moveSpeed.x = +MOVE_SPEED;	// 右方向に加速
            }
            // 衝突している壁の向きは左
            else
            {
                _moveSpeed.x = 0.0f;	// 移動速度は0にして衝突した方向には進めない状態にする
            }
            _spRend.flipX = false; // 向きを右向きに
        }
        // 上キーが押された時
        if (Input.GetKey(KeyCode.UpArrow))
        {
        	// 歩きアニメ、ジャンプアニメ中ではない場合
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
                !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // 歩きアニメ再生
                _anim.Play("Walk", 0, 0.0f);
            }
            // 左右キーを押していない場合はその方向の加速度は0にする
            if (Input.GetKey(KeyCode.LeftArrow) == false &&
                Input.GetKey(KeyCode.RightArrow) == false)
            {
                _moveSpeed.x = 0.0f;
            }
            // 衝突している壁の向きは手前
            if (blockNormal.z > -PLANE_VECTOR && wallNormal.z > -PLANE_VECTOR)
            {
            	_moveSpeed.z = +MOVE_SPEED;	// 奥方向に加速
            }
            else
          	{
          		_moveSpeed.z = 0.0f;	// 移動速度は0にして衝突した方向には進めない状態にする
          	}
        }
        // 下キーが押された時
        if (Input.GetKey(KeyCode.DownArrow))
        {
        	// 歩きアニメ、ジャンプアニメ中ではない場合
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Walk") &&
                !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // 歩きアニメ再生
                _anim.Play("Walk", 0, 0.0f);
            }
            // 左右キーを押していない場合はその方向の加速度は0にする
            if (Input.GetKey(KeyCode.LeftArrow) == false &&
                Input.GetKey(KeyCode.RightArrow) == false)
            {
                _moveSpeed.x = 0.0f;
            }
            // 衝突している壁の向きは奥
            if (blockNormal.z < +PLANE_VECTOR && wallNormal.z < +PLANE_VECTOR)
            {
            	_moveSpeed.z = -MOVE_SPEED;	// 手前方向に加速
            }
            else
          	{
            	_moveSpeed.z = 0.0f;	// 移動速度は0にして衝突した方向には進めない状態にする
        	}
        }

        // スペースキーが押された時
        if (Input.GetKeyDown(KeyCode.Space))
        {
        	// ジャンプアニメ中ではない場合
        	// 着地するまで一度以上のジャンプはできない状態にする
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                // ジャンプアニメ再生
                _anim.Play("Jump", 0, 0.0f);
                // ジャンプ状態でなければ
                // 上方向に加速度を付けてジャンプさせる
                if (!_JumpFlg)
                {
                    _moveSpeed.y = MOVE_SPEED;	// ジャンプで上方向に加速
                    _JumpFlg = true;		// ジャンプ中にする
                    _FloorFlg = false;		// 床にいる状態を解除
                }
            }
            // ここにジャンプの加速度処理を移行すると
            // ジャンプ直後は床判定が残っているため
            // _JumpFlgが解除され二段ジャンプができるようになる
        }

		// 左右奥手前に移動していなければ
		// 立ち状態に遷移
        if (!Input.GetKey(KeyCode.LeftArrow) == true &&
            !Input.GetKey(KeyCode.RightArrow) == true &&
            !Input.GetKey(KeyCode.UpArrow) == true &&
            !Input.GetKey(KeyCode.DownArrow) == true)
        {
        	// 立ち状態でなく、ジャンプ中状態でなければ
        	// 立ち状態アニメに変更
            if (!_anim.GetCurrentAnimatorStateInfo(0).IsName("Stand") &&
                !_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                _anim.Play("Stand", 0, 0.0f);
            }
            // 移動中の場合止める
            _moveSpeed.x = 0.0f;
            _moveSpeed.z = 0.0f;
        }
        //if (_FloorFlg == false)
        //{
        	// 重力を付ける
            _moveSpeed.y -= GRAVITY_SCALE;
        //}
        // 加速度に代入するため座標の取得
        Vector3 vel = _rb.velocity;

		// 速度を今現在の加速度に設定する
		// 最終的な移動量は必ず処理の最後に設定する
        vel.x = _moveSpeed.x;
        vel.z = _moveSpeed.z;

		// 床にいない状態ならば落下させる
        if (_FloorFlg == false)
        {
            vel.y = _moveSpeed.y;	// 重力を付ける
        }
        _rb.velocity = vel;	// 移動量を加速度に設定する
    }

	/// <summary>
	/// 他のオブジェクトとの衝突開始時に処理する
	/// <summary>
	/// <param name="Collision">衝突したオブジェクトの情報</param>
    private void OnCollisionEnter(Collision collision)
    {
    	// コリジョンから衝突中の面の向きを取得
        var normal = collision.contacts[0].normal;
		// ゲームオブジェクトからTagを取得する場合は
		// gameObject.tagはスクリプト側で決めたタグ名になるので
		// UnityEditor側で決めているタグと一致しているか確認するメソッドCompareTagを呼び出して確認する
		//=========================================
		// 衝突しているのが床の場合は着地状態になる
        if (collision.gameObject.CompareTag("Floor"))
        {
        	// ジャンプ中の場合は立ち状態に変更
            if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
            {
                _anim.Play("Stand", 0, 0.0f);	// 立ちアニメを再生
            }
        }
        // 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Block"))
        {
            _moveSpeed.y -= BLOCK_GRAVITY;	// 重力を増加
            blockNormal = normal;           // 壁の向きを取得する
            // 箱の場合床判定を取るには衝突した面が上向きの場合床と判定する
            if (blockNormal.y >= PLANE_VECTOR)
            {
            	// ジャンプ中の場合は立ち状態に変更
                if (_anim.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
                {
                    _anim.Play("Stand", 0, 0.0f);	// 立ちアニメを再生
                }
            }
        }
        // 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Wall"))
        {
            _moveSpeed.y -= BLOCK_GRAVITY;	// 重力を増加
            wallNormal = normal;           // 壁の向きを取得する
        }
    }

    /// <summary>
    /// 他のオブジェクトとの衝突中ならば処理する
    /// <summary>
    /// <param name="Collision">衝突したオブジェクトの情報</param>
    private void OnCollisionStay(Collision collision)
    {
        // コリジョンから衝突中の面の向きを取得
        var normal = collision.contacts[0].normal;

        // 衝突しているのが床の場合は着地状態になる
        if (collision.gameObject.CompareTag("Floor"))
        {
            _JumpFlg = false;		// ジャンプ中を解除する
            _moveSpeed.y = 0.0f;	// 着地していたらジャンプ中の加速度を0にする
            _FloorFlg = true;		// 着地状態にする
        }
        // 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Block"))
        {
            blockNormal = normal;	// 壁の向きを取得する
            // 箱の場合床判定を取るには衝突した面が上向きの場合床と判定する
            if (blockNormal.y >= PLANE_VECTOR)
            {
                _JumpFlg = false;	// ジャンプ中を解除する
                _moveSpeed.y = 0.0f;// 着地していたらジャンプ中の加速度を0にする
                _FloorFlg = true;	// 着地状態にする
            }
        }
        // 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Wall"))
        {
            wallNormal = normal;           // 壁の向きを取得する
        }
    }

	/// <summary>
	/// 他のオブジェクトとの衝突中ならば処理する
	/// <summary>
	/// <param name="Collision">衝突したオブジェクトの情報</param>
    private void OnCollisionExit(Collision collision)
    {
    	// 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Block"))
        {
        	// 箱との衝突していない状態
        	// 面の向きをクリアしておく
            blockNormal.x = 0.0f;
            blockNormal.z = 0.0f;
        }
        // 衝突しているのが壁の場合は壁衝突状態になる
        if (collision.gameObject.CompareTag("Wall"))
        {
            // 箱との衝突していない状態
            // 面の向きをクリアしておく
            wallNormal.x = 0.0f;
            wallNormal.z = 0.0f;
        }
    }
}