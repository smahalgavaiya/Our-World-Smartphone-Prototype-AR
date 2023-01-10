using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox;
using Mapbox.Unity.Map;
using Mapbox.Utils;

public class ViewController : MonoBehaviour
{
	public static ViewController Instance = null;

	public List<GameObject> _MapViews;
	public PinchRotationSample _PinchObj;
	[SerializeField]
	List<AbstractMap> _maps;
	public Vector2d _currentLatlong;
	public float _zoomvalue;
	public string _currentSearch;

	[Header("GameObject for pinch")]
	public List<GameObject> _PinchGameObject;

	private void Awake()
	{
		if (Instance == null)
			Instance = this;
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void OnviewClick(int index)
	{
		foreach (GameObject obj in _MapViews)
			obj.SetActive(false);

		_MapViews[index].SetActive(true);
		_maps[index].SetCenterLatitudeLongitude(_currentLatlong);
		_maps[index].SetZoom(_zoomvalue);
		if (_maps[index].isActiveAndEnabled)
			StartCoroutine(IemnumWaitFormap(index));

		_PinchObj.target = _PinchGameObject[index].transform;

		if (index == 0)
			_PinchObj.AR = true;
		else
			_PinchObj.AR = false;

	}

	IEnumerator IemnumWaitFormap(int index)
	{
		yield return new WaitForSeconds(2.0f);
		_maps[index].UpdateMap();
	}

}
