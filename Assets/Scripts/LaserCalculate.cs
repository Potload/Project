using UnityEngine;
using System.Collections;
//Bodhi Donselaar 2015
public class LaserCalculate : MonoBehaviour {
    public static bool[] Shoot;         //WRITE PlayerControll verteld of de speler schiet
    public static float[] Hoek;         //READ Output de hoek van de laser voor speler[]
    public static float[] CoolDown;     //READ/WRITE De Cooldown waarde 0 tot 1
    public static float[] CoolingDown;  //READ/WRITE Hoger dan 0 betekend dat hij gaat afkoelen
    public static bool[] OverHeated;    //READ Oververhit, gaat op false als hij afgekoeld is
    public static float[] Disable;      //Korte disable na een combine
    public GameObject[] Players;        //
    public Transform[] Lasers;          //
    public float CombinedChargeSpeed=1; //hoe snel de combined tevoorschijn komt
    public float UberChargeSpeed=1;     //hetzelfde als hierboven voor uber
    public float LaserDistance = 20;    //Max Distance lasers
    public float CoolDownRefill = 1;    //Afkoelsnelheid
    public float CoolDownDrain = 1;     //Drainsnelheid
    private float[] CoolDownEase;       //Easewaarde van afkoelen
    public float[] BeamLength;          //Hoe lang de lasers zijn
    private bool[] Combined;             //of een laser gecombined is
    public static int[] Partner;        //Met welke laser een laser gecombined is, -1 bij geen
    public static int[] CombinedSource; //Welke laser de source is voor combined laser 1 en 2
    public float[] ShootLength;  //0 tot 1, tevoorschijn komen van de lasers
    public float[] charge;              //opladen combined
    private Ray ray;                    //De ray die voor alle raycasten word gebruikt
    private RaycastHit hit;             //De hit die voor alle raycasten word gebruikt
    private bool[] CombinedActive;      //Of een gecombineerde laser deze frame bestaad

    //Priority Check
    public int COMB1SOURCE = -1;
    private int COMB1PARTNER=-1;
    public int COMB2SOURCE = -1;
    private int COMB2PARTNER=-1;

    private int OTHER1=-1;
    private int OTHER2 = -1;
    
    void Start () 
    {

        Shoot = new bool[4];
        ShootLength = new float[7];
        BeamLength=new float[6];
        Combined = new bool[6];
        Partner = new int[4];
        CombinedActive = new bool[3];
        CombinedSource = new int[2];
        charge = new float[3];
        Hoek = new float[4];
        Disable = new float[4];
        CoolDown = new float[4];
        CoolingDown = new float[4];
        CoolDownEase = new float[4];
        OverHeated = new bool[4];
        charge [0] = 0;
        charge [1] = 0;
        charge [2] = 0;
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
        BeginWaardes();
        PriorityCheck();
        CombinedLoop();
        FinalLoop();
    }
    
