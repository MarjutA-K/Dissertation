using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderManager : MonoBehaviour
{
    public List<PlantOrdersSO> orders = new List<PlantOrdersSO>();
    public List<PlantSO> plants = new List<PlantSO>();

    public void CompleteOrder(PlantOrdersSO order)
    {
        bool hasEnoughPlants = true;

        Debug.Log(hasEnoughPlants);

        /*foreach (PlantRequirementSO requirement in order.plantRequirements)
        {
            int requiredQuantity = requirement.quantityRequired;
            int availableQuantity = 0;

            foreach(PlantSO plant in plants)
            {
                if(plant.plantType == requirement.plant)
                {
                    availableQuantity += plant.quantity;
                }
            }

            if(availableQuantity < requiredQuantity)
            {
                hasEnoughPlants = false;
                break;
            }
        }*/

        if(hasEnoughPlants)
        {
            /*foreach(PlantRequirementSO requirement in order.plantRequirements)
            {
                int quantityToRemove = requirement.quantityRequired;
                foreach (PlantSO plant in plants)
                {
                    if(plant.plantType == requirement.plant)
                    {
                        if(quantityToRemove >= plant.quantity)
                        {
                            quantityToRemove -= plant.quantity;
                            plants.Remove(plant);
                            //Destroy()
                            Debug.Log("Destroy");
                        }
                        else
                        {
                            plant.quantity -= quantityToRemove;
                            break;
                        }
                    }
                }
            }*/

            //Add Money

            orders.Remove(order);

            Debug.Log("Order Complited");
        }
        else
        {
            Debug.Log("Not enough Plants to complete order");
        }
    }
}


/*public class GameManager : MonoBehaviour
{
    public PlantInventory inventory;
    public OrderManager orderManager;
    public Text rewardText;

    public void CheckOrderCompletion(Order order)
    {
        bool hasEnoughPlants = true;
        foreach (PlantRequirement requirement in order.plantRequirements)
        {
            int requiredQuantity = requirement.quantity;
            int availableQuantity = 0;
            foreach (Plant plant in inventory.plants)
            {
                if (plant.type == requirement.plantType)
                {
                    availableQuantity += plant.quantity;
                }
            }
            if (availableQuantity < requiredQuantity)
            {
                hasEnoughPlants = false;
                break;
            }
        }
        if (hasEnoughPlants)
        {
            foreach (PlantRequirement requirement in order.plantRequirements)
            {
                foreach (Plant plant in inventory.plants)
                {
                    if (plant.type == requirement.plantType)
                    {
                        plant.quantity -= requirement.quantity;
                        if (plant.quantity <= 0)
                        {
                            inventory.plants.Remove(plant);
                            break;
                        }
                    }
                }
            }
            inventory.GetComponent<AudioSource>().Play(); // Play sound effect
            inventory.GetComponent<PlantInventoryUI>().RefreshUI(); // Refresh UI
            inventory.GetComponent<PlantInventoryUI>().UpdateSelectedPlant(); // Deselect selected plant
            inventory.GetComponent<PlantInventoryUI>().UpdateSelectedOrder(); // Deselect selected order
            inventory.GetComponent<PlantInventoryUI>().UpdatePlantDescription(); // Update plant description panel
            inventory.GetComponent<PlantInventoryUI>().UpdateOrderDescription(); // Update order description panel
            inventory.GetComponent<PlantInventoryUI>().UpdateInventorySizeText(); // Update inventory size text
            inventory.GetComponent<PlantInventoryUI>().UpdateSellButton(); // Update sell button
            inventory.GetComponent<PlantInventoryUI>().UpdateOrderButton(); // Update order button
            rewardText.text = "+" + order.reward.ToString(); // Update reward text
            inventory.GetComponent<CurrencyManager>().AddCurrency(order.reward); // Add reward to currency
            orderManager.orders.Remove(order); // Remove order from order manager
            Destroy(order.gameObject); // Destroy order object
        }
    }
}*/
