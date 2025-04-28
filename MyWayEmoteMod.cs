using BepInEx;
using EmotesAPI;
using LethalEmotesAPI.ImportV2;
using UnityEngine;
using System.IO;
using System.Reflection;

[BepInPlugin("com.shari.mywayemote", "My Way Emote", "1.0.0")]
[BepInDependency("com.weliveinasociety.CustomEmotesAPI")]
public class MyWayEmote : BaseUnityPlugin
{
    private void Awake()
    {
        string bundleDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "assetbundles");
        string bundlePath = Path.Combine(bundleDir, "sinatraemote.bundle");
        Logger.LogInfo($"Looking for assetbundle at: {bundlePath}");

        if (!File.Exists(bundlePath))
        {
            Logger.LogError("AssetBundle file does not exist at the specified path!");
            return;
        }

        AssetBundle bundle = AssetBundle.LoadFromFile(bundlePath);
        if (bundle == null)
        {
            Logger.LogError("Failed to load AssetBundle. Returned null.");
            return;
        }

        AnimationClip clip = bundle.LoadAsset<AnimationClip>("mixamo.com");
        AudioClip song = bundle.LoadAsset<AudioClip>("myway");

        if (clip == null || song == null)
        {
            Logger.LogError("Required assets are missing in the bundle.");
            return;
        }

        var emote = new CustomEmoteParams
        {
            primaryAnimationClips = new AnimationClip[] { clip },
            primaryAudioClips = new AudioClip[] { song },
            displayName = "My Way",
            internalName = "myway",
            thirdPerson = true,
            preventMovement = false,
            stopWhenMove = false,
            syncAnim = true,
            syncAudio = true
        };

    
        
        EmoteImporter.ImportEmote(emote);
        Logger.LogInfo("Sinatra emote registered!");
    }
}
