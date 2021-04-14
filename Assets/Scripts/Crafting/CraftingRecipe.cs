using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CraftingRecipe/baseRecipe")]
public class CraftingRecipe : Item {
  public Item result;
  public Ingredient[] ingredients;

  private bool CanCraft() {
    foreach (Ingredient ingredient in ingredients) {
      bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item.name, ingredient.amount);

      if (!containsCurrentIngredient) {
        return false;
      }
    }

    return true;
  }

  private void RemoveIngredientsFromIventory() {
    foreach (Ingredient ingredient in ingredients) {
      Inventory.instance.RemoveItems(ingredient.item.name, ingredient.amount);
    }
  }

  public override void Use() {
    if (CanCraft()) {
      //remove items
      RemoveIngredientsFromIventory();

      //Replenish Durability of Items
      if (result.name == "Sword") {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<Player>().replenishWeapon(result.name, 1f);

      } else if (result.name == "Arrow") {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<Player>().replenishAmmo("Bow");
      } else if (result.name == "Potion") {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        playerObj.GetComponent<Player>().healPlayer(2);
        Debug.Log("You just crafted a Pot");
      }


      Debug.Log("You just crafted a: " + result.name);
    } else {
      Debug.Log("You dont have enaugh ingredients to craft: " + result.name);
    }
  }

  public override string GetItemDescription() {
    string itemIngredients = "";

    foreach (Ingredient ingredient in ingredients) {
      itemIngredients += "- " + ingredient.amount + " " + ingredient.item.name + "\n";
    }

    return itemIngredients;
  }


  [System.Serializable]
  public class Ingredient {
    public Item item;
    public int amount;
  }
}
