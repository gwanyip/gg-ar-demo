using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShotMger : MonoBehaviour {

    public string filePath;    
    public Texture2D newTexture2D;
    public GameObject planePrefab;
    public GameObject screenShot;
    public GameObject screenShotCamGO;
    public Camera screenShotcam;

    private void Start()
    {
        screenShotCamGO = GameObject.FindGameObjectWithTag("ScreenShotCamera");
        screenShotcam = screenShotCamGO.GetComponent<Camera>();
        screenShotcam.enabled = false;
    }

    public void AccessFileFromDir(string path)
    {
        // This function stores the file path from CaptureScreenShot locally
        filePath = path;
        Debug.Log("Filepath is " + filePath);
        ApplyNewTexture(filePath);
    }

    public void ApplyNewTexture(string filePath) {
        Debug.Log("In ApplyNewTexture, filePath is " + filePath);
        // Loading image to texture
        newTexture2D = LoadPNG(filePath);
        // Switch from AR Camera to Screenshot Camera
        Camera.main.enabled = false;
        screenShotcam.enabled = true;
        // Instantiating plane screenshot prefab
        screenShot = Instantiate(planePrefab, screenShotcam.transform.position + screenShotcam.transform.forward * 0.5f, Quaternion.Euler(90, -180, 0));
        // Debug.Log("Screenshot " + screenShot);
        // Applying new texture to instantiated prefab main texture
        screenShot.GetComponent<Renderer>().material.mainTexture = newTexture2D;
        // Applying image to new texture
        newTexture2D.Apply();
    }

    public static Texture2D LoadPNG(string filePath)
    {
        Debug.Log("In LoadPNG, filePath is " + filePath);
        Texture2D tex = null;
        byte[] fileData;

        if (File.Exists(filePath))
        {
            // Debug.Log("In LoadPNG IF statement");
            fileData = File.ReadAllBytes(filePath);
            tex = new Texture2D(1, 1);
            tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
        }
        return tex;
    }

    public void SwitchCams() {
        
    }

}
