using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour {

    private Animator menuAnimator;
    private bool isMenuOpen;

    // Use this for initialization
    void Start () {        
        menuAnimator = GetComponent<Animator>();
        ResetMenu();
        isMenuOpen = false;
    }
	
    public void ToggleButton() {        
        if (!isMenuOpen) {
            isMenuOpen = true;
            OpenMenu();
        } else if (isMenuOpen) {
            isMenuOpen = false;
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
