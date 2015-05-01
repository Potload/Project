using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CamTarget : MonoBehaviour {
    public Transform[] Players;
    private Vector3 Middle;
    Vector3 Furthest;
    Vector3 Furthest2;
    float Dis;
    Vector3 Other;
	void Update () 
    {
        Middle = (Players [0].position + Players [1].position + Players [2].position + Players [3].position) / 4;
        transform.position = Middle;
        Dis = 0;
        for (int i=0; i<4; i++)
        {
            if (Vector3.Distance(Players [i].position, Middle) > Dis)
            {
                Furthest = Players [i].position;
                Dis = Vector3.Distance(Players [i].position, Middle);
            }
        }
        Dis = 0;
        for (int j=0; j<4; j++)
        {
            if (Vector3.Distance(Players [j].position, Furthest) > Dis)
            {
                Furthest2 = Players [j].position;
                Dis = Vector3.Distance(Players [j].position, Furthest);
            }
        }

        transform.position = ((Furthest + Furthest2) / 2);
    }
        
}
