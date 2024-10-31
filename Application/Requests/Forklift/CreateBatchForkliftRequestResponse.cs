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
    public class CreateBatchForkliftRequestResponse : IRequest<List<int>>
    {
        public int SuccessfulInsertsCount { get; set; }
        public int FailedInsertsCount { get; set; }
    }
}
