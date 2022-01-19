﻿/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using TeamCloud.Model.Commands.Core;
using TeamCloud.Model.Data;

namespace TeamCloud.Model.Commands;

public sealed class ProjectTemplateUpdateCommand : UpdateCommand<ProjectTemplate, ProjectTemplateUpdateCommandResult>
{
    public ProjectTemplateUpdateCommand(User user, ProjectTemplate payload)
        : base(user, payload)
    { }
}
