using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WorkTracker.Database.DTO;
using WorkTracker.Database.DTOs;
using WorkTracker.Database.Models;

namespace WorkTracker.Server.AutoMapper
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<OwnerDTO, Owner>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<JobDTO, Job>();
            CreateMap<Job, JobDTO>();
            CreateMap<WorkerDTO, Worker>();
            CreateMap<Worker, WorkerDTO>();
            CreateMap<AssignmentDTO, Assignment>();
            CreateMap<Assignment, AssignmentDTO>();
            CreateMap<List<Assignment>, List<AssignmentDTO>>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<Comment, CommentDTO>();
        }
    }
}
