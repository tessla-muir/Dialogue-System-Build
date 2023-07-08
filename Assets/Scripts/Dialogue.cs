using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue", order = 0)]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();

#if UNITY_EDITOR 
        private void Awake()
        {
            if (nodes.Count == 0)
            {
                DialogueNode rootNode = new DialogueNode();
                rootNode.uniqueID = System.Guid.NewGuid().ToString();
                nodes.Add(rootNode);
            }
        }
#endif
        private void OnValidate()
        {
            nodeLookup.Clear(); // Start with a clean state
            foreach (DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.uniqueID] = node;
            }
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        // Returns all child nodes for a given parent node
        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode parent)
        {
            // Dictonary for efficency
            foreach (string childID in parent.children)
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    yield return nodeLookup[childID];
                }
            }
        }

        // Creates a child node for the given parent node
        public void CreateNode(DialogueNode parent)
        {
            DialogueNode newNode = new DialogueNode();
            newNode.rect.position = parent.rect.position + new Vector2(200, 0);
            newNode.uniqueID = System.Guid.NewGuid().ToString();
            parent.children.Add(newNode.uniqueID);
            nodes.Add(newNode);
            OnValidate(); // Updates Bezier curves
        }

        // Deletes a given node
        public void DeleteNode(DialogueNode nodeToRemove)
        {
            nodes.Remove(nodeToRemove);
            OnValidate(); // Updates GUI
            CleanChildren(nodeToRemove);
        }

        // Remove connection of a given node to children
        public void CleanChildren(DialogueNode nodeToClean)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.children.Remove(nodeToClean.uniqueID);
            }
        }
    }
}