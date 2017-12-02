using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour {

	public List<Material> EffectMaterials = new List<Material>();

	private void OnRenderImage(RenderTexture src, RenderTexture dest) {
		foreach(Material mat in EffectMaterials){
			if(mat == null) continue;

			Graphics.Blit(src, dest, mat);
		}
	}
}