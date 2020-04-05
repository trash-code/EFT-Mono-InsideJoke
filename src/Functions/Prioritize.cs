using EFT;
using EFT.Interactive;
using Exception5.Functions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Exception6.Functions
{
    class Prioritize
    {
        // ADD MORE OVERLOADS IF NEEDED
        public static List<Player> SortData(List<Player> p)
        {
            return p.OrderBy(tempPlayer => Vector2.Distance(MaociScreen.centerOfScreen, Camera.main.WorldToScreenPoint(tempPlayer.Transform.position))).ToList();
        }
        public static List<WorldInteractiveObject> SortData(List<WorldInteractiveObject> p)
        {
            return p.OrderBy(temp => Vector2.Distance(MaociScreen.centerOfScreen, Camera.main.WorldToScreenPoint(temp.transform.position))).ToList();
        }
        public static List<LootItem> SortByDistance(List<LootItem> lootItem) {
            return lootItem.OrderBy(tempPlayer => Vector3.Distance(Camera.main.transform.position, Camera.main.WorldToScreenPoint(tempPlayer.transform.position))).ToList();
        }
    }
}
