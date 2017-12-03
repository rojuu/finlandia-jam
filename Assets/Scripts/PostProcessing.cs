using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class PostProcessing : MonoBehaviour {

	public Material EffectMaterial;

	private void OnRenderImage(RenderTexture src, RenderTexture dest) {
		Graphics.Blit(src, dest, EffectMaterial);
	}
}