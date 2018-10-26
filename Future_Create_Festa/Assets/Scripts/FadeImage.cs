using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeImage : MonoBehaviour
{

    // フェード処理状態
    enum STATUS
    {
        LOOK = 0,       // 表示中
        INVISIBLE,      // 非表示中
        FADEIN,         // フェードイン中
        FADEOUT,        // フェードアウト中
    }

    private float LookTime;          // クリアイメージが見える時間
    private float InvisibleTime;     // クリアイメージが見えない時間
    private float FadeTime;          // フェードにかかる時間

    private STATUS status;          // ステータス
    private bool TimeGetFlg;        // 現在の時刻のリセット用
    private float NowTime;          // 現在の時間(alpha計算用)
    private float alpha;            // alpha計算用

    private Renderer renderer;
    private SpriteRenderer spRenderer;

    private bool isFadingIn;
    private bool isFadingOut;
    private bool isFading = false;

    public void Init(Renderer rend, float looktime, float invisibletime, float fadetime)
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = rend;

        // 各タイムの初期化
        LookTime = looktime;
        InvisibleTime = invisibletime;
        FadeTime = fadetime;
    }

    public void Init()
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = this.GetComponent<Renderer>();
    }

    public void Init(Renderer rend)
    {
        // 変数初期化
        status = STATUS.INVISIBLE;
        TimeGetFlg = false;
        NowTime = 0.0f;
        alpha = 0.0f;

        // 変更するオブジェクトのレンダラをセット
        renderer = rend;
    }

    // マテリアルのアルファ値セット
    public void SetMaterialAlpha(float alpha)
    {
        renderer.materials[0].color = new Color(1, 1, 1, alpha);
    }

    // フェード処理
    public void Fade()
    {
        switch (status)
        {
            case STATUS.LOOK:       // クリアイメージ表示中

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    SetMaterialAlpha(1);
                }

                // 表示時間超過したらフェードアウト開始
                if (NowTime > LookTime)
                {
                    status = STATUS.FADEOUT;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;
                }

                break;

            case STATUS.INVISIBLE:  // ステージイメージ表示中

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    SetMaterialAlpha(0);
                }

                // 非表示時間超過したらフェードイン開始
                if (NowTime > InvisibleTime)
                {
                    status = STATUS.FADEIN;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;
                }

                break;

            case STATUS.FADEIN:     // フェードイン

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 0.0f;
                    SetMaterialAlpha(alpha);
                }

                // フェードイン終了
                if (NowTime > FadeTime)
                {
                    status = STATUS.LOOK;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    // 現在の時間からアルファ値の割合計算
                    alpha = NowTime / FadeTime;
                    SetMaterialAlpha(alpha);
                }

                break;

            case STATUS.FADEOUT:    // フェードアウト

                // タイムの初期化
                if (!TimeGetFlg)
                {
                    TimeGetFlg = true;
                    NowTime = 0;
                    alpha = 1.0f;
                    SetMaterialAlpha(alpha);
                }

                // フェードアウト終了
                if (NowTime > FadeTime)
                {
                    status = STATUS.INVISIBLE;
                    TimeGetFlg = false;
                }
                else
                {
                    NowTime += Time.deltaTime;

                    // 現在の時間からアルファ値の割合計算
                    alpha = 1.0f - NowTime / FadeTime;
                    SetMaterialAlpha(alpha);
                }

                break;

        }
    }

    // フェードイン
    public IEnumerator MaterialFadeIn(Renderer rend, float fadeTime)
    {
        // 排他制御
        if (isFadingIn)
        {
            yield break;
        }

        isFadingIn = true;
        rend.materials[0].color = new Color(1, 1, 1, 0);

        float nowTime = 0.0f;
        float a = 0;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = nowTime / fadeTime;
            rend.materials[0].color = new Color(1, 1, 1, a);

            yield return true;

        }

        rend.materials[0].color = new Color(1, 1, 1, 1);

        isFadingIn = false;

    }

    public IEnumerator SpriteFadeIn(SpriteRenderer spRend, float fadeTime)
    {
        // 排他制御
        if (isFadingIn)
        {
            yield break;
        }

        isFadingIn = true;
        spRend.color = new Color(1, 1, 1, 0);

        float nowTime = 0.0f;
        float a = 0;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = nowTime / fadeTime;
            spRend.color = new Color(1, 1, 1, a);

            yield return true;

        }

        spRend.color = new Color(1, 1, 1, 1);

        isFadingIn = false;

    }

    public IEnumerator SpriteFadeIn(float fadeTime)
    {
        // 排他制御
        if (isFadingIn)
        {
            yield break;
        }

        isFadingIn = true;
        spRenderer.color = new Color(1, 1, 1, 0);

        float nowTime = 0.0f;
        float a = 0;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = nowTime / fadeTime;
            spRenderer.color = new Color(1, 1, 1, a);

            yield return true;

        }

        spRenderer.color = new Color(1, 1, 1, 1);

        isFadingIn = false;

    }


    // フェードアウト
    public IEnumerator MaterialFadeOut(Renderer rend, float fadeTime)
    {
        // 排他制御
        if (isFadingOut)
        {
            yield break;
        }

        isFadingOut = true;
        rend.materials[0].color = new Color(1, 1, 1, 1);

        float nowTime = 0.0f;
        float a = 1;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = 1.0f - nowTime / fadeTime;
            rend.materials[0].color = new Color(1, 1, 1, a);

            yield return true;

        }

        rend.materials[0].color = new Color(1, 1, 1, 0);

        isFadingOut = false;
    }

    public IEnumerator SpriteFadeOut(SpriteRenderer spRend, float fadeTime)
    {
        // 排他制御
        if (isFadingOut)
        {
            yield break;
        }

        isFadingOut = true;
        spRend.color = new Color(1, 1, 1, 1);

        float nowTime = 0.0f;
        float a = 1;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = 1.0f - nowTime / fadeTime;
            spRend.color = new Color(1, 1, 1, a);

            yield return true;

        }

        spRend.color = new Color(1, 1, 1, 0);

        isFadingOut = false;
    }

    public IEnumerator SpriteFadeOut(float fadeTime)
    {
        // 排他制御
        if (isFadingOut)
        {
            yield break;
        }

        isFadingOut = true;
        spRenderer.color = new Color(1, 1, 1, 1);

        float nowTime = 0.0f;
        float a = 1;

        while (nowTime < fadeTime)
        {
            nowTime += Time.deltaTime;

            // 現在の時間からアルファ値の割合計算
            a = 1.0f - nowTime / fadeTime;
            spRenderer.color = new Color(1, 1, 1, a);

            yield return true;

        }

        spRenderer.color = new Color(1, 1, 1, 0);

        isFadingOut = false;
    }


    public IEnumerator FadeInToOutChangeSprite(SpriteRenderer spRend, Sprite changeSp, float fadeTime)
    {
        if (isFading)
        {
            yield break;
        }

        isFading = true;


        yield return StartCoroutine(SpriteFadeIn(spRend, fadeTime));

        spRend.sprite = changeSp;

        yield return StartCoroutine(SpriteFadeOut(spRend, fadeTime));

        isFading = false;
    }

    public bool GetIsFading()
    {
        return isFading;
    }

    public bool GetIsFadingIn()
    {
        return isFadingIn;
    }

    public bool GetIsFadingOut()
    {
        return isFadingOut;
    }

    public void SetSpriteRenderer(SpriteRenderer spRend)
    {
        spRenderer = spRend;
    }

    public void SetSprite(Sprite sp)
    {
        spRenderer.sprite = sp;
    }
}
