using Application.Dtos;
using Domain.Enums;
using Domain.Structs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.Forklift
{
    public class BatchMoveForkliftResponse
    {
        public ForkliftPosition FinalPosition { get; set; }

        public List<string> MovementResults { get; set; }
    }
}
