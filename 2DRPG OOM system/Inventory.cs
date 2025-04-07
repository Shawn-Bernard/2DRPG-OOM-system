using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Inventory
{
    public List<Item> inventory = new List<Item>();
    
    private int InventorySlots;  // how many item the inventory can hold

    public int inventorySlots 
    {
        get { return InventorySlots; }

        set { InventorySlots = Math.Max(1, value); }   // the inventory cannot have negative numbers or 0 as slot numbers
    }

    public void SetInventorySlots(int num) 
    {
        inventorySlots = num;
    }

}

