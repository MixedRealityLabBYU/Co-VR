using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Normal.Realtime;
using UnityEngine.XR.Interaction.Toolkit;

public class GuiManager : MonoBehaviour
{
    public enum Phase
    {
        MAIN_MENU,
        //Host
        WALL_DRAWING,
        ORIGIN_SET,
        MAKE_ROOM_PROMPT,

        //Guest
        JOIN_ROOM_PROMPT,
        ORIGIN_TELEPORT,
        FINISHED
    }

    public Phase currentPhase = Phase.MAIN_MENU;

    [Header("Main Menu GUIs")]
    public GameObject MainMenu;

    [Header("Host Menu GUIs")]
    public GameObject HostMenu;
    public GameObject GuardianInstructionMenu;
    public GameObject GuardianConfirmation;
    public GameObject OriginMenu;
    public GameObject SetRoomPrompt;


    [Header("Host Menu Objects")]
    public GameObject WallCreation;
    public GameObject OriginSetting;

    [Header("Host Menu Inputs")]
    public TMP_InputField SetRoomNameInput;

    [Header("Guest Menu GUIs")]
    public GameObject GuestMenu;
    public GameObject JoinRoomPrompt;
    public GameObject TeleportingPrompt;

    [Header("Guest Menu Inputs")]
    public TMP_InputField JoinRoomNameInput;

    [Header("Networking")]
    public Realtime realtime;

    [Header("Guardian")]
    public GameObject defaultGuardian;
    private GameObject networkGuardian;//Set at join time

    [Header("InGame Menu")]
    public GameObject inGameMenu;

    [Header("World Objects")]
    public GameObject sceneWrapper;
    public GameObject ground;

    [Header("Other")]
    public GameObject headLine;
    public TogglePassthrough passthroughToggle;
    public GameObject leftHand;
    public GameObject rightHand;


    private string role = "";


    void Start()
    {
        realtime.didConnectToRoom += OnJoin;
    }

    // Update is called once per frame
    void Update()
    {
        MainMenu.SetActive(currentPhase == Phase.MAIN_MENU);
        HostMenu.SetActive(currentPhase == Phase.WALL_DRAWING || currentPhase == Phase.ORIGIN_SET || currentPhase == Phase.MAKE_ROOM_PROMPT);
        GuestMenu.SetActive(currentPhase == Phase.JOIN_ROOM_PROMPT || currentPhase == Phase.ORIGIN_TELEPORT);

        WallCreation.SetActive(currentPhase == Phase.WALL_DRAWING);

        GuardianInstructionMenu.SetActive(currentPhase == Phase.WALL_DRAWING && !WallCreation.GetComponent<WallDrawer>().GetFinished());
        GuardianConfirmation.SetActive(currentPhase == Phase.WALL_DRAWING && WallCreation.GetComponent<WallDrawer>().GetFinished());

        OriginMenu.SetActive(currentPhase == Phase.ORIGIN_SET);
        OriginSetting.SetActive(currentPhase == Phase.ORIGIN_SET || currentPhase == Phase.ORIGIN_TELEPORT || currentPhase == Phase.FINISHED);
        OriginSetting.GetComponent<OnButtonPress>().enabled = currentPhase == Phase.ORIGIN_SET;
        OriginSetting.transform.GetChild(0).gameObject.GetComponent<OnButtonPress>().enabled = currentPhase == Phase.ORIGIN_TELEPORT;
        OriginSetting.transform.GetChild(0).gameObject.SetActive(currentPhase == Phase.ORIGIN_SET || currentPhase == Phase.ORIGIN_TELEPORT);

        SetRoomPrompt.SetActive(currentPhase == Phase.MAKE_ROOM_PROMPT);
        JoinRoomPrompt.SetActive(currentPhase == Phase.JOIN_ROOM_PROMPT);
        TeleportingPrompt.SetActive(currentPhase == Phase.ORIGIN_TELEPORT);
        
        passthroughToggle.SetPassthrough(currentPhase == Phase.WALL_DRAWING || currentPhase == Phase.ORIGIN_TELEPORT || currentPhase == Phase.ORIGIN_SET);

        leftHand.SetActive(currentPhase != Phase.ORIGIN_TELEPORT && currentPhase != Phase.FINISHED);
        rightHand.SetActive(currentPhase != Phase.ORIGIN_TELEPORT && currentPhase != Phase.FINISHED);

        // Check if any gameObjects with the Co-location tag are active and if so, enable the ray interactors on the hands
        // leftHand.transform.parent.gameObject.GetComponent<XRRayInteractor>().enabled = GameObject.FindGameObjectsWithTag("Co-location").Length > 0;
        // rightHand.transform.parent.gameObject.GetComponent<XRRayInteractor>().enabled = GameObject.FindGameObjectsWithTag("Co-location").Length > 0;

        sceneWrapper.SetActive(currentPhase == Phase.FINISHED);
        ground.SetActive(currentPhase == Phase.MAIN_MENU || currentPhase == Phase.JOIN_ROOM_PROMPT);
        
        headLine.SetActive(currentPhase == Phase.ORIGIN_TELEPORT || currentPhase == Phase.ORIGIN_SET);

        inGameMenu.SetActive(currentPhase == Phase.FINISHED);
    }

