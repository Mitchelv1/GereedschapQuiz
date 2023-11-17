using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTool : MonoBehaviour
{
   [Header("Dots")]
   [SerializeField] private GameObject dotPrefab;
   [SerializeField] Transform dotParent;

   [Header("Lines")]
   [SerializeField] Transform lineParent;
   [SerializeField] private GameObject linePrefab;
   private LineController currentLine;

   private void Update() {
    if (Input.GetMouseButtonDown(0)) {

        if (currentLine == null) {
            currentLine = Instantiate(linePrefab, Vector3.zero, Quaternion.identity, lineParent).GetComponent<LineController>();
        }

        GameObject dot = Instantiate(dotPrefab, GetMousePosition(), Quaternion.identity, dotParent);
        currentLine.AddPoint(dot.transform);
    }
   }

   private Vector3 GetMousePosition() {
    Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    worldMousePosition.z = 0;

    return worldMousePosition;
   }
}
