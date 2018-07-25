using UnityEngine;
using System;

public class CameraObj : MonoBehaviour {

    public GameObject player;       //プレイヤーゲームオブジェクトへの参照を格納する Public 変数

    [SerializeField] private float distance = 4.0f; // プレイヤーとの距離
    [SerializeField] private float polarAngle = 0.0f; // 縦方向のアングル
    [SerializeField] private float azimuthalAngle = 0.0f; // 横方向のアングル

    // Use this for initialization
    void Start () {
    }

    // 各フレームで、Update の後に LateUpdate が呼び出されます。
    void LateUpdate()
    {
        // カメラ視点座標
        var lookAtPos = player.transform.position;
        lookAtPos.y += 1.0f;    // カメラオフセットY軸位置
        // カメラ座標更新
        updatePosition(lookAtPos);
        // カメラ視点座標更新
        transform.LookAt(lookAtPos);
    }

    // カメラ座標更新
    void updatePosition(Vector3 lookAtPos)
    {
        var da = azimuthalAngle * Mathf.Deg2Rad;　// 横軸のアングルの角度をラジアンに変換
        var dp = polarAngle * Mathf.Deg2Rad;      // 縦軸のアングルの角度をラジアンに変換
        // カメラの注視点
        // Mathf.Sin(0*Mathf.Deg2Rad)       0
        // Mathf.Sin(90*Mathf.Deg2Rad)      1
        // Mathf.Sin(180*Mathf.Deg2Rad)     0
        // Mathf.Sin(270*Mathf.Deg2Rad)     -1
        // Mathf.Cos(0*Mathf.Deg2Rad)       1
        // Mathf.Cos(90*Mathf.Deg2Rad)      0
        // Mathf.Cos(180*Mathf.Deg2Rad)     -1
        // Mathf.Cos(270*Mathf.Deg2Rad)     0
        // 横軸(da) 270 縦軸(dp) 90　の場合
        // x : プレイヤー座標 + 距離 * 1 * 0 = プレイヤー位置
        // y : プレイヤー座標 + 距離 * 0 = プレイヤー位置
        // z : プレイヤー座標 + 距離 * 1 * -1 = プレイヤー位置 - 距離
        transform.position = new Vector3(
            lookAtPos.x + distance * Mathf.Sin(dp) * Mathf.Cos(da),
            lookAtPos.y + distance * Mathf.Cos(dp),
            lookAtPos.z + distance * Mathf.Sin(dp) * Mathf.Sin(da));
    }
}
