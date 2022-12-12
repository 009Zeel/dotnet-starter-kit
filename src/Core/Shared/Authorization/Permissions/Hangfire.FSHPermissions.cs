﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.WebApi.Shared.Authorization.Permissions;
public static partial class FSHPermissions
{
    public static partial class Hangfire
    {
        public static readonly FSHPermission View = new("View Hangfire", FSHAction.View, FSHResource.Hangfire);
    }
}
