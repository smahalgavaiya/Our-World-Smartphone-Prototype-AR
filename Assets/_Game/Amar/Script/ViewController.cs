using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
	public List<GameObject> _MapViews;
	public PinchRotationSample _PinchObj;

	[Header("GameObject for pinch")]
	public List<GameObject> _PinchGameObject;

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

		_PinchObj.target = _PinchGameObject[index].transform;

		if (index == 0)
			_PinchObj.AR = true;
		else
			_PinchObj.AR = false;

	}
}
