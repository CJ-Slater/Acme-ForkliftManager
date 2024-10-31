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
    public class BatchMoveForkliftRequest : IRequest<BatchMoveForkliftResponse>
    {
        public string ForkliftName { get; set; }

        public List<MoveForkliftRequest> Commands { get; set; }
    }
}
