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


    public Texture2D tex;
    private ScreenShotMger screenShotMger;
    private string screenShotPath;
    private string screenShotDirectory;
    private string screenShotAblumName;
    private string screenShotFilename;
    
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
        //CaptureAndSaveEventListener.onError += OnError;
        //CaptureAndSaveEventListener.onSuccess += OnSuccess;
    }

    void OnDisable()
    {
        //CaptureAndSaveEventListener.onError += OnError;
        //CaptureAndSaveEventListener.onSuccess += OnSuccess;
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
    public Texture2D GetScreenShot(int Width, int Height, Camera cam, ImageType Itype)
    {
        return snapShot.GetScreenShot(Width, Height, cam, Itype);

    }
    public void SaveTextureAtPath(Texture2D tex2D, string path, ImageType imgType)
    {
        snapShot.SaveTextureAtPath(tex2D, path, imgType);

    }
    public void SaveTextureToGallery(Texture2D tex, ImageType Itype)
    {
        snapShot.SaveTextureToGallery(tex, Itype);
        

    }
    public void CaptureToTexture()
    {
        screenShotMger.ToggleCanvas("off");
        Texture2D tex = snapShot.GetScreenShot(Screen.width, Screen.height, Camera.main, ImageType.PNG);
        snapShot.SaveTextureToGallery(tex, ImageType.PNG);
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
