using UnityEngine;
using System.Collections;

public class LaserCalculate : MonoBehaviour {
	public GameObject[] Players;
    public Transform[] Lasers;
    public float LaserDistance = 20;
	private Ray ray;  
	private RaycastHit hit;  
    private bool[] CombinedActive;
	public float[] BeamLength;
    public bool[] Combined;
    public int[] Partner;

	void Start () 
	{
		BeamLength=new float[6];
        Combined = new bool[4];
        Partner = new int[4];
        CombinedActive = new bool[2];
	}
	
	void Update () 
	{
        for (int h=0; h<4; h++) //beginwaardes
        {
            BeamLength [h] = LaserDistance;
            Lasers[h].transform.localScale=new Vector3(1,1,BeamLength[h]);
            Combined[h] = false;
            Partner[h]=-1;
        }

        CombinedActive [0] = false;
        CombinedActive [1] = false;

		for (int i=0; i<4; i++)//4x voor iedere laser, eerste loop, combineerd de eerste lasers
        {
            if (!Combined[i]) //ben ik al gecombined? zoja, doe geen nieuwe raycast
            {
                int layerMask = 1 << Players[i].gameObject.layer;
                layerMask = ~layerMask;

                ray = new Ray(Players[i].transform.position,Players[i].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[i],layerMask)) //raak ik iets?
                { 
                    BeamLength[i]=hit.distance; //ik raak iets, dus ik word korter
                    int HitLayer=hit.collider.gameObject.layer-Players[0].gameObject.layer;
                    if (HitLayer>-1&&HitLayer<4) //raak ik een andere laser? hoger dan 3 is een gecombinede
                    {
                        if (HitLayer>i) //moet de code van de andere laser nog uitgevoerd worden?
                        {
                            if (!Combined[HitLayer]) //is de laser die ik raak nog niet gecombined?
                            {
                                BeamLength[HitLayer]=Vector3.Distance(hit.point,hit.collider.transform.position);
                                Lasers[HitLayer].transform.localScale=new Vector3(1,1,BeamLength[HitLayer]);
                                Combined[i]=true;
                                Combined[HitLayer]=true;
                                Partner[i]=HitLayer;
                                Partner[HitLayer]=i;
                                if (!CombinedActive[0])
                                {
                                    CombinedActive[0]=true;
                                    Lasers[4].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                                    Lasers[4].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);
                                }
                                else
                                {
                                    CombinedActive[1]=true;
                                    Lasers[5].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                                    Lasers[5].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);
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
                                    Lasers[Partner[HitLayer]].transform.localScale=new Vector3(1,1,BeamLength[Partner[HitLayer]]);

                                    //combine de nieuwe lasers
                                    BeamLength[HitLayer]=Vector3.Distance(hit.point,hit.collider.transform.position);
                                    Lasers[HitLayer].transform.localScale=new Vector3(1,1,BeamLength[HitLayer]);
                                    Combined[i]=true;
                                    Combined[HitLayer]=true;
                                    Partner[i]=HitLayer;
                                    Partner[HitLayer]=i;
                                    //combined laser 1 nieuwe positie
                                    Lasers[4].position=(Lasers[i].forward+Lasers[HitLayer].forward)*0.5f+hit.point;
                                    Lasers[4].rotation=Quaternion.LookRotation(Lasers[i].forward+Lasers[HitLayer].forward);
                                }
                            }
                        }
                    }
                }
                    Lasers[i].transform.localScale=new Vector3(1,1,BeamLength[i]);
            }
        }

        Lasers[4].gameObject.SetActive(CombinedActive[0]);
        Lasers[5].gameObject.SetActive(CombinedActive[1]);

        for (int k=0; k<2; k++)//Raycast gecombineerde lasers
        {
            int layerMask = 1 << Lasers[k+4].gameObject.layer;
            layerMask = ~layerMask;
                
                ray = new Ray(Lasers[k+4].transform.position,Lasers[k+4].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, LaserDistance,layerMask)) //raak ik iets?
                {
                    BeamLength[k+4]=Vector3.Distance(hit.point,Lasers[k+4].transform.position);
                    Lasers[k+4].transform.localScale=new Vector3(1,1,BeamLength[k+4]);
                }
            
        }

        for (int j=0; j<4; j++)//4x voor iedere laser, tweede loop, nieuwe raycast voor ongecombineerde en check of ze een gecombineerde raken
        {
            if (!Combined[j])//als een laser niet gecombined is
            {
                int layerMask = 1 << Players[j].gameObject.layer;
                layerMask = ~layerMask;
                
                ray = new Ray(Players[j].transform.position,Players[j].transform.forward);  
                if(Physics.Raycast(ray.origin,ray.direction, out hit, BeamLength[j],layerMask)) //raak ik iets?
                { 
                    BeamLength[j]=hit.distance;
                    //raak ik een gecombineerde laser?
                }
                Lasers[j].transform.localScale=new Vector3(1,1,BeamLength[j]);
            }
        }
	}
}
