using System;

namespace ProjectsModule.Model
{
    public interface IProject
    {
        string Comment { get; set; }
        bool Complete { get; set; }
        DateTime Deadline { get; set; }
        int Id { get; set; }
        int Priority { get; set; }
        string ProjectName { get; set; }
        int StatusID { get; set; }
        int SystemId { get; set; }
    }
}