using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton Pattern
    public static EquipmentManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    Inventory inventory;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newItem)
    {
        int slotIdx = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if(currentEquipment[slotIdx] != null)
        {
            oldItem = currentEquipment[slotIdx];
            inventory.Add(oldItem);
        }

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIdx] = newItem;
    }

    public void Unequip(int slotIdx)
    {
        if (currentEquipment[slotIdx] != null)
        {
            Equipment oldItem = currentEquipment[slotIdx];
            inventory.Add(oldItem);

            currentEquipment[slotIdx] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for(int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
