using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadbundle : MonoBehaviour
{
	public string ModelLinkAndroid;
	public string ModellinkiOS;
	public GameObject _Model;
	GameObject _instance;


	// Start is called before the first frame update
	void Start()
	{
		StartCoroutine(LoadBundle());
	}

	// Update is called once per frame
	void Update()
	{



	}


	public void OnButtonClick()
	{

		Debug.Log(_Model.GetComponent<RotateObject>().Play);
		ArTapToPlaceObject.Instance._placedObj.GetComponent<RotateObject>().Play = !ArTapToPlaceObject.Instance._placedObj.GetComponent<RotateObject>().Play;

	}

	IEnumerator LoadBundle()
	{
		while (!Caching.ready)
		{
			yield return null;
		}

		//Begin download

#if UNITY_IOS
		WWW www = WWW.LoadFromCacheOrDownload(ModellinkiOS, 10); // iOS
#endif
#if UNITY_ANDROID
		WWW www = WWW.LoadFromCacheOrDownload(ModelLinkAndroid, 10);
#endif

		yield return www;

		if (www.error != null)
			yield return null;

		else
		{
			AssetBundle bundle = www.assetBundle;
			string name = bundle.mainAsset.name;
			Debug.Log("name" + name);
			AssetBundleRequest bundleRequest = bundle.LoadAssetAsync(name, typeof(GameObject));

			Debug.Log("Bundlerequest" + bundleRequest);

			yield return bundleRequest;

			_Model = bundleRequest.asset as GameObject;
			_Model.gameObject.tag = "Model";

			ArTapToPlaceObject.Instance.objectToPlace = _Model;

			bundle.Unload(false);

			//_LoadAR.gameObject.SetActive(false);

			Debug.Log("bundle loaded succsessfully");

		}
		www.Dispose();
	}
}
