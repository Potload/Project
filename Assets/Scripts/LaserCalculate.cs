using UnityEngine;
using System.Collections;
//Bodhi Donselaar 2015
public class LaserCalculate : MonoBehaviour {
    public static bool[] Shoot;
    public static float[] Hoek;
    public static float[] CoolDown;
    public static float[] CoolingDown;
    public static bool[] OverHeated;
    public static float[] Disable;
	public GameObject[] Players;
    public Transform[] Lasers;
    public float CombinedChargeSpeed=1;
    public float UberChargeSpeed=1;
    public float LaserDistance = 20;
    public float CoolDownRefill = 1;
    public float CoolDownDrain = 1;
    private float[] CoolDownEase;
    private float[] BeamLength;
    private bool[] Combined;
    public static int[] Partner;
    public static int[] CombinedSource;
    public static float[] ShootLength;
    private float[] charge;
	private Ray ray;  
	private RaycastHit hit;  
    private bool[] CombinedActive;

	void Start () 
	{

        Shoot = new bool[4];
        ShootLength = new float[7];
		BeamLength=new float[6];
        Combined = new bool[6];
        Partner = new int[4];
        CombinedActive = new bool[3];
        CombinedSource = new int[2];
        charge = new float[7];
        Hoek = new float[4];
        Disable = new float[4];
        CoolDown = new float[4];
        CoolingDown = new float[4];
        CoolDownEase = new float[4];
        OverHeated = new bool[4];
        charge [4] = 0;
        charge [5] = 0;
        charge [6] = 0;
        CombinedSource [0] = 0;
        CombinedSource [1] = 0;
        for (int l=0; l<4; l++) //beginwaardes start
        {
            CoolDown[l]=0;
            CoolingDown[l]=1;
            CoolDownEase[l]=1;
            OverHeated[l]=false;
            Disable[l]=0;

        }
	}
	
