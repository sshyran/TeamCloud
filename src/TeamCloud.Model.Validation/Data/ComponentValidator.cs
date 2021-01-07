/**
 *  Copyright (c) Microsoft Corporation.
 *  Licensed under the MIT License.
 */

using FluentValidation;
using TeamCloud.Model.Data;

namespace TeamCloud.Model.Validation.Data
{
    public sealed class ComponentValidator : AbstractValidator<Component>
    {
        public ComponentValidator()
        {
            RuleFor(obj => obj.Id)
                .MustBeGuid();

            RuleFor(obj => obj.ProjectId)
                .MustBeGuid();

            RuleFor(obj => obj.Provider)
                .NotEmpty();

            RuleFor(obj => obj.Creator)
                .MustBeGuid();
        }
    }
}
