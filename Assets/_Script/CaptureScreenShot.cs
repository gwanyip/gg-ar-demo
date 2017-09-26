using UnityEngine;
using System.Collections;
using System.IO;

public class CaptureScreenShot : MonoBehaviour
{

    string x = "0";
    string y = "0";
    string width = "0";
    string height = "0";
    string log = "Log";

    private ScreenShotMger screenShotMger;
    private string screenShotPath;
    private string screenShotDirectory;
    private string screenShotAblumName;
    private string screenShotFilename;
    public Texture2D tex1;
	public Texture2D tex2;
	public Texture2D tex3;
	public Texture2D tex4;
    
    CaptureAndSave snapShot;

    void Start()
    {
        snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
        screenShotMger = GameObject.FindObjectOfType<ScreenShotMger>();
        // Build path name for Android
        screenShotDirectory = "/storage/emulated/0/Pictures/";
        screenShotAblumName = snapShot.ALBUM_NAME;
		Debug.Log ("Application.dataPath " + Application.dataPath);
		Debug.Log ("Application.persistentDataPath " + Application.persistentDataPath);
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
        log += "\n" + error;
        Debug.Log("Error : " + error);
    }

    void OnSuccess(string msg)
    {
        log += "\n" + msg;
        Debug.Log("Success : " + msg);
        if (msg != null)
        {
            // Saving File name
            screenShotMger.AccessFileFromDir(msg);
			Debug.Log (msg);

        }
    }

    public void CaptureSaveToAlbum() {
        // Saves image locally to C:\Users\gwany\Pictures\
        screenShotMger.ToggleCanvas("off");
        snapShot.CaptureAndSaveToAlbum(ImageType.PNG);

        // Saves image on device
        // snapShot.CaptureAndSaveAtPath(System.IO.Path.Combine(Application.persistentDataPath,"Image.jpg"),ImageType.JPG);
        // Save image to Assets folder
        // snapShot.CaptureAndSaveAtPath(Application.dataPath + "/Resources/AppImages/", ImageType.JPG);
    }

}
