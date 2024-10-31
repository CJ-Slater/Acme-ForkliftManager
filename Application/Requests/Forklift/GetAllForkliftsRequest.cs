using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Windows.Input;
using Application.Dtos;

namespace Application.Requests.Forklifts
{
    public class GetAllForkliftsRequest : IRequest<List<ForkliftDto>>
    {
    }
}
