using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileEngine : MonoBehaviour {

	public enum Type {
		Unset,
		Empty,
		Wall
	}

	public Dictionary<Type, Sprite> sprites = new Dictionary<Type, Sprite>();
	public Vector2 mapSize;
	public Sprite defaultSprite;
	public Vector2 currentPosition;
	public Vector2 viewPortSize;
	public GameObject tile;

	private Type[,] map;


	// Use this for initialization
	void Start () {
		map = new Type[(int)mapSize.x,(int)mapSize.y];
		initDefaultSprites ();
		initTileMap ();
		addTilesToWorld ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void initDefaultSprites() {
		foreach (Type type in Type.GetValues(typeof(Type))) {
			if (sprites.ContainsKey (type) == false) {
				sprites.Add (type, defaultSprite);
			}
		}
	}

	private void initTileMap ()
	{
		for (int x = 0; x < mapSize.x; ++x) {
			for (int y = 0; y < mapSize.y; ++y) {
				map[x, y] = (Type)Random.Range(0, 3);
			}
		}
	}

	private void addTilesToWorld ()
	{
		int size = (int)tile.GetComponent<Collider2D> ().bounds.size.x;
		for (int x = 0; x < mapSize.x; ++x) {
			for (int y = 0; y < mapSize.y; ++y) {
				GameObject t = Instantiate (tile);
				t.transform.position = new Vector2 (x * size, y * size);
				Debug.Log (t.transform.position);
				Type type = map [x, y];
				Sprite sprite = sprites[type];
				t.GetComponent<SpriteRenderer> ().sprite = sprite;
			}
		}
	}
}
