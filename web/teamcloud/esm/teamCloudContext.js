/*
 * Copyright (c) Microsoft Corporation.
 * Licensed under the MIT License.
 *
 * Code generated by Microsoft (R) AutoRest Code Generator.
 * Changes may cause incorrect behavior and will be lost if the code is regenerated.
 */
import * as coreClient from "@azure/core-client";
export class TeamCloudContext extends coreClient.ServiceClient {
    /**
     * Initializes a new instance of the TeamCloudContext class.
     * @param credentials Subscription credentials which uniquely identify client subscription.
     * @param $host server parameter
     * @param options The parameter options
     */
    constructor(credentials, $host, options) {
        if (credentials === undefined) {
            throw new Error("'credentials' cannot be null");
        }
        if ($host === undefined) {
            throw new Error("'$host' cannot be null");
        }
        // Initializing default values for options
        if (!options) {
            options = {};
        }
        const defaults = {
            requestContentType: "application/json; charset=utf-8",
            credential: credentials
        };
        const packageDetails = `azsdk-js-teamcloud/1.0.0-beta.1`;
        const userAgentPrefix = options.userAgentOptions && options.userAgentOptions.userAgentPrefix
            ? `${options.userAgentOptions.userAgentPrefix} ${packageDetails}`
            : `${packageDetails}`;
        if (!options.credentialScopes) {
            options.credentialScopes = ["openid"];
        }
        const optionsWithDefaults = Object.assign(Object.assign(Object.assign({}, defaults), options), { userAgentOptions: {
                userAgentPrefix
            }, baseUri: options.endpoint || "{$host}" });
        super(optionsWithDefaults);
        // Parameter assignments
        this.$host = $host;
    }
}
//# sourceMappingURL=teamCloudContext.js.map