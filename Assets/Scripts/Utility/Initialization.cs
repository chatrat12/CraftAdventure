﻿using System.Linq;
using UnityAsync;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initialization
{
    public static void Initialize(Vector3 position)
    {
        var player = InitializePlayerScene(position);
        InitializeCameraRig(player);
        InitializeUI(player);
    }

    private static Player InitializePlayerScene(Vector3 position)
    {
        var playerScene = SceneManager.CreateScene("PlayerScene");

        var avatar = GameObject.Instantiate(Resources.Load<PlayerAvatar>("Player/P_Player"));
        SceneManager.MoveGameObjectToScene(avatar.gameObject, playerScene);
        avatar.transform.position = position;

        return avatar.Player;
    }

    private static void InitializeCameraRig(Player player)
    {
        var cameraRig = GameObject.Instantiate(Resources.Load<CameraRig>("Player/P_CameraRig"));
        SceneManager.MoveGameObjectToScene(cameraRig.gameObject, player.Avatar.gameObject.scene);
        cameraRig.transform.localPosition = player.Avatar.transform.position;
        cameraRig.Player = player;
    }

    private async static void InitializeUI(Player player)
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        await Await.NextUpdate();
        var t = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        var menus = SceneManager.GetSceneByName("UI").GetRootGameObjects()
                                                     .Select(g => g.GetComponentInChildren<UIPlayerMenus>())
                                                     .First(m => m != null);
        player.Menu = menus;
        menus.Player = player;
        player.Menu.Hide();
    }
}