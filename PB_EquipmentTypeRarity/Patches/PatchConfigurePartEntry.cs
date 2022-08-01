using HarmonyLib;
using PhantomBrigade.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PB_EquipmentTypeRarity.Patches
{
    [HarmonyPatch(typeof(CIViewBaseCustomizationSelector), "ConfigurePartEntry")]
    public static class PatchConfigurePartEntry
    {
        public static bool Prefix(CIViewBaseCustomizationSelector __instance,
            EquipmentEntity part,
            CIHelperEquipmentSelector block,
            bool showSelector)
        {
            int id = part.id.id;
            HashSet<string> tags = part.tagCache.tags;
            string partModelName = part.GetPartModelName();
            string equipmentGroupName = part.GetEquipmentGroupName();
            int num1 = Mathf.RoundToInt(DataHelperStats.GetAveragePartLevel(part));
            int num2 = part.hasRating ? part.rating.i : 1;
            string ratingString = DataHelperStats.GetRatingString(num2);
            Color averageLevelColor = DataHelperStats.GetAverageLevelColor(num2);
            string hexTagRgba = UtilityColor.ToHexTagRGBA(averageLevelColor);


            //----Start Actual Change
            string itemString = ModEntry.ModSettings.ShowRarity
                ? $"{partModelName}\n{equipmentGroupName} / {hexTagRgba}{ratingString}[-][aa]"
                : $"{partModelName}\n{equipmentGroupName}";

            block.labelLeft.text = itemString;

            //----End Actual Change

            block.labelLeftLevel.text = num1.ToString();
            block.spriteIconAddition.enabled = part.isInventoryAddition;
            block.spriteOverlay.color = averageLevelColor;
            if (showSelector)
            {

                block.buttonRight.gameObject.SetActive(true);
                if (block.buttonRight.callbackOnClick == null)
                {
                    MethodInfo selectionToggleMethodInfo = AccessTools.Method(typeof(CIViewBaseCustomizationSelector), "OnEquipmentSelectionToggle", 
                        new Type[] { typeof(int) });

                    Delegate OnEquipmentSelectionToggleDelegate = selectionToggleMethodInfo.CreateDelegate(typeof(Action<int>), __instance);

                    block.buttonRight.callbackOnClick = new UICallback((Action<int>) OnEquipmentSelectionToggleDelegate, id);
                }
                else
                    block.buttonRight.callbackOnClick.argumentInt = id;
            }
            else
                block.buttonRight.gameObject.SetActive(false);

            return false;
        }

    }
}