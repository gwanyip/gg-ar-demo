using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScreenShotMger : MonoBehaviour {

    public string filePath;    
    public GameObject planePrefab;
    public Camera screenShotcam;
    public Camera arCam;

    private CaptureScreenShot captureScreenShot;
    private NativeShare nativeShare;
    private GameObject screenShotCamGO;
    private GameObject arCameGO;
    private Canvas uiCanvas;
    private GameObject screenShot;
    private Texture2D newTexture2D;
    private GameObject generalButtons;
    private GameObject screenShotButtons;

    private void Start()
    {
        // Getting Shot Camera locally
        screenShotCamGO = GameObject.FindGameObjectWithTag("ScreenShotCamera");
        screenShotcam = screenShotCamGO.GetComponent<Camera>();
        screenShotcam.enabled = false;

        // Getting AR Camera locally
        arCameGO = GameObject.FindGameObjectWithTag("MainCamera");
        arCam = arCameGO.GetComponent<Camera>();
        arCam.enabled = true;

        // Getting UI Canvas
        uiCanvas = GameObject.FindGameObjectWithTag("UI").GetComponent<Canvas>();

        // Getting CaptureScreenShot locally
        captureScreenShot = GameObject.FindObjectOfType<CaptureScreenShot>();

        // Storing NativeShare locally
        nativeShare = GameObject.FindObjectOfType<NativeShare>();

        // Storing GeneralButtons locally
        generalButtons = GameObject.FindGameObjectWithTag("GeneralButtons");

        // Storing ScreenShotButtons locally
        screenShotButtons = GameObject.FindGameObjectWithTag("ScreenShotButtons");
        screenShotButtons.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.B)) {
            ShowMenuButtons("general");
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            ShowMenuButtons("screenshot");
        }
    }

    public void TakeScreenShot() {
        captureScreenShot.CaptureSaveToAlbum();
    }

    public void ShareScreenShot() {
        nativeShare.Share();
    }

    public void DestroyScreenShot() {
        Destroy(screenShot);
        SwitchCamOn("ar");
        ShowMenuButtons("general");
    }

    public void AccessFileFromDir(string path)
    {
        // This function stores the file path from CaptureScreenShot locally
        filePath = path;
        Debug.Log("Filepath in ScreenShotMger is  " + filePath);
        ApplyNewTexture(filePath);
    }

    public void ApplyNewTexture(string filePath) {
        // Managing menu state and visibility
        ToggleCanvas("on");
        ShowMenuButtons("screenshot");
        // Calculating screen size for texture plane
        float height = (screenShotcam.orthographicSize * 2.0f) / 10f;
        float width = height * Screen.width / Screen.height;

        Debug.Log("In ApplyNewTexture, filePath is " + filePath);
        // Loading image to texture
        newTexture2D = LoadPNG(filePath);

        // Switch shot cam on
        SwitchCamOn("shot");

        // Instantiating plane screenshot prefab
        screenShot = Instantiate(planePrefab, screenShotcam.transform.position + screenShotcam.transform.forward * 0.5f, Quaternion.Euler(90, -180, 0));
        // Updating size of plane to fit camera size
        screenShot.transform.localScale = new Vector3(width, 1f, height);
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

    public void SwitchCamOn(string cam) {
        // Switch from AR Camera to Screenshot Camera
        if (cam == "ar")
        {
            arCam.enabled = true;
            screenShotcam.enabled = false;
        }
        else if (cam == "shot") {
            arCam.enabled = false;
            screenShotcam.enabled = true;
        }
    }

    public void ToggleCanvas(string state)
    {
        if (state == "on")
        {
            uiCanvas.enabled = true;
        }
        else if (state == "off")
        {
            uiCanvas.enabled = false;
        }
    }

    public void ShowMenuButtons(string menu) {
        if (menu == "general") {
            Debug.Log("Show general menu");
            generalButtons.SetActive(true);
            screenShotButtons.SetActive(false);
        } else if (menu == "screenshot") {
            Debug.Log("Show screenshot menu");
            screenShotButtons.SetActive(true);
            generalButtons.SetActive(false);            
        }
    }

}
