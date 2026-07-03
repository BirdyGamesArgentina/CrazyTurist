using System.Collections.Generic;
using UnityEngine;

namespace PathFinding
{
    public class Node : MonoBehaviour
    {
        // lo que importa
        [SerializeField] List<Node> neighbors;
        public List<Node> Neighbors
        {
            get
            {
                return neighbors;
            }
        }


        // conexion de camino, reconstruccion del camino
        Node parent;

        public Node GetParent()
        {
            return parent;
        }
        public void SetParent(Node _parent)
        {
            parent = _parent;
        }
        public void Clear()
        {
            parent = null;
            costo = float.MaxValue;
            costoFinal = float.MaxValue;
            isOpen = false;
            isVisited = false;
        }


        [SerializeField] float detectionRadius = 2f;
        [SerializeField] LayerMask floorsAndObstacles;
        [SerializeField] LayerMask nodes;
        [SerializeField] LayerMask nodeMask;

        [Range(0f, 2f)][SerializeField] float floorOffset = 1f;

        [SerializeField] float maxSlope = 0.5f;

        [Header("Gizmos")]
        [SerializeField] bool drawsphere;
        [SerializeField] float sphereRadius = 0.2f;
        [SerializeField] bool drawDetection;
        [SerializeField] bool drawConnections;

        public float costo = 999f;
        public float costoFinal = 999f;

        [Range(0, 3)]
        public int environmental = 0;

        public bool isVisited = false;
        public bool isOpen = false;

        private void Start()
        {
            //var rig = this.gameObject.GetComponent<Rigidbody>();
            //rig.isKinematic = true;
            //var col = this.gameObject.GetComponent<Collider>();
            //col.enabled = false;
        }


        /////////////////// Esto se ejecuta en Modo Edicion
        public void Bake()
        {
            Adjust();
            Detect();
        }

        void Adjust()
        {
            if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out RaycastHit hit, 20, floorsAndObstacles))
            {
                transform.position = hit.point + Vector3.up * floorOffset;
            }
        }
        void Detect()
        {
            neighbors = new List<Node>();

            Collider[] cols = Physics.OverlapSphere(transform.position, detectionRadius, nodes);

            for (int i = 0; i < cols.Length; i++)
            {
                Node node = cols[i].GetComponent<Node>();

                if (node != null && node != this)
                {
                    Vector3 dir = node.transform.position - transform.position;

                    Ray ray = new Ray();
                    ray.origin = transform.position;
                    ray.direction = dir;


                    if (Physics.Raycast(ray, out RaycastHit hit, dir.magnitude, nodeMask))
                    {
                        //  Lo Estoy Viendo
                        var hitted = hit.collider.GetComponent<Node>();
                        if (hitted != null && hitted == node)
                        {

                            float h = node.transform.position.y - transform.position.y;

                            // 5.4 Mathf.Abs => 5.4
                            // -3.5 Mathf.Abs => 3.5
                            if (Mathf.Abs(h) < maxSlope)
                            {
                                neighbors.Add(node);
                            }
                        }
                    }
                }
            }
        }


        private void OnDrawGizmos()
        {
            return;

            if (drawsphere)
            {
                Gizmos.color = isOpen ? Color.yellow : (isVisited ? Color.magenta : Color.black);
                Gizmos.DrawSphere(this.transform.position, sphereRadius);
            }

            if (drawDetection)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, detectionRadius);
            }


            if (drawConnections)
            {
                Gizmos.color = Color.white;

                if (neighbors != null)
                {
                    for (int i = 0; i < neighbors.Count; i++)
                    {
                        Vector3 dir = neighbors[i].transform.position - transform.position;
                        dir /= 2;
                        Gizmos.DrawLine(transform.position, transform.position + dir);
                    }
                }
            }


            // Para testear el PathFinder (Logica de Pesos Externos)
            if (environmental <= 0) return;


            if (environmental >= 1)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawCube(transform.position + Vector3.up * 0.2f, Vector3.one * 0.15f);
            }
            if (environmental >= 2)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawCube(transform.position + Vector3.up * 0.3f, Vector3.one * 0.15f);
            }
            if (environmental >= 3)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position + Vector3.up * 0.4f, Vector3.one * 0.15f);
            }
        }
    }

}
