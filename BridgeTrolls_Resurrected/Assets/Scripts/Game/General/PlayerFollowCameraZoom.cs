using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerFollowCameraZoom : MonoBehaviour {

	private List<GameObject> _allTargets = new List<GameObject>();

	private float _boundingBoxPadding = 2f;

	private float _minimumOrthographicSize = 4.5f;

	private float _cameraZoomSpeed = 15f;

	private float _originalCameraSize;

	private GameObject _boundsRepGo;

	private float _boundLeftLocation;
	private float _boundRightLocation;
	private float _boundTopLocation;
	private float _boundBottomLocation;

	Camera _camera;

	void Awake()
	{
		SpriteRenderer renderer;
		_camera = GetComponent<Camera>();
		_camera.orthographic = true;

		_originalCameraSize = _camera.orthographicSize;
		_boundsRepGo = GameObject.FindGameObjectWithTag(Tags.SCREEN_BOUND_OBJECT_TAG);

		_minimumOrthographicSize = (_camera.orthographicSize * _minimumOrthographicSize / 5.4f);

		renderer = _boundsRepGo.gameObject.GetComponent<SpriteRenderer>();

		_boundLeftLocation = _boundsRepGo.transform.position.x - ((renderer.bounds.size.x * _boundsRepGo.transform.localScale.x) / 2);
		_boundRightLocation = _boundsRepGo.transform.position.x + ((renderer.bounds.size.x * _boundsRepGo.transform.localScale.x) / 2);
		_boundTopLocation = _boundsRepGo.transform.position.y + ((renderer.bounds.size.y * _boundsRepGo.transform.localScale.y) / 2);
		_boundBottomLocation = _boundsRepGo.transform.position.y - ((renderer.bounds.size.y * _boundsRepGo.transform.localScale.y) / 2);
	}

	void LateUpdate()
	{
		if(_allTargets.Count == 0)
		{
			FindAllPlayers();
		}

		Rect boundingBox = CalculateTargetsBoundingBox();
		//transform.position = CalculateCameraPosition(boundingBox);
		_camera.orthographicSize = CalculateOrthographicSize(boundingBox);
		CorrectedCameraPosition(CalculateCameraPosition(boundingBox));
	}

	Rect CalculateTargetsBoundingBox()
	{
		float minX = Mathf.Infinity;
		float maxX = Mathf.NegativeInfinity;
		float minY = Mathf.Infinity;
		float maxY = Mathf.NegativeInfinity;

		foreach(GameObject target in _allTargets)
		{
			Vector3 position = target.transform.position;

			minX = Mathf.Min(minX, position.x);
			minY = Mathf.Min(minY, position.y);
			maxX = Mathf.Max(maxX, position.x);
			maxY = Mathf.Max(maxY, position.y);
		}

		return Rect.MinMaxRect(
			minX - _boundingBoxPadding,
			maxY + _boundingBoxPadding,
			maxX + _boundingBoxPadding,
			minY - _boundingBoxPadding);
	}

	Vector3 CalculateCameraPosition (Rect boundingBox)
	{
		Vector2 boundingBoxCenter = boundingBox.center;

		return new Vector3(boundingBoxCenter.x, boundingBoxCenter.y, _camera.transform.position.z);
	}

	private void CorrectedCameraPosition(Vector3 correctedPosition)
	{
		float cameraHeightHalfSize = Camera.main.orthographicSize;
		float cameraWidthHalfSize = cameraHeightHalfSize * Screen.width / Screen.height;

		Vector2 newCameraPosition = new Vector2(correctedPosition.x, correctedPosition.y);

		if (correctedPosition.x + cameraWidthHalfSize > _boundRightLocation) {
			newCameraPosition.x = _boundRightLocation - cameraWidthHalfSize;
		} else if(correctedPosition.x - cameraWidthHalfSize < _boundLeftLocation){
			newCameraPosition.x = _boundLeftLocation + cameraWidthHalfSize;
		}

		if (correctedPosition.y + cameraHeightHalfSize > _boundTopLocation) {
			newCameraPosition.y = _boundTopLocation - cameraHeightHalfSize;
		} else if (correctedPosition.y - cameraHeightHalfSize < _boundBottomLocation) {
			newCameraPosition.y = _boundBottomLocation + cameraHeightHalfSize;
		}

		_camera.transform.position = Vector3.Lerp(_camera.transform.position,new Vector3(newCameraPosition.x,newCameraPosition.y,_camera.transform.position.z),0.2f);
	}

	float CalculateOrthographicSize (Rect boundingBox)
	{
		float orthographicSize = _camera.orthographicSize;
		Vector3 topRight = new Vector3(boundingBox.x + boundingBox.width, boundingBox.y, 0f);
		Vector3 topRightAsViewport = _camera.WorldToViewportPoint(topRight);

		if (topRightAsViewport.x >= topRightAsViewport.y)
			orthographicSize = Mathf.Abs(boundingBox.width) / _camera.aspect / 2f;
		else
			orthographicSize = Mathf.Abs(boundingBox.height) / 2f;

		return Mathf.Clamp(Mathf.Lerp(_camera.orthographicSize, orthographicSize, Time.deltaTime * _cameraZoomSpeed), _minimumOrthographicSize, Mathf.Infinity);
	}

	private void FindAllPlayers()
	{
		GameObject[] currentPlayers = GameObject.FindGameObjectsWithTag(Tags.PLAYER_TAG);
		GameObject[] currentMinions = GameObject.FindGameObjectsWithTag(Tags.MINION_TAG);
		GameObject currentTroll = GameObject.FindGameObjectWithTag(Tags.TROLL_TAG);

		foreach(GameObject player in currentPlayers) //Add all the found Neutral Players
		{
			_allTargets.Add(player);
		}
		foreach(GameObject minion in currentMinions) //Add all the found Minion Players
		{
			_allTargets.Add(minion);
		}
		_allTargets.Add(currentTroll);
	}

}