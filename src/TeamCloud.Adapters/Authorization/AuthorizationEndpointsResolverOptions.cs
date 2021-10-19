﻿using System;
using System.Collections.Generic;
using System.Text;
using TeamCloud.Configuration;
using TeamCloud.Configuration.Options;

namespace TeamCloud.Adapters.Authorization
{
    [Options]
    public sealed class AuthorizationEndpointsResolverOptions : IAuthorizationEndpointsResolverOptions
    {
        private readonly EndpointApiOptions endpointApiOptions;

        public AuthorizationEndpointsResolverOptions(EndpointApiOptions endpointApiOptions)
        {
            this.endpointApiOptions = endpointApiOptions ?? throw new ArgumentNullException(nameof(endpointApiOptions));
        }

        public string BaseUrl => endpointApiOptions.Url;
    }
}
