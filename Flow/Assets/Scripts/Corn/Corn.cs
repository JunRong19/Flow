using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Corn", order = 1)]
public class Corn : ScriptableObject {
	public Sprite CornSprite;

    public CornType Type = CornType.None;
}
