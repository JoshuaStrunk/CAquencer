using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Cell {
	[System.Serializable]
	public class Pattern {
		public int id;
		public int[] pattern = new int[5];
		
		//p should only be 5 long
		public Pattern(int i, int[] p){
			id = 0;
			pattern = new int[5]{0,0,0,0,0};
		}
	}

	public Color color;
	public int id;
	public List<Pattern> patterns;
	public bool playNote = true;

	public Cell() {
		color = Color.black;
		id = 0;
		patterns = new List<Pattern>();
	}

	public void addPattern(int i, int[] p) {
		patterns.Add(new Pattern(i, p));
	}
	public void removePattern(int i) {
		patterns.RemoveAt(i);
	}
}

