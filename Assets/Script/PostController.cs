using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PostController : MonoBehaviour
{

	void Start()
	{
		StartCoroutine(HttpConnect());
	}

	IEnumerator HttpConnect()
	{
		//ポスト通信でリクエストパラメーターを送りたい（リクエストに値を送れる）
		//ゲット通信でも送れるがurlにデータが出てしまう
		//WWWForm リクエストパラメーターを送りたいときはこれを使う
		WWWForm form = new WWWForm();
		//第一引数にキーの名前
		//第二引数に値　送れるのはint（少数を送りたいときは文字列で） か文字列　通信は最終的に文字列になる
		form.AddField("x", 5);
		form.AddField("y", 8);

		//
		string url = "https://joytas.net/php/calc.php";//ゲット通信ならurlの後ろに5&みたいな感じで送れるが
		//post通信だとWWWFormを使う
													   //UnityWebRequest uwr = UnityWebRequest.Post(url,form);
		UnityWebRequest uwr = UnityWebRequest.Post(url, form);
		//yield return uwr.SendWebRequest();
		yield return uwr.SendWebRequest();
		if (uwr.isHttpError || uwr.isNetworkError)
		{
			Debug.Log(uwr.error);
		}
		else
		{
			Debug.Log(uwr.downloadHandler.text);
		}
	}
}