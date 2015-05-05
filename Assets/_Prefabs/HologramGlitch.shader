Shader "Custom/HologramGlitch" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Alpha ("Alpha (A)", 2D) = "white" {}
        _AlphaColor ("Alpha Color", Color) = (0,0,0,1)
         _AlphaRolloff ("Alpha Roll Off", 2D) = "white" {}
        _ImageOver ("ImageOver (RGB)", 2D) = "white" {}
          _ImageColor ("Image Color", Color) = (1,1,1,1)
          
         _Emission ("Emmisive Color", Color) = (0,0,0,0)
         _SpecColor ("Spec Color", Color) = (1,1,1,1)
		_GreenBlue ("Green / Blue", float) = 1
        
        
    }
    

	
    SubShader {
    
	
    
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
       
       Material
		{
			Diffuse(1,1,1,1)
			Ambient(1,1,1,1)
			Emission[_Emission]
			Specular[_SpecColor]
			
		}
		Lighting On
		SeparateSpecular On
       
        ZWrite Off
     
        Blend SrcAlpha OneMinusSrcAlpha
       // ColorMask RGB
       
        Pass  {
        	Cull off
            SetTexture[_MainTex] {
                Combine texture
            }
            SetTexture[_Alpha] {
            ConstantColor [_AlphaColor]
                Combine previous, texture * constant
            }
             SetTexture[_AlphaRolloff] {
                Combine previous * texture 
            }
            
      

        }
        Blend SrcAlpha OneMinusSrcAlpha
    	
            Pass  {
            
            
            Lighting On
            
           
        	Cull off
            SetTexture[_ImageOver] {
             ConstantColor [_ImageColor]
                Combine texture  * constant
            }
        }
        
        
        
        
        
        
       
    }
    
   
		
		
    

}


