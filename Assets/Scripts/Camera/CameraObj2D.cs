using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObj2D : MonoBehaviour
{
    public GameObject player;       //プレイヤーゲームオブジェクトへの参照を格納する Public 変数

    private Rigidbody2D _rb;    // 剛体

    // Use this for initialization
    void Start()
    {
        _rb = player.GetComponent<Rigidbody2D>();  // 剛体呼び出し
    }

    // Update is called once per frame
    void Update()
    {
        // カメラをプレイヤーのいる位置を視点として等速横移動する
         //transform.position = new Vector3(player.transform.position.x, 1, -10);
    }
}
