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
            CreateMap<OwnerDTO, Owner>().ReverseMap();
            CreateMap<JobDTO, Job>().ReverseMap();
            CreateMap<WorkerDTO, Worker>().ReverseMap();
            CreateMap<CommentDTO, Comment>().ReverseMap();
            CreateMap<Assignment, AssignmentDTO>().ReverseMap();
        }
    }
}
