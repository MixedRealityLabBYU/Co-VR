using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{

    public static bool menuActive = false;

    public GameObject menu;
    public GameObject guiManager;

    // Start is called before the first frame update
    void Start()
    {
        menu = transform.GetChild(0).gameObject;   
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(menuActive);
    }

    public void Toggle()
    {
        
        menuActive = !menuActive;
        
        //Set the transform equal to the camera's transform, but with the y position set to 0
        //This is so that the menu is always facing the camera, but not floating above the ground
        transform.position = new Vector3(Camera.main.transform.position.x, 0, Camera.main.transform.position.z);
        
        //Rotate the transform to the same direction as the camera, but only the y rotation
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0);
    }

    public void ResumeButton()
    {
        menuActive = false;
    }

    public void RecalibrateButton()
    {
        guiManager.GetComponent<GuiManager>().Recalibrate();
        menuActive = false;
    }

    public void QuitButton()
    {
        guiManager.GetComponent<GuiManager>().QuitToMenu();
        menuActive = false;
    }

}
