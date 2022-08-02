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
        public static void Postfix(
            EquipmentEntity part,
            CIHelperEquipmentSelector block)
        {
            string partModelName = part.GetPartModelName();
            string equipmentGroupName = part.GetEquipmentGroupName();
            int num2 = part.hasRating ? part.rating.i : 1;
            string ratingString = DataHelperStats.GetRatingString(num2);
            Color averageLevelColor = DataHelperStats.GetAverageLevelColor(num2);
            string hexTagRgba = UtilityColor.ToHexTagRGBA(averageLevelColor);

            //----Start Actual Change
            string itemString = ModEntry.ModSettings.ShowRarity
                ? $"{partModelName}\n{equipmentGroupName} / {hexTagRgba}{ratingString}[-][aa]"
                : $"{partModelName}\n{equipmentGroupName}";

            block.labelLeft.text = itemString;
        }

    }
}