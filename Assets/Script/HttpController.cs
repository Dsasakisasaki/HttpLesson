using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpController : MonoBehaviour
{

	void Start()
	{
		StartCoroutine(HttpConnect());
	}
	IEnumerator HttpConnect()
	{
		string url = "https://joytas.net/php/hello.php";
		//Unity2018~
		UnityWebRequest uwr = UnityWebRequest.Get(url);
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