using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ImageController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Unityでは通信はコルーチンを使う
        StartCoroutine(HttpConnect());
    }

    IEnumerator HttpConnect()
    {
        string url = "https://joytas.net/php/man.jpg";
        //getTexture画像取得用インスタンス　Requestリクエスト型
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();
        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //ダウンロードされた画像をTexture型で取得
            //通信したインスタンスを入れるとTexture型として取得できる
            Texture texture = DownloadHandlerTexture.GetContent(uwr);

            //textureからスプライトに変換
            //スプライトには中心の概念がある　ので　画像からスプライトに変える必要がある
            //Sprite.Create(texture2D,texture2Dのどこを使うか,画像のpivotを指定)
            //
            //第一引数　ダウンロードしてきたTexture　(Texture2D)textureダウンキャストするのは決まり
            //第二引数　使う範囲（１の画像のどの部分使うか）　今回全部　x,y,width,heigt
            //第三引数　作るスプライトの画像の中心を設定
            Sprite sp = Sprite.Create((Texture2D)texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            sp.name = "man";//インスペクターに付けた名前表示される

            //Imageコンポーネント取得
            Image image = GetComponent<Image>();

            //取得した画像サイズをもとにImageコンポーネントの大きさ設定
            //今回は元々の画像のサイズにしてる　
            //コンポーネントのレクトトランスフォームを設定すればそれをもとにサイズ変更してくれる
            //.sizeDeltaはコンポーネントのrectTransformの中のWidthとHeight項目
            //.positionだとnew Vector3(0,0,0);で設定で、コンポーネントのrectTransformの中のPos X,Pos Y,Pos Z項目
            image.rectTransform.sizeDelta = new Vector2(
                texture.width, texture.height);

            //作成したスプライトを設定
            image.sprite = sp;

        }
    }
}