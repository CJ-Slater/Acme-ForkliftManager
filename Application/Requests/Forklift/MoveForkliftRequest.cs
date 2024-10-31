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
    public class MoveForkliftRequest : IRequest<ForkliftPosition>
    {
        public string ForkliftName { get; set; }
        public int Distance { get; set; }
        public RotationDirection RotationDirection { get; set; }
        public int Degrees { get; set; }
    }
}
