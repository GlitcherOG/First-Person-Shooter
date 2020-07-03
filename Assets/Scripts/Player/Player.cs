using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class Player : NetworkBehaviour
{
    //Game mode
    [SerializeField] int playersTeamID;
    public int teamID { get { return playersTeamID; } }
    public bool Flag;
    public float health = 100;
    public static Player Instance;
    public Text text;
    public GameObject HolderFlag;
    public GameObject flagObject;

    /// <summary>
    /// If the player is the owner of this object
    /// </summary>
    public override void OnStartAuthority()
    {
        Instance = this;
    }

    [ClientCallback]
    private void Update()
    {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
        text.text = health.ToString();
    }

    /// <summary>
    /// Toggles the plays holding flag state
    /// </summary>
    /// <param name="flag"></param>
    public void IsHoldingFlag(GameObject flag = null)
    {
        Flag = !Flag;
        if (flag == null)
        {
            flagObject.transform.parent = null;
            flagObject.transform.position = flagObject.GetComponent<Flag>().originalLocation;
            flagObject = null;
        }
        else
        {
            flagObject = flag;
            flagObject.transform.parent = HolderFlag.transform;
            flagObject.transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    public void damage(float dam)
    {
        health -= dam;
    }

}
