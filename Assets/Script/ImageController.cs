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
        StartCoroutine(HttpConnect());
    }

    IEnumerator HttpConnect()
    {
        string url = "https://joytas.net/php/man.jpg";
        UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(url);
        yield return uwr.SendWebRequest();
        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //ダウンロードされた画像をTexture型で取得
            Texture texture = DownloadHandlerTexture.GetContent(uwr);

            //textureからスプライトに変換
            //Sprite.Create(texture2D,texture2Dのどこを使うか,画像のpivotを指定)
            Sprite sp = Sprite.Create((Texture2D)texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            //Imageコンポーネント取得
            Image image = GetComponent<Image>();

            //取得した画像サイズをもとにImageコンポーネントの大きさ設定
            image.rectTransform.sizeDelta = new Vector2(
                texture.width, texture.height);

            //作成したスプライトを設定
            image.sprite = sp;

        }
    }
}