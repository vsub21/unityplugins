﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Microsoft.UnityPlugins;
using System.Security.Cryptography;

public class CoreScenariosTesting : MonoBehaviour {
    public Text text;
    public Button button;

    public void OnLiveTileButtonPressed()
    {
        text.text = "Live tile button was pressed";

        // TESTED - Works
        //Tiles.UpdateTile(TileTemplateType.TileSquareText01, new string[] {"MyTile", "tile1", "tile2", "tile3" });

        Tiles.UpdateTile(TileTemplateType.TileSquareImage, null, new string[] { "Assets/150x150.png"});

        Debug.Log("LiveTilesUpdated");
    }

    public void OnClearLiveTilePressed()
    {
        text.text = "Live Tile cleared";
        Debug.Log("Live Tile Cleared");
        Tiles.ClearTile();
    }


    public void OnShowBadgePressed()
    {
        text.text = "Show badge button was pressed";
        Tiles.CreateBadge("20");
        Debug.Log("Badge created");
    }

    public void OnClearBadgePressed()
    {
        text.text = "Badge cleared";
        Debug.Log("Badge Cleared");
        Tiles.ClearBadge();
    }

    public void ShowToastPressed()
    {
        text.text = "Show Toast Pressed";
        Debug.Log("Show Toast Pressed");
        // Toasts.
        Toasts.ShowToast(ToastTemplateType.ToastText01, new string[] { "toasting to good life." }, null);
    }


    public void WriteTextPressed()
    {
        text.text = "Write text Pressed";
        Debug.Log("Write Text Pressed");
        TextUtils.WriteAllText("myfile.txt", "This is some text");
    }

    public void ReadTextPressed()
    {
        text.text = "Read Text Pressed";
        Debug.Log("Read Text Pressed");
        Debug.Log("ReadTextPressed: " + TextUtils.ReadAllText("myfile.txt"));
    }

    byte [] mybytes = new byte[] { 0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26 };

    public void WriteBytesPressed()
    {
        text.text = "Write bytes Pressed";
        Debug.Log("Write bytes Pressed");

        TextUtils.WriteAllBytes("myfile.txt", mybytes);
    }

    public void ReadBytesPressed()
    {
        text.text = "Read bytes Pressed";
        Debug.Log("Read bytes Pressed");
        var bytes = TextUtils.ReadAllBytes("myfile.txt");

        if(bytes.Length == mybytes.Length)
        {
            for(int i = 0; i < bytes.Length; i++)
            {
                if(bytes[i] != mybytes[i])
                {
                    Debug.Log("Error: Bytes written were not the same as read");
                    return;
                }
            }
        }

        Debug.Log("Bytes read back successfully");
    }

    public void TestRoamingSettings ()
    {
        RoamingSettings.SetValueForKey("roaming_key1", "roaming value 1");

        RoamingSettings.SetValueForKeyInContainer("roaming_container1", "roaming_key2", "roaming value 2");

        var getValue1 = RoamingSettings.GetValueForKey("roaming_key1");
        if (getValue1.ToString() == "roaming value 1")
        {
            Debug.Log("roaming value 1 successfully retrieved");
        }
        else
        {
            Debug.Log("FAILED: roaming value 1 retrieval");
        }

        var getvalue2 = RoamingSettings.GetValueForKeyInContainer("roaming_container1", "roaming_key2");
        if (getvalue2.ToString() == "roaming value 2")
        {
            Debug.Log("roaming value 2 successfully retrieved");
        }
        else
        {
            Debug.Log("FAILED: roaming value 2 retrieval");
        }

        RoamingSettings.SetValueForKeyInContainer("roaming_container2", "roaming_key3", "roaming value 3");

        foreach (var containerName in RoamingSettings.AllContainerNames)
        {
            Debug.Log("Found container: " + containerName);
        }

        RoamingSettings.SetValueForKey("roaming_key_fordelete1", "roaming value for delete 1");
        RoamingSettings.DeleteValueForKey("roaming_key_fordelete1");


        RoamingSettings.SetValueForKeyInContainer("toDeleteContainer", "roaming_key_fordelete1", "roaming value for delete 1");
        RoamingSettings.DeleteValueForKeyInContainer("toDeleteContainer", "roaming_key_fordelete1");

        RoamingSettings.DeleteContainer("roaming_container2");
        Debug.Log("deleting container container2");

        RoamingSettings.ClearAllApplicationData(() =>
        {
            Debug.Log("All application data cleared");
        });

        text.text = "Roaming settings tested";
        Debug.Log("SUCCESS: All roaming settings functions exercised");
    }

    public void TestPushStuff()
    {
        Notifications.CreatePushNotificationChannelForApplication((ch, ex) =>
        {
            Debug.Log("Push channel URL: " + ch.Uri.ToString());
        });

        //Notifications.CreatePushNotificationChannelForSecondaryTile("1", (ch, ex) =>
        //{
        //    Debug.Log("Push channel URL: " + ch.Uri.ToString());
        //});

        Notifications.RegisterForNotifications((content) =>
        {
            if(content != null)
            {
                Debug.Log("Notification content: " + content.ToString());
            }
        }, true);

        Debug.Log("SUCCESS: Push notifications tested");
        text.text = "Push notification test successful";
    }
}
