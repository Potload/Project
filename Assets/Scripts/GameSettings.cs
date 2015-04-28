using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

//Bodhi Donselaar 2015
public class GameSettings : MonoBehaviour {
    public enum TextureRes{High, Medium, Low};
    public TextureRes TextureResolution=TextureRes.High;
    public enum AA{Off,FXAA,MSAA2,MSAA4};
    public AA AntiAliasingLevel=AA.FXAA;
    public enum ShaderQuality{Low,High};
    public ShaderQuality Shaders=ShaderQuality.High;
    public enum ParticleQuality{Low,High};
    public ParticleQuality Particles=ParticleQuality.High;
    public static bool FXAA=false;
    public bool Vsync=true;
    public bool Shadows=true;
    public bool SoftShadows=true;
    public bool Apply=false;


	void Start () 
    {
        ApplySettings();
	}
	
	void Update ()
    {
        if (!Shadows)
        {
            SoftShadows=false;
        }
        if (Apply)
        {
            ApplySettings();
            Apply=false;
        }

    }

    void ApplySettings()
    {
        switch(TextureResolution)
        {
            case TextureRes.High: 
                QualitySettings.masterTextureLimit=0;
                break;
            case TextureRes.Medium:
                QualitySettings.masterTextureLimit=1;
                break;
            case TextureRes.Low:
                QualitySettings.masterTextureLimit=2;
                break;
            default:
                QualitySettings.masterTextureLimit=0;
                break;
        }


        switch(AntiAliasingLevel)
        {
            case AA.Off: 
                QualitySettings.antiAliasing = 0;
                Camera.main.GetComponent<Antialiasing>().enabled = false;
                break;
            case AA.FXAA: 
                QualitySettings.antiAliasing = 0;
                Camera.main.GetComponent<Antialiasing>().enabled = true;
                break;
            case AA.MSAA2: 
                QualitySettings.antiAliasing = 2;
                Camera.main.GetComponent<Antialiasing>().enabled = false;
                break;
            case AA.MSAA4: 
                QualitySettings.antiAliasing = 4;
                Camera.main.GetComponent<Antialiasing>().enabled = false;
                break;
            default:
                QualitySettings.antiAliasing = 0;
                Camera.main.GetComponent<Antialiasing>().enabled = false;
                break;
        }

        if (Vsync)
        {
            QualitySettings.vSyncCount = 1;
        } else
        {
            QualitySettings.vSyncCount = 0;
        }

        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag ("LightShadow");
        for(var i = 0 ; i < gameObjects.Length ; i ++)
        {
            Light lightid=gameObjects[i].GetComponent<Light>();

            if (Shadows)
            {
                if (SoftShadows)
                {
                    lightid.shadows=LightShadows.Soft;
                } else
                {
                    lightid.shadows=LightShadows.Hard;
                }
            } else
            {
                SoftShadows=false;
                lightid.shadows=LightShadows.None;
            }
        }

    }
}