    public void RegressPhase()
    {
        Debug.Log("RegressPhase called");
        if (currentPhase == Phase.MAIN_MENU) return;
        if (currentPhase == Phase.WALL_DRAWING)
        {
            currentPhase = Phase.MAIN_MENU;
            WallCreation.GetComponent<WallDrawer>().ClearWalls();
        }
        else if (currentPhase == Phase.ORIGIN_SET)
        {
            currentPhase = Phase.WALL_DRAWING;
            WallCreation.GetComponent<WallDrawer>().ClearWalls();
        }
        else if (currentPhase == Phase.MAKE_ROOM_PROMPT)
        {
            currentPhase = Phase.ORIGIN_SET;
        }
        else if (currentPhase == Phase.JOIN_ROOM_PROMPT)
        {
            currentPhase = Phase.MAIN_MENU;
        }
        else if (currentPhase == Phase.ORIGIN_TELEPORT)
        {
            currentPhase = Phase.JOIN_ROOM_PROMPT;
        }
    }

    public void ProgressPhase()
    {
        if (currentPhase == Phase.FINISHED) 
        {
            currentPhase = Phase.MAIN_MENU;
            return;
        }
        currentPhase++;
    }

    public void Recalibrate()
    {
        currentPhase = Phase.ORIGIN_TELEPORT;
    }

    public void QuitToMenu()
    {
        currentPhase = Phase.MAIN_MENU;
        defaultGuardian.SetActive(true);
        WallCreation.SetActive(true);
        WallCreation.GetComponent<WallDrawer>().ClearWalls();
        WallCreation.GetComponent<WallDrawer>().UpdateGuardian();
        WallCreation.SetActive(false);
        OriginSetting.GetComponent<Origin>().DisableNetworkTransform();
        QuitRoom();
    }

    public void SetPhase(Phase settingPhase)
    {
        currentPhase = settingPhase;
    }


    public void HostButton()
    {
        role = "Host";
        SetPhase(Phase.WALL_DRAWING);
    }

    public void GuestButton()
    {
        role = "Guest";
        SetPhase(Phase.JOIN_ROOM_PROMPT);
    }


    //Networking Functions
    public void HostRoomButton()
    {
        JoinRoom(SetRoomNameInput.text);
        
        SetPhase(Phase.FINISHED);
        
    }

    public void JoinRoomButton()
    {
        JoinRoom(JoinRoomNameInput.text);


        SetPhase(Phase.ORIGIN_TELEPORT);
    }


    public void JoinRoom(string name)
    {
        realtime.Connect(name);
    }

    public void QuitRoom()
    {
        realtime.Disconnect();
    }


    //Listeners
    public void OnJoin(Realtime realtime)
    {
        if(role == "Host")
        {
            networkGuardian = Realtime.Instantiate("Guardian");
            
            SpawnTorch();

            networkGuardian.GetComponent<Guardian>().SetVerticesJson(defaultGuardian.GetComponent<Guardian>().GetVerticesJson());
            networkGuardian.GetComponent<Guardian>().SetTrianglesJson(defaultGuardian.GetComponent<Guardian>().GetTrianglesJson());
            defaultGuardian.SetActive(false);
            
            //Make the host the owner of the sceneWrapper
            
            sceneWrapper.GetComponent<RealtimeTransform>().RequestOwnership();
            sceneWrapper.GetComponent<RealtimeView>().RequestOwnership();
            

            OriginSetting.GetComponent<Origin>().OnJoin();
        }
        else
        {
            defaultGuardian.SetActive(false);
            OriginSetting.GetComponent<Origin>().EnableNetworkTransform();
        }
    }

    public void SpawnTorch()
    {
        if(role == "" || role == "Guest") return;
        
        GameObject torch = Realtime.Instantiate("OldHandTorch");
        // Request ownership of torch
        torch.GetComponent<RealtimeTransform>().RequestOwnership();
        torch.GetComponent<RealtimeView>().RequestOwnership();
    }
}
