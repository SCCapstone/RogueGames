using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookDropdown : MonoBehaviour {

  public Dropdown dropdown;
  public Item[] recipesArray;
  public Text recipeDescription;
  public GameObject itemPreview;
  public GameObject mainMenu;
  public GameObject craftingNotebook;

  void Start() {
    PopulateList(recipesArray);
    recipeDescription.text = recipesArray[0].GetItemDescription();
    itemPreview.GetComponent<SpriteRenderer>().sprite = recipesArray[0].icon;
  }

  public void Dropdown_IndexChanged(int index) {
    recipeDescription.text = recipesArray[index].GetItemDescription();
    itemPreview.GetComponent<SpriteRenderer>().sprite = recipesArray[index].icon;
  }

  void PopulateList(Item[] recipesArray) {
    List<string> recipes = new List<string>();
    foreach (var option in recipesArray) {
      recipes.Add(option.name);
    }

    dropdown.AddOptions(recipes);

  }

  public void CloseNotebook() {
    Debug.Log("Notebook closed, returning to Menu");
    craftingNotebook.SetActive(false);
    mainMenu.SetActive(true);
  }



}
