using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PhantomBrigade.Data;

namespace PB_EquipmentTypeRarity.Patches
{

    [HarmonyPatch(typeof(CIViewBaseCustomizationSelector), "Start")]
    public static class GroupByAlphaPatch
    {
        public static bool Prepare()
        {
            return ModEntry.ModSettings.EnableGroupByAlpha;
        }

        public static void Postfix(CIViewBaseCustomizationSelector __instance, List<CIViewBaseCustomizationSelector.SortingMode> ___sortingModes)
        {

            try
            {
                //-- Get the existing sub system sort.
                MethodInfo groupSubSystemSortMethodInfo = AccessTools.Method(typeof(CIViewBaseCustomizationSelector), "CompareSubsystemForSortingByGroup");
                Func<EquipmentEntity, EquipmentEntity, int, int> groupSubSystemSortMethodInfoFunction = (Func<EquipmentEntity, EquipmentEntity, int, int>)groupSubSystemSortMethodInfo.CreateDelegate(typeof(Func<EquipmentEntity, EquipmentEntity, int, int>), __instance);

                //--Create the sort mode.

                ___sortingModes.Add(
                    new CIViewBaseCustomizationSelector.SortingMode()
                    {
                        key = "groupByAlpha",
                        text = "Alpha Group",
                        funcPart = ComparePartForSortingByGroupAlpha,
                        funcSubsystem = groupSubSystemSortMethodInfoFunction,
                    });


            }
            catch (Exception ex)
            {
                FileLog.Log(ex.ToString());
            }
        }

        private static int ComparePartForSortingByGroupAlpha(EquipmentEntity part1, EquipmentEntity part2, int orderMultiplier)
        {
            try
            {

                int result = string.Compare(GetGroupLastSegment(part1), GetGroupLastSegment(part2), StringComparison.InvariantCulture);

                if (result != 0) return result * orderMultiplier * -1;

                //---The original game code for the group sort, minus the grouping

                //Removing original grouping sort.
                //result = string.Compare(
                //    part1.hasDataLinkPartPreset ? part1.dataLinkPartPreset.data.groupMainKey : string.Empty, 
                //    part2.hasDataLinkPartPreset ? part2.dataLinkPartPreset.data.groupMainKey : string.Empty, 
                //    StringComparison.InvariantCultureIgnoreCase);

                result = DataHelperStats.GetAveragePartLevel(part1).CompareTo(DataHelperStats.GetAveragePartLevel(part2));
                if (result != 0) return result * orderMultiplier;

                result = string.Compare(
                    part1.partBlueprint.sockets.FirstOrDefault<string>(),
                    part2.partBlueprint.sockets.FirstOrDefault<string>(),
                    StringComparison.InvariantCulture);

                if (result != 0) return result;

                result = string.Compare(
                    part1.hasDataKeyPartPreset ? part1.dataKeyPartPreset.s : (string)null,
                    part2.hasDataKeyPartPreset ? part2.dataKeyPartPreset.s : (string)null,
                    StringComparison.InvariantCulture);

                if (result != 0) return result * -orderMultiplier;

                return string.Compare(part1.nameInternal.s, part2.nameInternal.s, StringComparison.InvariantCultureIgnoreCase) * -orderMultiplier;

            }
            catch (Exception ex)
            {
                FileLog.Log(ex.ToString());
                throw;
            }
        }


        private static string GetGroupLastSegment(EquipmentEntity part)
        {
            string partGroup = part.hasDataLinkPartPreset ? part.dataLinkPartPreset.data.groupMainKey : string.Empty;
            int index = partGroup.LastIndexOf('_');
            if (index != -1)
            {
                return partGroup.Substring(index + 1);
            }
            return partGroup;
        }

    }
}
