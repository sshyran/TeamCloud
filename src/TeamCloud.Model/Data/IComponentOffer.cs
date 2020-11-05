/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using Newtonsoft.Json;
using TeamCloud.Model.Common;
using TeamCloud.Serialization;

namespace TeamCloud.Model.Data
{
    [JsonObject(NamingStrategyType = typeof(TeamCloudNamingStrategy))]
    public interface IComponentOffer : IIdentifiable, IDisplayName
    {
        string ProviderId { get; set; }

        string Description { get; set; }

        string InputJsonSchema { get; set; }

        ComponentScope Scope { get; set; }

        ComponentType Type { get; set; }
    }
}
