// Copyright (c) Microsoft Corporation.
// Licensed under the MIT License.

import { useMutation, useQueryClient } from 'react-query'
import { useNavigate, useParams } from 'react-router-dom';
import { ComponentDefinition } from 'teamcloud';
import { api } from '../API';
import { useOrg, useProject, useProjectComponents } from '.';

export const useCreateProjectComponent = () => {

    const navigate = useNavigate();

    const { orgId, projectId } = useParams() as { orgId: string, projectId: string };

    const { data: org } = useOrg();
    const { data: project } = useProject();
    const { data: components } = useProjectComponents();

    const queryClient = useQueryClient();

    return useMutation(async (componentDef: ComponentDefinition) => {
        if (!project) throw Error('No project')

        const { data } = await api.createComponent(project.organization, project.id, {
            body: componentDef,
            onResponse: (raw, flat) => {
                if (raw.status >= 400)
                    throw new Error(raw.parsedBody || raw.bodyAsText || `Error: ${raw.status}`)
            }
        });

        return data;
    }, {
        onSuccess: data => {
            if (data) {
                queryClient.setQueryData(['org', project?.organization, 'project', project?.id, 'component', data.slug], data)
                queryClient.setQueryData(['org', project?.organization, 'project', project?.id, 'component'], components ? [...components, data] : [data])

                navigate(`/orgs/${org?.slug ?? orgId}/projects/${project?.slug ?? projectId}/components/${data?.slug}`);
            }
        }
    }).mutateAsync
}
