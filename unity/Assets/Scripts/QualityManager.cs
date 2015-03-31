// QualityManager sets shader LOD's and enabled/disables special effects
// based on platform and/or desired quality settings.

// Disable 'autoChoseQualityOnStart' if you want to overwrite the quality
// for a specific platform with the desired level.

using UnityEngine;

[ExecuteInEditMode, RequireComponent((typeof(Camera))), RequireComponent(typeof(ShaderDatabase))]
public class QualityManager : MonoBehaviour
{
    public enum Quality
    {
        Lowest = 100,
        Poor = 190,
        Low = 200,
        Medium = 210,
        High = 300,
        Highest = 500,
    }

    public bool autoChoseQualityOnStart = true;
    public Quality currentQuality = Quality.Highest;

    public MobileBloom bloom;
    public HeightDepthOfField depthOfField;
    public ColoredNoise noise;
    public RenderFogPlane heightFog;
    public MonoBehaviour reflection;
    public ShaderDatabase shaders;

    public static Quality quality = Quality.Highest;

    void Awake()
    {
        if (!bloom)
            bloom = GetComponent<MobileBloom>();
        if (!noise)
            noise = GetComponent<ColoredNoise>();
        if (!depthOfField)
            depthOfField = GetComponent<HeightDepthOfField>();
        if (!heightFog)
            heightFog = gameObject.GetComponentInChildren<RenderFogPlane>();
        if (!shaders)
            shaders = GetComponent<ShaderDatabase>();
        if (!reflection)
            reflection = GetComponent("ReflectionFx") as MonoBehaviour;

        if (autoChoseQualityOnStart)
            AutoDetectQuality();

        ApplyAndSetQuality(currentQuality);
    }

    // we support dynamic quality adjustments if in edit mode

#if UNITY_EDITOR

    void Update()
    {
        var newQuality = currentQuality;
        if (newQuality != quality)
            ApplyAndSetQuality(newQuality);
    }

#endif

    private void AutoDetectQuality()
    // Some special quality settings cases for various platforms
    {
#if UNITY_IPHONE
	
		switch (iPhoneSettings.generation)
		{
			case iPhoneGeneration.iPad1Gen:
				currentQuality = Quality.Low;
			break;
			case iPhoneGeneration.iPad2Gen:
				currentQuality = Quality.High;
			break;
			case iPhoneGeneration.iPhone3GS:
			case iPhoneGeneration.iPodTouch3Gen:
				currentQuality = Quality.Low;
			break;
			default:
				currentQuality = Quality.Medium;
			break;
		}
		
#elif UNITY_ANDROID

        currentQuality = Quality.Low;

#else
	// Desktops/consoles
	
		switch (Application.platform)
		{
			case RuntimePlatform.NaCl:
				currentQuality = Quality.Highest;
			break;
			case RuntimePlatform.FlashPlayer:
				currentQuality = Quality.Low;
			break;
			default:
				currentQuality = SystemInfo.graphicsPixelFillrate < 2800 ? Quality.High : Quality.Highest;
			break;
		}

#endif

        Debug.Log(string.Format(
            "AngryBots: Quality set to '{0}'{1}",
            currentQuality,
#if UNITY_IPHONE
			" (" + iPhoneSettings.generation + " class iOS)"
#elif UNITY_ANDROID
 " (Android)"
#else
			" (" + Application.platform + ")"
#endif
));
    }

    private void ApplyAndSetQuality(Quality newQuality)
    {
        quality = newQuality;

        // default states

        camera.cullingMask = -1 & ~(1 << LayerMask.NameToLayer("Adventure"));
        var textAdventure = GameObject.Find("TextAdventure");
        if (textAdventure)
            textAdventure.GetComponent<TextAdventureManager>().enabled = false;

        // check for quality specific states		

        if (quality == Quality.Lowest)
        {
            DisableAllFx();
            if (textAdventure)
                textAdventure.GetComponent<TextAdventureManager>().enabled = true;
            camera.cullingMask = 1 << LayerMask.NameToLayer("Adventure");
            EnableFx(depthOfField, false);
            EnableFx(heightFog, false);
            EnableFx(bloom, false);
            EnableFx(noise, false);
            camera.depthTextureMode = DepthTextureMode.None;
        }
        else if (quality == Quality.Poor)
        {
            EnableFx(depthOfField, false);
            EnableFx(heightFog, false);
            EnableFx(bloom, false);
            EnableFx(noise, false);
            EnableFx(reflection, false);
            camera.depthTextureMode = DepthTextureMode.None;
        }
        else if (quality == Quality.Low)
        {
            EnableFx(depthOfField, false);
            EnableFx(heightFog, false);
            EnableFx(bloom, false);
            EnableFx(noise, false);
            EnableFx(reflection, true);
            camera.depthTextureMode = DepthTextureMode.None;
        }
        else if (quality == Quality.Medium)
        {
            EnableFx(depthOfField, false);
            EnableFx(heightFog, false);
            EnableFx(bloom, true);
            EnableFx(noise, false);
            EnableFx(reflection, true);
            camera.depthTextureMode = DepthTextureMode.None;
        }
        else if (quality == Quality.High)
        {
            EnableFx(depthOfField, false);
            EnableFx(heightFog, false);
            EnableFx(bloom, true);
            EnableFx(noise, true);
            EnableFx(reflection, true);
            camera.depthTextureMode = DepthTextureMode.None;
        }
        else
        { // Highest
            EnableFx(depthOfField, true);
            EnableFx(heightFog, true);
            EnableFx(bloom, true);
            EnableFx(reflection, true);
            EnableFx(noise, true);
            if ((heightFog && heightFog.enabled) || (depthOfField && depthOfField.enabled))
                camera.depthTextureMode |= DepthTextureMode.Depth;
        }

        Debug.Log("AngryBots: setting shader LOD to " + quality);

        Shader.globalMaximumLOD = (int) quality;
        foreach (var s in shaders.shaders)
        {
            s.maximumLOD = (int) quality;
        }
    }

    private void DisableAllFx()
    {
        camera.depthTextureMode = DepthTextureMode.None;
        EnableFx(reflection, false);
        EnableFx(depthOfField, false);
        EnableFx(heightFog, false);
        EnableFx(bloom, false);
        EnableFx(noise, false);
    }

    private void EnableFx(MonoBehaviour fx, bool enable)
    {
        if (fx != null)
            fx.enabled = enable;
    }
}
