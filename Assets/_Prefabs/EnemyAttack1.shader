Shader "Custom/Enemy1 Attack" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
   		_MainColor ("Base Color", Color) = (1,1,1,1)
          
//         _Emission ("Emmisive Color", Color) = (0,0,0,0)
//         _SpecColor ("Spec Color", Color) = (1,1,1,1)
		
        
        
    }
    

	
    SubShader {
    
	
    
        Tags { "RenderType" = "Transparent" "Queue" = "Transparent"}
       
        ZWrite Off
     
        Blend SrcAlpha OneMinusSrcAlpha
       // ColorMask RGB
       
        Pass  {
        	Cull Off
            SetTexture[_MainTex] {
             ConstantColor [_MainColor]
                Combine texture * constant
            }
            
            
      

        }
    
        
        
        
        
        
        
       
    }
    
   
		
		
    

}


