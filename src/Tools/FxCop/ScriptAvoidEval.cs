﻿// ScriptAvoidEval.cs
// Script#/Tools/FxCop
// Copyright (c) Nikhil Kothari.
// Copyright (c) Microsoft Corporation.
// This source code is subject to terms and conditions of the Microsoft 
// Public License. A copy of the license can be found in License.txt.
//

using System;
using System.Diagnostics;
using Microsoft.FxCop.Sdk;

namespace ScriptSharp.FxCop {

    public sealed class ScriptAvoidEval : BaseIntrospectionRule {

        public ScriptAvoidEval() :
            base(typeof(ScriptAvoidEval).Name,
                 typeof(ScriptAvoidEval).Namespace + ".RuleData",
                 typeof(ScriptAvoidEval).Assembly) {
        }

        public override ProblemCollection Check(Member member) {
            Visit(member);
            return Problems;
        }

        public override void VisitMethodCall(MethodCall call) {
            Method method = ((MemberBinding)call.Callee).BoundMember as Method;

            if ((method != null) &&
                (method.DeclaringType.FullName == "System.Script") &&
                (method.Name.Name == "Eval")) {
                Problems.Add(new Problem(GetResolution(), call.SourceContext));
            }
        }
    }
}
