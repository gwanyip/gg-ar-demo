using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    private Animator menuAnimator;

	// Use this for initialization
	void Start () {        
        menuAnimator = GetComponent<Animator>();
        ResetMenu();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.B)) {
            Debug.Log("Menu Open btn press");
            OpenMenu();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            Debug.Log("Menu Open btn close");
            CloseMenu();
        }
    }

    public void OpenMenu() {       
        // Open menu
        Debug.Log("Menu Open");
        menuAnimator.SetBool("MenuOpen", true);
        menuAnimator.SetBool("MenuClose", false);
    }

    public void CloseMenu()
    {
        // Close menu
        Debug.Log("Menu Close");
        menuAnimator.SetBool("MenuClose", true);
        menuAnimator.SetBool("MenuOpen", false);
    }

    public void ResetMenu()
    {
        // Close menu
        Debug.Log("Reset menu");
        menuAnimator.SetBool("MenuClose", false);
        menuAnimator.SetBool("MenuOpen", false);
    }
}
