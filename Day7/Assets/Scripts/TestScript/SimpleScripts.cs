using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleScripts : MonoBehaviour
{
    public MeshRenderer thisrender;

    public GameObject RotModel;

    public GameObject MoveModel;

    private List<Vector3> OriginTran;

    public GameObject UIIstruc;

    //public GameObject RecoverModel;

    public GameObject sceneobject1;

    public GameObject sceneobject2;

    void Start()

    {
        List<Vector3> cubelist = new List<Vector3>();

        cubelist.Add(sceneobject1.transform.position);
        cubelist.Add(sceneobject2.transform.position);
        cubelist.Add(sceneobject1.transform.eulerAngles);
        cubelist.Add(sceneobject2.transform.eulerAngles);

        OriginTran = cubelist;
        //Debug.Log(OriginPos[0]);

        Debug.Log("test123123");

    }

    public void CallHideUI()
    {
        UIIstruc.SetActive(!UIIstruc.activeInHierarchy);
    }

    public void RecoverAllPos()
    {
        sceneobject1.transform.position = OriginTran[0];
        sceneobject2.transform.position = OriginTran[1];
        sceneobject1.transform.eulerAngles = OriginTran[2];
        sceneobject2.transform.eulerAngles = OriginTran[3];
    }


    public void ChangeColor()

    {
        thisrender.material.color = Color.red;
    }
    public void ClearColor()
    {
        thisrender.material.color = Color.white;
    }

    public void Rotatemodel()
    {
        RotModel.transform.eulerAngles += new Vector3(0,30,0);
    }

    public void MovePos()

    {     
            MoveModel.transform.position = new Vector3(MoveModel.transform.position.x, MoveModel.transform.position.y - 0.04f, MoveModel.transform.position.z);
        
     
    }

    public void ButtonRecover()
    {
        MoveModel.transform.position = new Vector3(MoveModel.transform.position.x, MoveModel.transform.position.y + 0.04f, MoveModel.transform.position.z);
    }

}
