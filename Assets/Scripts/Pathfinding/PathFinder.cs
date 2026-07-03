using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace PathFinding
{
    public class PathFinder : MonoBehaviour
    {


        bool walk = false;
        Node origin = null;
        Node destiny = null;

        Vector3 desired = Vector3.zero;
        Vector3 steering = Vector3.zero;
        Vector3 velocity = Vector3.zero;

        void Update()
        {
            if (walk)
            {
                desired = path[index].transform.position - transform.position;

                if (desired.magnitude < PathFindingOptions.instance.closeDist)
                {
                    index++;
                    if (index >= path.Count)
                    {
                        index = 0;
                        walk = false;

                        OnFinish.Invoke();
                    }
                }
                else
                {
                    desired = desired.normalized * PathFindingOptions.instance.speed;

                    // calculo de Avoidance

                    steering = desired - velocity;
                    steering = Vector3.ClampMagnitude(steering, PathFindingOptions.instance.steeringForce);
                    velocity += steering;
                    velocity = Vector3.ClampMagnitude(velocity, PathFindingOptions.instance.speed);

                    transform.position += velocity * Time.deltaTime;
                    transform.forward = velocity;
                }
            }
        }

        int index = 0;
        List<Node> path = new List<Node>();

        Action OnFinish = delegate { };
        public void CallbackOnFinish(Action OnFinish)
        {
            this.OnFinish = OnFinish;
        }
        public void GoToPosition(Vector3 posToGo)
        {
            origin = null;
            destiny = null;

            path.Clear();

            origin = FindMostClosestNode(this.transform.position);
            destiny = FindMostClosestNode(posToGo);

            if (origin == null || destiny == null) 
            {
                Debug.Log("Origen nulo: " + origin == null);
                Debug.Log("destiny nulo: " + destiny == null);
                walk = false; return; 
            }

            path = AStar(origin, destiny);
            if (path.Count > 0)
            {
                walk = true;
            }
            

            //StartCoroutine(AStarStepByStep(origin, destiny, OnGetPath).GetEnumerator());
        }

        void OnGetPath(List<Node> path)
        {
            if (path.Count > 0)
            {
                this.path = path;
                walk = true;
            }
        }

        List<Node> AStar(Node initial, Node final)
        {
            foreach (var n in NodeBaker.instance.Nodes)
            {
                n.Clear();
            }

            List<Node> visited = new List<Node>();
            PriorityQueue<Node> abiertos = new PriorityQueue<Node>();

            initial.costo = 0;
            initial.costoFinal = Vector3.Distance(initial.transform.position, final.transform.position);

            abiertos.Enqueue(initial, initial.costoFinal);
            initial.isOpen = true;

            while (abiertos.Count > 0)
            {
                Node current = abiertos.Dequeue();
                current.isOpen = false;

                if (current == final)
                {
                    return Reconstruct(initial, final);
                }

                visited.Add(current);
                current.isVisited = true;

                foreach (var n in current.Neighbors)
                {
                    if (visited.Contains(n)) continue;

                    float newCost = current.costo + Vector3.Distance(current.transform.position, n.transform.position);

                    if (newCost < n.costo)
                    {
                        n.SetParent(current);
                        n.costo = newCost;

                        float H = Vector3.Distance(n.transform.position, final.transform.position);

                        n.costoFinal = n.costo + H;

                        abiertos.Enqueue(n, n.costoFinal);
                        current.isOpen = false;
                    }
                }

            }

            return null;

        }

        IEnumerable AStarStepByStep(Node initial, Node final, Action<List<Node>> path)
        {
            index = 0;

            foreach (var n in NodeBaker.instance.Nodes)
            {
                n.Clear();
            }

            List<Node> visited = new List<Node>();
            PriorityQueue<Node> abiertos = new PriorityQueue<Node>();

            initial.costo = 0;
            initial.costoFinal = Vector3.Distance(initial.transform.position, final.transform.position);

            abiertos.Enqueue(initial, initial.costoFinal);
            initial.isOpen = true;

            while (abiertos.Count > 0)
            {
                Node current = abiertos.Dequeue();
                current.isOpen = false;

                if (current == final)
                {
                    path.Invoke(ReconstructTheta(initial, final));

                    yield break;
                }

                visited.Add(current);
                current.isVisited = true;

                foreach (var n in current.Neighbors)
                {
                    if (visited.Contains(n)) continue;

                    yield return new WaitForSeconds(0.05f);

                    float newCost = current.costo + Vector3.Distance(current.transform.position, n.transform.position) + n.environmental;

                    if (newCost < n.costo)
                    {
                        n.SetParent(current);
                        n.costo = newCost;

                        float H = Vector3.Distance(n.transform.position, final.transform.position);

                        n.costoFinal = n.costo + H;

                        abiertos.Enqueue(n, n.costoFinal);
                        current.isOpen = false;
                    }
                }

            }

            yield return null;

        }

        List<Node> Dijkstra(Node initial, Node final)
        {
            PriorityQueue<Node> open = new PriorityQueue<Node>(); // para desencolar el proximo
            List<Node> visited = new List<Node>(); // para no revisitar algo que ya calculé

            foreach (Node node in NodeBaker.instance.Nodes)
            {
                node.Clear();
            }

            initial.costo = 0;
            open.Enqueue(initial, initial.costo);

            while (open.Count > 0)
            {
                Node current = open.Dequeue();

                if (current == final)
                {
                    return Reconstruct(initial, final);
                }

                visited.Add(current);

                foreach (var n in current.Neighbors)
                {
                    if (visited.Contains(n)) continue;

                    float newCost = current.costo + Vector3.Distance(current.transform.position, n.transform.position);

                    if (newCost < n.costo)
                    {
                        n.SetParent(current);
                        n.costo = newCost;
                        open.Enqueue(n, newCost);
                    }
                }
            }

            return null;
        }


        List<Node> BFS(Node initial, Node final)
        {
            Queue<Node> open = new Queue<Node>(); // para desencolar el proximo
            List<Node> visited = new List<Node>(); // para no revisitar algo que ya calculé

            //semilla para el while
            open.Enqueue(initial);
            visited.Add(initial);

            while (open.Count > 0)
            {
                Node current = open.Dequeue();

                if (current == final)
                {
                    return Reconstruct(initial, final);
                }

                foreach (Node n in current.Neighbors) // Explode
                {
                    if (visited.Contains(n)) continue;

                    n.SetParent(current); // la conexion es para hacer el Reconstruct
                    visited.Add(n);
                    open.Enqueue(n);
                }
            }

            return null;
        }

        List<Node> Reconstruct(Node initial, Node final)
        {
            List<Node> list = new List<Node>();

            // como semilla el nodo final
            Node current = final;

            while (current != null && current != initial)
            {
                list.Add(current);
                current = current.GetParent();
            }

            list.Add(initial);
            list.Reverse();
            return list;
        }

        

        bool OnShight(Node a, Node b)
        {
            Vector3 offset = Vector3.up * PathFindingOptions.instance.sphereCastRadius;

            if (a == null) throw new System.Exception("Nodo A no existe");
            if (b == null) throw new System.Exception("Nodo A no existe");

            Vector3 dir = (b.transform.position + offset) - (a.transform.position + offset);
            float magnitude = dir.magnitude;

            Ray ray = new Ray(a.transform.position, dir);

            return !Physics.SphereCast(ray, PathFindingOptions.instance.sphereCastRadius, magnitude, PathFindingOptions.instance.thetaObstacle);

        }




        List<Node> ReconstructTheta(Node initial, Node final)
        {
            List<Node> list = new List<Node>();

            // como semilla el nodo final
            Node current = final;

            while (current != null && current != initial)
            {
                list.Add(current);

                var prev = current.GetParent();
                var best = prev;

                while (prev != null && OnShight(current, prev))
                {

                    best = prev;
                    prev = prev.GetParent();
                }

                current = best;
            }

            list.Add(initial);
            list.Reverse();
            return list;
        }

        float mostClose;
        Node best;
        Node FindMostClosestNode(Vector3 point)
        {
            Collider[] cols = Physics.OverlapSphere(point, PathFindingOptions.instance.detectionRadius, PathFindingOptions.instance.layerNode);

            best = null;
            mostClose = PathFindingOptions.instance.detectionRadius + 1;

            for (int i = 0; i < cols.Length; i++)
            {
                Node node = cols[i].GetComponent<Node>();

                if (node != null)
                {
                    Vector2 dir = point - node.transform.position;

                    if (dir.magnitude < mostClose)
                    {
                        mostClose = dir.magnitude;
                        best = node;
                    }
                }
            }

            return best;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            if (origin) Gizmos.DrawSphere(origin.transform.position, 0.3f);
            if (destiny) Gizmos.DrawSphere(destiny.transform.position, 0.3f);
        }

    }

    public class PriorityQueue<T>
    {
        List<PriorityPair> list;

        public int Count { get { return list.Count; } }

        public PriorityQueue()
        {
            list = new List<PriorityPair>();
        }

        public void Enqueue(T _data, float priority)
        {
            // checkeo si ya existia para no generar duplicados
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].data.Equals(_data))
                {
                    // si queremos podemos checkear si la nueva prioridad es mas chica

                    list[i].UpdatePriority(priority);
                    list = list
                        .OrderBy(a => a.priority)
                        .ToList();
                    return;
                }
            }

            PriorityPair pair = new PriorityPair(_data, priority);
            list.Add(pair);

            list = list
                .OrderBy(a => a.priority)
                .ToList();
        }

        public T Dequeue()  /// lo agarra, lo remueve y lo devuelve
        {
            T element = list[0].data;
            list.RemoveAt(0);
            return element;
        }

        public T Peek() /// lo agarra, y lo devuelve (solo muestro el proximo)
        {
            T element = list[0].data;
            //list.RemoveAt(0);
            return element;
        }


        struct PriorityPair
        {
            public T data;
            public float priority;

            public PriorityPair(T _data, float _prior)
            {
                this.data = _data;
                this.priority = _prior;
            }

            // por si tengo que overridear por uno mejor
            public void UpdatePriority(float _newPriority)
            {
                this.priority = _newPriority;
            }
        }
    }

    #region Para click
    //void OnEndClick()
    //{
    //    Vector3 posToGo = Target.Position;
    //    print(posToGo);

    //    origin = null;
    //    destiny = null;

    //    path.Clear();
    //    walk = false;

    //    origin = FindMostClosestNode(this.transform.position);
    //    destiny = FindMostClosestNode(posToGo);

    //    if (origin == null || destiny == null) { walk = false; return; }

    //    StartCoroutine(AStarStepByStep(origin, destiny, OnGetPath).GetEnumerator());

    //    calcular BFS, Dijkstra, AStar, ThetaStar
    //     path = AStar(origin, destiny);

    //    if (path.Count > 0)
    //    {
    //        walk = true;
    //    }
    //}
    #endregion

}