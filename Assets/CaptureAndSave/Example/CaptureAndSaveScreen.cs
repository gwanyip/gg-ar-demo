using UnityEngine;
using System.Collections;
using System.IO;

public class CaptureAndSaveScreen : MonoBehaviour {
	
	string x = "0";
	string y = "0";
	string width = "0";
	string height = "0";
    string log = "Log";

    public string screenShotDirectory;
    public string screenShotFilename;
    public Texture2D tex;
    private GUIStyle guiStyle = new GUIStyle();

    public ScreenShotMger screenShot;

	CaptureAndSave snapShot ;

	
	void Start()
	{
		snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
	}

	void OnEnable()
	{
		CaptureAndSaveEventListener.onError += OnError;
		CaptureAndSaveEventListener.onSuccess += OnSuccess;
	}

	void OnDisable()
	{
		CaptureAndSaveEventListener.onError += OnError;
		CaptureAndSaveEventListener.onSuccess += OnSuccess;
	}

	void OnError(string error)
	{
		log += "\n"+error;
		Debug.Log ("Error : "+error);
	}

	void OnSuccess(string msg)
	{
		log += "\n"+msg;
		Debug.Log ("Success : "+msg);
        if (msg != null) {
            // Get Directorys
            // screenShotDirectory = Path.GetDirectoryName(msg);
            // Saving File name
            // screenShotFilename = Path.GetFileName(msg);
            // Send in Directory path and filename into ScreenShot
            // screenShot.AccessFileFromDir(screenShotDirectory, screenAblumName, screenShotFilename);
        }
	}

	void OnGUI()
	{
        guiStyle.fontSize = 20;
        GUILayout.Label (log, guiStyle);
		if(GUI.Button(new Rect(20,200,150,50),"Save Full Screen"))
		{
            // Saves image locally to C:\Users\gwany\Pictures\
            snapShot.CaptureAndSaveToAlbum(ImageType.JPG);
            // Saves image on device
            // snapShot.CaptureAndSaveAtPath(System.IO.Path.Combine(Application.persistentDataPath,"Image.jpg"),ImageType.JPG);
            // Save image to Assets folder
            // snapShot.CaptureAndSaveAtPath(Application.dataPath + "/Resources/AppImages/", ImageType.JPG);
        }

        if (GUI.Button(new Rect(200,290,170,50),"Save in double resolution"))
		{
			snapShot.CaptureAndSaveToAlbum(Screen.width * 2, Screen.height * 2, Camera.main,ImageType.JPG);
		}

		GUI.Label(new Rect(20,190,500,20),"------------------------------------------------------------------------------------------------------------------------------");

		GUI.Label(new Rect(20,280,50,20),"X : ");
		x = GUI.TextField(new Rect(80,280,50,20),x);

		GUI.Label(new Rect(160,280,50,20),"Y : ");
		y = GUI.TextField(new Rect(200,280,50,20),y);

		GUI.Label(new Rect(20,310,50,20),"Width : ");
		width = GUI.TextField(new Rect(80,310,50,20),width);

		GUI.Label(new Rect(150,310,50,20),"Height : ");
		height = GUI.TextField(new Rect(200,310,50,20),height);

		if(GUI.Button(new Rect(20,340,150,50),"Save Selected Screen") && int.Parse(width) > 0 && int.Parse(height) > 0)
		{
			snapShot.CaptureAndSaveToAlbum(int.Parse(x),int.Parse(y),int.Parse(width),int.Parse(height),ImageType.JPG);
		}

	
		GUI.Label(new Rect(20,410,500,20),"------------------------------------------------------------------------------------------------------------------------------");
		GUI.Label(new Rect(70,430,200,50),"Click This Texture to Save");
		if(GUI.Button(new Rect(50,450,200,200),tex) && tex != null)
		{
			snapShot.SaveTextureToGallery(tex,ImageType.JPG);
		}

	}
}
