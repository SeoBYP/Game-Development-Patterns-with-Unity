using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chapter.Adapter
{
    public class ClientAdapter : MonoBehaviour
    {
        public InventoryItem item;
        private InventorySystem _inventorySystem;
        private IInventorySystem _inventorySystemAdapter;

        private void Start()
        {
            _inventorySystem = new InventorySystem();
            _inventorySystemAdapter = new InventorySystemAdapter();
        }

        private void OnGUI()
        {
            if(GUILayout.Button("Add item(no Adapter)"))
            {
                _inventorySystem.AddItem(item);
            }
            if (GUILayout.Button("Add item (with Adapter)"))
                _inventorySystemAdapter.AddItem(item, SaveLocation.Both);
        }
    }
}
