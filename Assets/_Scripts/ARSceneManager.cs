using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARSceneManager : MonoBehaviour {

	public GameObject m_HitObject;

	private GameObject statusObj;
	private GameObject pointCloudObj;
	private UnityPointCloudExample pointCloudParticle;
    private Renderer pointCloudRender;
	private Text statusText;
	private Renderer hitObject_rend;

	// Use this for initialization
	void Start () {

		// Getting StatusText and setting to blank
		statusObj = GameObject.Find("StatusText");
		statusText = statusObj.GetComponent<Text> ();
		RemoveStatusText ();
		// Setting status text to initial message
		ScanningEnv();

		// Getting hit object renderer
		hitObject_rend = m_HitObject.GetComponent<Renderer> ();
		hitObject_rend.enabled = false;

		// Getting point cloud object
		pointCloudObj = GameObject.Find ("PointCloudExample");
		Debug.Log ("pointCloudObj: " + pointCloudObj);
		pointCloudParticle = pointCloudObj.GetComponent<UnityPointCloudExample> ();
		Debug.Log ("pointCloudParticle: " + pointCloudParticle);
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeSinceLevelLoad >= 8.0f && Time.timeSinceLevelLoad <= 8.2f) {
			TapToPlace ();
		}
		if (Input.GetKeyUp (KeyCode.B)) {
			TapToPlace ();
		}
	}

	public void RenderObject(){
		RemoveStatusText ();
		hitObject_rend.enabled = true;
	}

	public void ScanningEnv(){
		// Updating status text
		statusText.text = "Scanning environment for surface...";
	}

	public void TapToPlace(){
		// Updating status text
		statusText.text = "Tap to place object";
		// Turning Point Cloud off
		// pointCloudParticle.numPointsToShow = 0;
		pointCloudParticle.enabled = false;
		DestroyAllParticles ();
	}

	public void RemoveStatusText(){
		// Removing status text
		statusText.text = "";
	}

	public void DestroyAllParticles(){

		// var hitParticles = GameObject.Find ("PointCloudPrefab(Clone)");

		foreach(GameObject hitParticle in pointCloudParticle.pointCloudObjects)
		{
			Destroy(hitParticle);
		}
	}

}
