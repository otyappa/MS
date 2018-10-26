using UnityEngine;
using System;
using System.Collections;

public class GlobalCoroutine : MonoBehaviour
{

    public static void Go(IEnumerator coroutine, String name = "")
    {
        GameObject obj = new GameObject();     // コルーチン実行用オブジェクト作成
        obj.name = "GlobalCoroutine_" + name;

        GlobalCoroutine component = obj.AddComponent<GlobalCoroutine>();
        if (component != null)
        {
            component.StartCoroutine(component.Do(coroutine));
        }
        else
        {
            Destroy(obj.gameObject);              // コルーチン実行用オブジェクトを破棄
        }
    }

    IEnumerator Do(IEnumerator src)
    {
        while (src.MoveNext())
        {               // コルーチンの終了を待つ
            yield return null;
        }

        Destroy(this.gameObject);              // コルーチン実行用オブジェクトを破棄
    }
}