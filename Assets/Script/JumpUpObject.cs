using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpUpObject : MonoBehaviour
{
    private float startHeight;
    private float hideHeight = 1.0f;

    void Start()
    {
        Hide();
    }
    /// <summary>
    /// ゲームオブジェクトを隠す
    /// </summary>
    private void Hide()
    {
        startHeight = transform.position.y;

        transform.position = new Vector3(transform.position.x,
            transform.position.y - hideHeight, transform.position.z);
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(col.gameObject.tag);

            HeadUp();
        }
    }
    /// <summary>
    ///   顔を出す
    /// </summary>
    private void HeadUp()
    {
        transform.DOMoveY(startHeight, 0.25f);
    }
}
