using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class ButtonController : MonoBehaviour
{
    public InputField et1;
    public InputField et2;
    public Text result;

    //押したときの処理
    //ボタンオブジェクトはボタンコンポーネントを持っていてそこにオンクリック()メッソドあるのでそこに
    //ボタン押された時に実行したいスクリプトをもってるオブジェクトを設定する
    //だいたいボタンオブジェクト自身が持っているので自身を登録することが多い
    public void btClick()
    {
        //et.textでアクセスできる
        string x = et1.text;
        string y = et2.text;
        StartCoroutine(HttpConnect(x, y));
    }

    //文字列が入ってくる
    IEnumerator HttpConnect(string x,string y)
    {
        WWWForm form = new WWWForm();
        form.AddField("x", x);
        form.AddField("y", y);
        string url = "https://joytas.net/php/calc.php";
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();
        if(uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.Log(uwr.error);
        }
        else
        {
            //通信した結果が.downloadHandler.textに入ってくる（文字列）
            result.text = uwr.downloadHandler.text;
        }
    }
}
