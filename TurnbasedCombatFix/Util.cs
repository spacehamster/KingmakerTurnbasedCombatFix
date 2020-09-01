using HarmonyLib;
using System;
using System.Linq;
using System.Reflection;

namespace TurnbasedCombatFix
{
    public static class Util
    {
        private static MethodBase GetOriginalMethod(this HarmonyMethod attr)
        {
            switch (attr.methodType)
            {
                case MethodType.Normal:
                    if (attr.methodName is null)
                        return null;
                    return AccessTools.DeclaredMethod(attr.declaringType, attr.methodName, attr.argumentTypes);

                case MethodType.Getter:
                    if (attr.methodName is null)
                        return null;
                    return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetGetMethod(true);

                case MethodType.Setter:
                    if (attr.methodName is null)
                        return null;
                    return AccessTools.DeclaredProperty(attr.declaringType, attr.methodName).GetSetMethod(true);

                case MethodType.Constructor:
                    return AccessTools.DeclaredConstructor(attr.declaringType, attr.argumentTypes);

                case MethodType.StaticConstructor:
                    return AccessTools.GetDeclaredConstructors(attr.declaringType)
                        .Where(c => c.IsStatic)
                        .FirstOrDefault();
            }

            return null;
        }
        public static void TogglePatch(Harmony harmony, Type type, bool enabled)
        {
            var methodInfo = HarmonyMethodExtensions.GetMergedFromType(type);
            if (methodInfo.methodType is null) // MethodType default is Normal
                methodInfo.methodType = MethodType.Normal;
            var originalMethod = methodInfo.GetOriginalMethod();
            var patcheInfo = Harmony.GetPatchInfo(originalMethod);
            var patches = patcheInfo
                .Postfixes
                .Concat(patcheInfo.Prefixes)
                .Concat(patcheInfo.Transpilers)
                .Concat(patcheInfo.Finalizers)
                .Where(p => p.PatchMethod.DeclaringType == type)
                .ToList();
            if (enabled && patches.Count() == 0)
            {
                Main.Log($"Patching {type.Name}");
                var patchProcessor = harmony.CreateClassProcessor(type);
                patchProcessor.Patch();
            }
            if (enabled && patches.Count() > 0)
            {
                Main.Log($"{type.Name} already applied");
            }
            if (!enabled && patches.Count() > 0)
            {
                Main.Log($"Unpatching {type.Name}");
                foreach (var patch in patches)
                {
                    harmony.Unpatch(originalMethod, patch.PatchMethod);
                }
            }
            if (!enabled && patches.Count() == 0)
            { 
                Main.Log($"{type.Name} already not applied");
            }
        }
    }
}
