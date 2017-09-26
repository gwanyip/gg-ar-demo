using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARSceneManager : MonoBehaviour {

	public GameObject m_HitObject;
    public ScreenShotMger screenShotMger;

	private GameObject statusObj;
	private GameObject pointCloudObj;
	private UnityPointCloudExample pointCloudParticle;
    private Renderer pointCloudRender;
	private Text statusText;
	private Renderer hitObject_rend;

	// Use this for initialization
	void Start () {

        // Getting ScreenShotMger
        screenShotMger = GameObject.FindWithTag("SceneMger").GetComponent<ScreenShotMger>();

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
        if (Time.timeSinceLevelLoad >= 2.0f && Time.timeSinceLevelLoad <= 2.2f){
            // screenShotMger.StatusBar("scanEnv");
            StartCoroutine(screenShotMger.FadeStatusBackground(1.0f, 0.7f, "scanEnv", 6f, true));
        }
        if (Time.timeSinceLevelLoad >= 10.0f && Time.timeSinceLevelLoad <= 10.2f) {
			screenShotMger.StatusBar("placeObj");
            TapToPlace ();
		}
	}

	public void RenderObject(){
		Color zeroAlpha = new Color(1, 1, 1, 0);		
        GameObject statusBarBkground = GameObject.FindGameObjectWithTag("StatusBarBkground");
        statusBarBkground.GetComponent<RawImage>().color = zeroAlpha;

        screenShotMger.StatusBar("default");
		hitObject_rend.enabled = true;
	}

	public void TapToPlace(){		
        // Turning Point Cloud off
		pointCloudParticle.enabled = false;
		DestroyAllParticles ();
	}

	public void DestroyAllParticles(){

		// var hitParticles = GameObject.Find ("PointCloudPrefab(Clone)");

		foreach(GameObject hitParticle in pointCloudParticle.pointCloudObjects)
		{
			Destroy(hitParticle);
		}
	}

}