    private void BeginWaardes()
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
            CooldownFunction(h);
        }
        CombinedActive [0] = false;
        CombinedActive [1] = false;
        CombinedActive [2] = false;
        Combined [4] = false;
        Combined [5] = false;
        Lasers[4].transform.localScale=new Vector3(1,10,0);
        Lasers[5].transform.localScale=new Vector3(1,10,0);
    }

    private void PriorityCheck()
    {
        if (COMB1SOURCE == -1 && COMB2SOURCE == -1)
        {
            FirstLoop(0);
            FirstLoop(1);
            FirstLoop(2);
            FirstLoop(3);
        } else
        {
            if (COMB1SOURCE!=-1&&COMB2SOURCE!=-1)
            {
                //Lasers[4].gameObject.SetActive(true);
                //Lasers[5].gameObject.SetActive(true);

                Lasers[5].transform.localScale=new Vector3(1,10,BeamLength[5]);
                Lasers[COMB2SOURCE].transform.localScale=new Vector3(1,0.1f,0);
                Lasers[COMB2PARTNER].transform.localScale=new Vector3(1,0.1f,0);
                FirstLoop(COMB1SOURCE);
                FirstLoop(COMB1PARTNER);
                Lasers[5].transform.localScale=new Vector3(1,10,0);
                Lasers[4].transform.localScale=new Vector3(1,10,BeamLength[4]);
                Lasers[COMB2SOURCE].transform.localScale=new Vector3(1,0.1f,BeamLength[COMB2SOURCE]);
                Lasers[COMB2PARTNER].transform.localScale=new Vector3(1,0.1f,BeamLength[COMB2PARTNER]);
                FirstLoop(COMB2SOURCE);
                FirstLoop(COMB2PARTNER);
            }
            else
            {
                Lasers[4].gameObject.SetActive(true);
                OTHER1=0;
                OTHER2=0;
                while(OTHER1==COMB1SOURCE||OTHER1==COMB1PARTNER)
                {
                    OTHER1+=1;
                }
                while(OTHER2==COMB1SOURCE||OTHER2==COMB1PARTNER||OTHER1==OTHER2)
                {
                    OTHER2+=1;
                }
                Lasers[OTHER1].transform.localScale=new Vector3(1,0.1f,0);
                Lasers[OTHER2].transform.localScale=new Vector3(1,0.1f,0);
                FirstLoop(COMB1SOURCE);
                FirstLoop(COMB1PARTNER);
                Lasers[4].transform.localScale=new Vector3(1,10,BeamLength[4]);
                Lasers[OTHER1].transform.localScale=new Vector3(1,0.1f,BeamLength[OTHER1]);
                Lasers[OTHER2].transform.localScale=new Vector3(1,0.1f,BeamLength[OTHER2]);
                FirstLoop(OTHER1);
                FirstLoop(OTHER2);
            }
            
        }
        //alle lasers weer normale lengte voor de combined check
        Lasers[0].transform.localScale=new Vector3(1,0.1f,BeamLength[0]);
        Lasers[1].transform.localScale=new Vector3(1,0.1f,BeamLength[1]);
        Lasers[2].transform.localScale=new Vector3(1,0.1f,BeamLength[2]);
        Lasers[3].transform.localScale=new Vector3(1,0.1f,BeamLength[3]);

        //deactivate lasers als ze niet meer bestaan na de eerste loop, reset charge
        Lasers[4].gameObject.SetActive(CombinedActive[0]);
        if (!CombinedActive [0])
        {
            COMB1SOURCE=-1;
            COMB2SOURCE=-1;
            BeamLength[4]=0;
            ShootLength[4]=0;
            charge[0]=0;
        }
        Lasers[5].gameObject.SetActive(CombinedActive[1]);
        if (!CombinedActive [1])
        {
            COMB2SOURCE=-1;
            BeamLength[5]=0;
            ShootLength[5]=0;
            charge[1]=0;
        }
    }
    
    private void FirstLoop(int i)
    {
            if (Disable[i]>0)
            {
                Shoot[i]=false;
                Disable[i]-=Time.deltaTime;
            }
            
            if (!Combined[i]&&Shoot[i]) //ben ik al gecombined? zoja, doe geen nieuwe raycast
            {
                int layerMask = 1 << Players[i].gameObject.layer;
                layerMask = ~layerMask;
                Hoek[i]=0;
                ray = new Ray(Players[i].transform.position,Players[i].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[i]*ShootLength[i],layerMask)) //raak ik iets?
                { 
				if (hit.collider.gameObject.tag == "Enemy"){

					hit.collider.gameObject.GetComponent<Enemy>().LaserNoCombo();

				}
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
                                    //switch van eigenaar
                                    if (COMB1SOURCE!=i)
                                    {
                                        if (COMB2SOURCE==i)
                                    {
                                        float Temp=BeamLength[4];
                                        BeamLength[4]=BeamLength[5];
                                        BeamLength[5]=Temp;
                                        Temp=ShootLength[4];
                                        ShootLength[4]=ShootLength[5];
                                        ShootLength[5]=Temp;
                                        Temp=charge[0];
                                        charge[0]=charge[1];
                                        charge[1]=Temp;
                              
                                    }
                                    else
                                    {
                                        BeamLength[4]=0;
                                        ShootLength[4]=0;
                                        charge[0]=0;
                                    }
                                    }
                                    COMB1SOURCE=i;
                                    COMB1PARTNER=HitLayer;
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
                                    //switch van eigenaar
                                    if (COMB2SOURCE!=i)
                                    {
                                        BeamLength[5]=0;
                                        ShootLength[5]=0;
                                        charge[1]=0;
                                    }
                                    COMB2SOURCE=i;
                                    COMB2PARTNER=HitLayer;
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
                            
                        }
                    }
                }
            Lasers[i].transform.localScale=new Vector3(1,0.1f,BeamLength[i]);
            //maak zelf en partner langer tijdelijk als ze gecombined zijn zodat er geen straal tussendoor kan
            if (Combined[i])
            {
                Lasers[i].transform.localScale=new Vector3(1,0.1f,BeamLength[i]+1);
                Lasers[Partner[i]].transform.localScale=new Vector3(1,0.1f,BeamLength[Partner[i]]+1);
            }
        }
        
        

    }
    
    private void CombinedLoop()
    {
        for (int k=0; k<2; k++)//Raycast gecombineerde lasers
        {
            if (CombinedActive[k]&&!Combined[k+4])
            {
                
                int layerMask = 1 << Lasers[k+4].gameObject.layer;
                layerMask = ~layerMask;
                
                ray = new Ray(new Vector3(Lasers[k+4].transform.position.x,Lasers[k+4].transform.position.y+0.1f,Lasers[k+4].transform.position.z),Lasers[k+4].transform.forward);  
                
                charge[k]+=Time.deltaTime*CombinedChargeSpeed;
                if (charge[k]>1)
                {
                    ShootLength[k+4]=Mathf.Lerp(ShootLength[k+4],1,Time.deltaTime*10);
                }
                BeamLength[k+4]=LaserDistance*ShootLength[k+4]; //als ie niks raakt is ie nog steeds maximaal laserdistance
                if(Physics.Raycast(ray.origin,ray.direction, out hit, LaserDistance*BeamLength[k+4],layerMask)) //raak ik iets?
                {
					if (hit.collider.gameObject.tag == "Enemy"){
						
						hit.collider.gameObject.GetComponent<Enemy>().LaserCombined();
						
					}
                    int HitLayer=hit.collider.gameObject.layer-Players[0].gameObject.layer;
                    BeamLength[k+4]=hit.distance*ShootLength[k+4];
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
    }
    
    private void FinalLoop()
    {
        for (int j=0; j<4; j++)//4x voor iedere laser, tweede loop, nieuwe raycast voor ongecombineerde en check of ze een gecombineerde raken
        {
            if (!Combined[j]&&Shoot[j])//als een laser niet gecombined is
            {
                int layerMask = 1 << Players[j].gameObject.layer;
                layerMask = ~layerMask;             
                ray = new Ray(Players[j].transform.position,Players[j].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[j],layerMask)) //raak ik iets?
				
				if (hit.collider.gameObject.tag == "Enemy"){
					
					hit.collider.gameObject.GetComponent<Enemy>().LaserFinal();
					
				}
				
				{ 
                    BeamLength[j]=hit.distance;
                    //raak ik een gecombineerde laser?
                }
                Lasers[j].transform.localScale=new Vector3(1,0.1f,BeamLength[j]);
            }
        }
    }
    
    private void CooldownFunction(int h)
    {
        //cooldown aanvullen
        if (CoolDown [h] < 1 && CoolingDown [h] > 1)
        {
            CoolDown [h] += Time.deltaTime * CoolDownRefill * CoolDownEase [h];              
            if (CoolDown [h] > 1)
            {
                CoolDown [h] = 1;
                CoolingDown [h] = 0;
                OverHeated [h] = false;
            }
        }
        if (Shoot [h] == false && CoolDown [h] < 1)
        {
            CoolingDown [h] += Time.deltaTime;
            CoolDownEase [h] += Time.deltaTime;
        }
        if (Shoot [h])
        {
            CoolDownEase [h] = 0;
            CoolDown [h] -= Time.deltaTime * CoolDownDrain;
            if (CoolDown [h] < 0)
            {
                CoolDown [h] = 0;
                CoolingDown [h] = 0.1f; 
                OverHeated [h] = true;
            }
        }
        if (CoolDown [h] < 0)
        {
            CoolDown [h] = 0;
            OverHeated [h] = true;
        }
        if (OverHeated [h] && CoolDown [h] < 0.1)
        {
            xbox.VL [h] = 1;
            xbox.VR [h] = 1;
        }
    }
}