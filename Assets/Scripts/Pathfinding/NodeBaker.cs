using UnityEditor;
using UnityEngine;

namespace PathFinding
{
    [ExecuteInEditMode]
    public class NodeBaker : MonoBehaviour
    {

        public static NodeBaker instance;

        [SerializeField] Node[] nodes;
        public Node[] Nodes { get { return nodes; } }

        [SerializeField] bool bake;
        [SerializeField] bool update;

        void Start()
        {
            if (instance == null) instance = this;
            else Destroy(this.gameObject);
        }


        void Update()
        {
            if (bake || update)
            {
                bake = false;

                nodes = GetComponentsInChildren<Node>();

                for (int i = 0; i < nodes.Length; i++)
                {
                    nodes[i].Bake();
                }
            }

        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged += OnStateChange;
        }
        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnStateChange;
        }

        void OnStateChange(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                this.enabled = false;
            }
        }
    }

}

