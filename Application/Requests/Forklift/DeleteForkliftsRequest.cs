using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Windows.Input;

namespace Application.Requests.Forklifts
{
    public class DeleteForkliftsRequest : IRequest<bool>
    {
    }
}
