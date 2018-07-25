using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMove : MonoBehaviour {

    // プレイヤーオブジェクト
    [SerializeField]
    private GameObject _Player;

    // 移動速度
    private Vector3 moveSpeed;

	// Use this for initialization
	void Start () {
        moveSpeed = Vector3.zero;   // 移動速度初期化
    }
	
	// Update is called once per frame
	void Update () {
		if(moveSpeed != Vector3.zero)
        {
            // プレイヤーの座標を編集できるように変数に設定
            Vector3 pos = _Player.transform.position;

            pos += moveSpeed;

            _Player.transform.position = pos;   // 移動開始
        }
	}

    /// <summary>
    /// 左移動
    /// </summary>
    public void OnRightMove()
    {
        // プレイヤーの座標を編集できるように変数に設定
        Vector3 pos = _Player.transform.position;

        pos.x += 0.1f;  // X軸方向に進める

        _Player.transform.position = pos;   // 移動開始
    }

    /// <summary>
    /// 右加速度設定
    /// </summary>
    public void OnRightMoveSpeed()
    {
        moveSpeed.x += 0.1f;
    }

    /// <summary>
    /// 速度停止設定
    /// </summary>
    public void OnMoveStop()
    {
        moveSpeed.x = 0.0f;
    }
}
