using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject toggleGameObject;

    public void Toggle()
    {
        if (toggleGameObject!=null)
        {
            bool isActive = toggleGameObject.activeSelf;
            toggleGameObject.SetActive(!isActive);
            toggleGameObject.transform.localScale += new Vector3(0,1f,0);
        }
    }
}
