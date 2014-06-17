// Adapted from aspnetwebstack - https://github.com/ASP-NET-MVC/aspnetwebstack

//    Copyright (c) Microsoft Open Technologies, Inc.  All rights reserved.
//    Microsoft Open Technologies would like to thank its contributors, a list
//    of whom are at http://aspnetwebstack.codeplex.com/wikipage?title=Contributors.
//
//    Licensed under the Apache License, Version 2.0 (the "License"); you
//    may not use this file except in compliance with the License. You may
//    obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
//    implied. See the License for the specific language governing permissions
//    and limitations under the License.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace RezRouting.Utility.Expressions
{
    public static class ControllerActionExpressionHelper
    {
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "This is an appropriate nesting of generic types")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "Users cannot use anonymous methods with the LambdaExpression type")]
        public static RouteValueDictionary GetRouteValuesFromExpression<TController>(Expression<Action<TController>> action) where TController : Controller
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            MethodCallExpression call = action.Body as MethodCallExpression;
            if (call == null)
            {
                throw new ArgumentException("Action must be a method call", "action");
            }

            string actionName = GetTargetActionName(call.Method);

            var rvd = new RouteValueDictionary();
            rvd.Add("Action", actionName);
            
            AddParameterValuesFromExpressionToDictionary(rvd, call);
            return rvd;
        }

        // This method contains some heuristics that will help determine the correct action name from a given MethodInfo
        // assuming the default sync / async invokers are in use. The logic's not foolproof, but it should be good enough
        // for most uses.
        private static string GetTargetActionName(MethodInfo methodInfo)
        {
            string methodName = methodInfo.Name;

            // do we know this not to be an action?
            if (methodInfo.IsDefined(typeof(NonActionAttribute), true /* inherit */))
            {
                throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                                                                  "Cannot call non-action: {0}", methodName));
            }

            // has this been renamed?
            ActionNameAttribute nameAttr = methodInfo.GetCustomAttributes(typeof(ActionNameAttribute), true /* inherit */).OfType<ActionNameAttribute>().FirstOrDefault();
            if (nameAttr != null)
            {
                return nameAttr.Name;
            }

            // targeting an async action?
            if (methodInfo.DeclaringType.IsSubclassOf(typeof(AsyncController)))
            {
                if (methodName.EndsWith("Async", StringComparison.OrdinalIgnoreCase))
                {
                    return methodName.Substring(0, methodName.Length - "Async".Length);
                }
                if (methodName.EndsWith("Completed", StringComparison.OrdinalIgnoreCase))
                {
                    throw new InvalidOperationException(String.Format(CultureInfo.CurrentCulture,
                                                                      "Cannot call completed method: {0}", methodName));
                }
            }

            // fallback
            return methodName;
        }

        private static void AddParameterValuesFromExpressionToDictionary(RouteValueDictionary rvd, MethodCallExpression call)
        {
            ParameterInfo[] parameters = call.Method.GetParameters();

            if (parameters.Length > 0)
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    Expression arg = call.Arguments[i];
                    object value = null;
                    ConstantExpression ce = arg as ConstantExpression;
                    if (ce != null)
                    {
                        // If argument is a constant expression, just get the value
                        value = ce.Value;
                    }
                    else
                    {
                        value = CachedExpressionCompiler.Evaluate(arg);
                    }
                    rvd.Add(parameters[i].Name, value);
                }
            }
        }
    }
}
