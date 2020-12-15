/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using TeamCloud.Data;
using TeamCloud.Model.Commands;
using TeamCloud.Model.Commands.Core;
using TeamCloud.Model.Data;

namespace TeamCloud.Orchestrator.Handlers
{
    public sealed class ComponentCommandHandler
        : ICommandHandler<ComponentCreateCommand>,
          ICommandHandler<ComponentUpdateCommand>,
          ICommandHandler<ComponentDeleteCommand>
    {
        private readonly IComponentRepository componentRepository;
        private readonly IComponentDeploymentRepository componentDeploymentRepository;

        public ComponentCommandHandler(IComponentRepository componentRepository, IComponentDeploymentRepository componentDeploymentRepository)
        {
            this.componentRepository = componentRepository ?? throw new ArgumentNullException(nameof(componentRepository));
            this.componentDeploymentRepository = componentDeploymentRepository ?? throw new ArgumentNullException(nameof(componentDeploymentRepository));
        }

        public async Task<ICommandResult> HandleAsync(ComponentCreateCommand command, IAsyncCollector<ICommand> commandQueue, IDurableClient durableClient = null)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (commandQueue is null)
                throw new ArgumentNullException(nameof(commandQueue));

            var commandResult = command.CreateResult();

            try
            {
                commandResult.Result = await componentRepository
                    .AddAsync(command.Payload)
                    .ConfigureAwait(false);

                if (commandResult.Result.Type == Model.Data.ComponentType.Environment)
                {
                    var componentDeployment = new ComponentDeployment()
                    {
                        ComponentId = commandResult.Result.Id,
                        ProjectId = commandResult.Result.ProjectId
                    };

                    componentDeployment = await componentDeploymentRepository
                        .AddAsync(componentDeployment)
                        .ConfigureAwait(false);


                    await commandQueue
                        .AddAsync(new ComponentDeploymentExecuteCommand(command.User, componentDeployment))
                        .ConfigureAwait(false);
                }

                await commandQueue
                    .AddAsync(new ComponentMonitorCommand(command.User, command.Payload))
                    .ConfigureAwait(false);

                commandResult.RuntimeStatus = CommandRuntimeStatus.Completed;
            }
            catch (Exception exc)
            {
                commandResult.Errors.Add(exc);
            }

            return commandResult;
        }

        public async Task<ICommandResult> HandleAsync(ComponentUpdateCommand command, IAsyncCollector<ICommand> commandQueue, IDurableClient durableClient = null)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (commandQueue is null)
                throw new ArgumentNullException(nameof(commandQueue));

            var commandResult = command.CreateResult();

            try
            {
                commandResult.Result = await componentRepository
                    .SetAsync(command.Payload)
                    .ConfigureAwait(false);

                commandResult.RuntimeStatus = CommandRuntimeStatus.Completed;
            }
            catch (Exception exc)
            {
                commandResult.Errors.Add(exc);
            }

            return commandResult;
        }

        public async Task<ICommandResult> HandleAsync(ComponentDeleteCommand command, IAsyncCollector<ICommand> commandQueue, IDurableClient durableClient = null)
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (commandQueue is null)
                throw new ArgumentNullException(nameof(commandQueue));

            var commandResult = command.CreateResult();

            try
            {
                commandResult.Result = await componentRepository
                    .RemoveAsync(command.Payload)
                    .ConfigureAwait(false);

                commandResult.RuntimeStatus = CommandRuntimeStatus.Completed;
            }
            catch (Exception exc)
            {
                commandResult.Errors.Add(exc);
            }

            return commandResult;
        }
    }
}
