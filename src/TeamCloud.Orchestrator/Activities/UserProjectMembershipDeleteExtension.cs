/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using TeamCloud.Model.Data;
using TeamCloud.Orchestration;

namespace TeamCloud.Orchestrator.Activities
{
    internal static class UserProjectMembershipDeleteExtension
    {
        public static Task<User> DeleteUserProjectMembershipAsync(this IDurableOrchestrationContext functionContext, User user, Guid projectId)
            => functionContext.CallActivityWithRetryAsync<User>(nameof(UserProjectMembershipDeleteActivity), (user, projectId));
    }
}
