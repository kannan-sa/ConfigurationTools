using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations;

public static class AnimationExtension
{
    public static void ActivateAndPreserveOffset(this AimConstraint aimConstraint) {

        if (aimConstraint == null) {
            Debug.LogError("Constraint instance is null.");
            return;
        }

        Type type = aimConstraint.GetType();

        MethodInfo method = type.GetMethod("ActivateAndPreserveOffset",
            BindingFlags.Instance | BindingFlags.NonPublic);

        if (method == null) {
            Debug.LogError("Method 'ActivateAndPreserveOffset' not found on " + type.FullName);
            return;
        }

        try {
            method.Invoke(aimConstraint, null);
            // Debug.Log("ActivateAndPreserveOffset invoked successfully.");
        }
        catch (Exception ex) {
            Debug.LogException(ex);
        }
    }
}
