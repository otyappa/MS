using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trance : MonoBehaviour {
   public  Renderer Material_Manager;
	// Use this for initialization
	void Start () {
  /*      Material_Manager = this.GetComponent<Renderer>();
        Material m=Material_Manager.material;
        m.SetFloat("_Mode", 2);
        m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        m.SetInt("_ZWrite", 0);
        m.DisableKeyword("_ALPHATEST_ON");
        m.EnableKeyword("_ALPHABLEND_ON");
        m.DisableKeyword("_ALPHAPREMULTIPLY_ON");*/

     //   Debug.Log(Material_Manager.material.color.b);
     //   Material_Manager.material.color = new Color(Material_Manager.material.color.r, Material_Manager.material.color.g, Material_Manager.material.color.b, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
