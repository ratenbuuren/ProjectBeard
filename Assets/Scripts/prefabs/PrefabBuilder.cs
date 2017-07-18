using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Prefabs : MonoBehaviour {
  public class PrefabBuilder {

    private GameObject obj;
    private GameObject template;

    public PrefabBuilder(GameObject template) {
      this.template = template;
      this.obj = Instantiate(template);
    }

    public PrefabBuilder position(float x, float y) {
      return position(new Vector2(x, y));
    }

    public PrefabBuilder position(Vector2 pos) {
      obj.transform.position = pos;
      obj.name = String.Format ("{0} [{1},{2}]", template.name, pos.x, pos.y);
      return this;
    }
 
    public PrefabBuilder parent(GameObject parent) {
      obj.transform.parent = parent.transform;
      return this;
    }

    public PrefabBuilder rotate(float angle) {
      obj.transform.Rotate(new Vector3(0, 0, angle));
      return this;
    }

    public GameObject build() {
      return obj;
    }
  }
}
