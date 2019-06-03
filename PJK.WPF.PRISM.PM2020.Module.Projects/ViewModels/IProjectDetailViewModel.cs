﻿using System.Threading.Tasks;

namespace PJK.WPF.PRISM.PM2020.Module.Projects.ViewModels
{
    public interface IProjectDetailViewModel
    {
        Task LoadAsync(int projectId);
        bool HasChanges { get; }
    }
}