	void Update () 
	{
        for (int h=0; h<4; h++) //beginwaardes
        {


            ShootLength[h]=Mathf.Lerp(ShootLength[h],1,Time.deltaTime*10);
            if (!Shoot[h])
            {
                ShootLength[h]=0;
                Lasers[h].transform.localScale=new Vector3(0,0,0);
            }
            Hoek[h]=0;
            BeamLength [h] = LaserDistance*ShootLength[h];
            Lasers[h].transform.localScale=new Vector3(1,0.1f,BeamLength[h]);
            Combined[h] = false;
            Partner[h]=-1;
            Lasers[h].gameObject.SetActive(Shoot[h]);
            charge[h]=0;

            //cooldown aanvullen
            if (CoolDown[h]<1&&CoolingDown[h]>1)
            {
                CoolDown[h]+=Time.deltaTime*CoolDownRefill*CoolDownEase[h];              
                if (CoolDown[h]>1)
                {
                    CoolDown[h]=1;
                    CoolingDown[h]=0;
                    OverHeated[h]=false;
                }
            }
            if (Shoot[h]==false&&CoolDown[h]<1)
            {
                CoolingDown[h]+=Time.deltaTime;
                CoolDownEase[h]+=Time.deltaTime;
            }
            if (Shoot[h])
            {
                CoolDownEase[h]=0;
                CoolDown[h]-=Time.deltaTime*CoolDownDrain;
                if (CoolDown[h]<0)
                {
                    CoolDown[h]=0;
                    CoolingDown[h]=0.1f;
                    OverHeated[h]=true;
                }
            }
            if (CoolDown[h]<0)
            {
                CoolDown[h]=0;
                OverHeated[h]=true;
            }
            if (OverHeated[h]&&CoolDown[h]<0.1)
            {
                xbox.VL[h]=1;
                xbox.VR[h]=1;
            }
        }

        CombinedActive [0] = false;
        CombinedActive [1] = false;
        CombinedActive [2] = false;
        Combined [4] = false;
        Combined [5] = false;
        Lasers[4].transform.localScale=new Vector3(1,0.1f,LaserDistance);
        Lasers[5].transform.localScale=new Vector3(1,0.1f,LaserDistance);
       


		for (int i=0; i<4; i++)//4x voor iedere laser, eerste loop, combineerd de eerste lasers
        {
            if (Disable[i]>0)
            {
                Shoot[i]=false;
                Disable[i]-=Time.deltaTime;
                print(Disable[i] +" "+i);
            }

            if (!Combined[i]&&Shoot[i]) //ben ik al gecombined? zoja, doe geen nieuwe raycast
            {
                int layerMask = 1 << Players[i].gameObject.layer;
                layerMask = ~layerMask;
                Hoek[i]=0;
                ray = new Ray(Players[i].transform.position,Players[i].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[i],layerMask)) //raak ik iets?
                { 
                    BeamLength[i]=hit.distance; //ik raak iets, dus ik word korter
                    int HitLayer=hit.collider.gameObject.layer-Players[0].gameObject.layer;
                    if (HitLayer>-1&&HitLayer<4) //raak ik een andere laser? hoger dan 3 is een gecombinede
                    {
                        if (HitLayer>i&&Shoot[HitLayer]) //moet de code van de andere laser nog uitgevoerd worden?
                        {
                            if (!Combined[HitLayer]) //is de laser die ik raak nog niet gecombined?
                            {
                                BeamLength[HitLayer]=Vector3.Distance(hit.point,hit.collider.transform.position);
                                Lasers[HitLayer].transform.localScale=new Vector3(1,0.1f,BeamLength[HitLayer]);
                                Combined[i]=true;
                                Combined[HitLayer]=true;
                                Partner[i]=HitLayer;
                                Partner[HitLayer]=i;
                                if (!CombinedActive[0])
                                {
                                    CombinedActive[0]=true;
                                    CombinedSource[0]=i;
                                    Lasers[4].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                                    Lasers[4].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);
                                    Lasers[4].transform.localScale=new Vector3(1,0.1f,0);

                                    //Hoek Lezen
                                    Hoek[i]=Vector3.Angle(Lasers[i].transform.forward,Lasers[4].transform.forward);
                                    Vector3 cross= Vector3.Cross(Lasers[i].transform.forward, Lasers[4].transform.forward);
                                    if (cross.y < 0) Hoek[i] = -Hoek[i];
                                    Hoek[HitLayer]=-Hoek[i];
                                }
                                else
                                {
                                    CombinedActive[1]=true;
                                    CombinedSource[1]=i;
                                    Lasers[5].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                                    Lasers[5].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);
                                    Lasers[5].transform.localScale=new Vector3(1,0.1f,0);

                                    //Hoek Lezen
                                    Hoek[i]=Vector3.Angle(Lasers[i].transform.forward,Lasers[5].transform.forward);
                                    Vector3 cross3= Vector3.Cross(Lasers[i].transform.forward, Lasers[5].transform.forward);
                                    if (cross3.y < 0) Hoek[i] = -Hoek[i];
                                    Hoek[HitLayer]=-Hoek[i];
                                }
                            }
                            else //de laser die ik raak is al gecombined
                            {
                                if (Vector3.Distance(Players[i].transform.position,Players[HitLayer].transform.position)
                                    <
                                    Vector3.Distance(Players[HitLayer].transform.position,Players[Partner[HitLayer]].transform.position)) //is de afstand korter?
                                {
                                    //reset de laser die verder weg is
                                    Combined[Partner[HitLayer]]=false;
                                    Partner[Partner[HitLayer]]=-1;
                                    BeamLength[Partner[HitLayer]]=LaserDistance;
                                    Lasers[Partner[HitLayer]].transform.localScale=new Vector3(1,0.1f,BeamLength[Partner[HitLayer]]);
                                    Disable[Partner[HitLayer]]=0.5f;
                                    print(Partner[HitLayer]+" disabled");


                                    //combine de nieuwe lasers
                                    BeamLength[HitLayer]=Vector3.Distance(hit.point,hit.collider.transform.position);
                                    Lasers[HitLayer].transform.localScale=new Vector3(1,0.1f,BeamLength[HitLayer]);
                                    Combined[i]=true;
                                    Combined[HitLayer]=true;
                                    Partner[i]=HitLayer;
                                    Partner[HitLayer]=i;
                                    //combined laser 1 nieuwe positie
                                    CombinedSource[0]=i;
                                    Lasers[4].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.2f+hit.point;
                                    Lasers[4].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);

                                    //Hoek Lezen
                                    Hoek[i]=Vector3.Angle(Lasers[i].transform.forward,Lasers[4].transform.forward);
                                    Vector3 cross5= Vector3.Cross(Lasers[i].transform.forward, Lasers[4].transform.forward);
                                    if (cross5.y < 0) Hoek[i] = -Hoek[i];
                                    Hoek[HitLayer]=-Hoek[i];
                                }
                            }
                        }
                    }
                }
                Lasers[i].transform.localScale=new Vector3(1,0.1f,BeamLength[i]);
            }
        }

        Lasers[4].gameObject.SetActive(CombinedActive[0]);
        Lasers[5].gameObject.SetActive(CombinedActive[1]);

        for (int k=0; k<2; k++)//Raycast gecombineerde lasers
        {
            if (CombinedActive[k]&&!Combined[k+4])
            {

                int layerMask = 1 << Lasers[k+4].gameObject.layer;
                layerMask = ~layerMask;
                
                ray = new Ray(new Vector3(Lasers[k+4].transform.position.x,Lasers[k+4].transform.position.y+0.1f,Lasers[k+4].transform.position.z),Lasers[k+4].transform.forward);  

                charge[k+4]+=Time.deltaTime*CombinedChargeSpeed;
                if (charge[k+4]>1)
                {
                    ShootLength[k+4]=Mathf.Lerp(ShootLength[k+4],1,Time.deltaTime*10);
                }
                BeamLength[k+4]=LaserDistance*ShootLength[k+4]; //als ie niks raakt is ie nog steeds maximaal laserdistance
                if(Physics.Raycast(ray.origin,ray.direction, out hit, LaserDistance,layerMask)) //raak ik iets?
                {
                    int HitLayer=hit.collider.gameObject.layer-Players[0].gameObject.layer;
                    BeamLength[k+4]=hit.distance;
                    if (HitLayer==4||HitLayer==5)
                    {
                        CombinedActive [2] = true;
                        BeamLength[HitLayer]=Vector3.Distance(hit.point,hit.collider.transform.position);
                        Lasers[HitLayer].transform.localScale=new Vector3(1,0.5f,BeamLength[HitLayer]);
                        Combined[k+4]=true;
                        Combined[HitLayer]=true;
                        Lasers[6].position=(Lasers[k+4].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                        Lasers[6].rotation=Quaternion.LookRotation(Lasers[k+4].forward+Lasers[HitLayer].forward);
                    }
                }
                Lasers[k+4].transform.localScale=new Vector3(1,0.5f,BeamLength[k+4]);
            }
        }
        Lasers[6].gameObject.SetActive(CombinedActive[2]);

        for (int j=0; j<4; j++)//4x voor iedere laser, tweede loop, nieuwe raycast voor ongecombineerde en check of ze een gecombineerde raken
        {
            if (!Combined[j]&&Shoot[j])//als een laser niet gecombined is
            {
                int layerMask = 1 << Players[j].gameObject.layer;
                layerMask = ~layerMask;             
                ray = new Ray(Players[j].transform.position,Players[j].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[j],layerMask)) //raak ik iets?
                { 
                    BeamLength[j]=hit.distance;
                    //raak ik een gecombineerde laser?
                }
                Lasers[j].transform.localScale=new Vector3(1,0.1f,BeamLength[j]);
            }
        }
        //kort de laseranimatie in van de gecombineerde
        if (!Lasers [4].gameObject.activeSelf)
        {
            ShootLength[4]=0;
            charge[4]=0;
        }
        if (!Lasers [5].gameObject.activeSelf)
        {
            ShootLength[5]=0;
            charge[5]=0;
        }
        if (!Lasers [6].gameObject.activeSelf)
        {
            ShootLength[6]=0;
            charge[6]=0;
        }
        //print(charge [4]);
	}
}
