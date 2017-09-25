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

    public ScreenShotMger screenShotMger;

    private string screenShotPath;
    private string screenShotDirectory;
    private string screenShotAblumName;
    private string screenShotFilename;
    private Texture2D tex;
    
    CaptureAndSave snapShot;

    void Start()
    {
        snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
        screenShotMger = GameObject.FindObjectOfType<ScreenShotMger>();
        // Build path name for Android
        screenShotDirectory = "/storage/emulated/0/Pictures/";
        screenShotAblumName = snapShot.ALBUM_NAME;
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
        // Debug.Log("Success : " + msg);
        if (msg != null)
        {
            // Saving File name
            // screenShotFilename = Path.GetFileName(msg);
            // screenShotPath = screenShotDirectory + screenShotAblumName + "/" + screenShotFilename;
            // Send in Directory path and filename into ScreenShot
            screenShotMger.AccessFileFromDir(msg);

        }
    }

    public void CaptureSaveToAlbum() {
        // Saves image locally to C:\Users\gwany\Pictures\
        screenShotMger.ToggleCanvas("off");
        snapShot.CaptureAndSaveToAlbum(ImageType.JPG);
        // ToggleCanvas("on");
        Debug.Log("Image captured");
        // Saves image on device
        // snapShot.CaptureAndSaveAtPath(System.IO.Path.Combine(Application.persistentDataPath,"Image.jpg"),ImageType.JPG);
        // Save image to Assets folder
        // snapShot.CaptureAndSaveAtPath(Application.dataPath + "/Resources/AppImages/", ImageType.JPG);
    }

}
