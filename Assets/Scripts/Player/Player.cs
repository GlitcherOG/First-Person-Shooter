using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    //Game mode
    [SerializeField] int playersTeamID;
    public int teamID { get { return playersTeamID; } }
    public bool Flag;
    public static Player Instance;
    public GameObject HolderFlag;
    public GameObject flagObject;


    public override void OnStartAuthority()
    {
        Instance = this;
    }
    [ClientCallback]
    private void Update()
    {

    }


    public void IsHoldingFlag(GameObject flag = null)
    {
        Flag = !Flag;
        if (flag == null)
        {
            flagObject.transform.parent = null;
            flagObject = null;
        }
        else
        {
            flagObject = flag;
            flagObject.transform.parent = HolderFlag.transform;
            flagObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }


}
