﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System.Collections.Generic;
using Newtonsoft.Json;
using TeamCloud.Model.Common;
using TeamCloud.Serialization;

namespace TeamCloud.Model.Data
{
    [JsonObject(NamingStrategyType = typeof(TeamCloudNamingStrategy))]
    public interface ITeamCloudInstance : ITags
    {
        string Version { get; set; }

        AzureResourceGroup ResourceGroup { get; set; }

        IList<TeamCloudApplication> Applications { get; set; }
    }
}
