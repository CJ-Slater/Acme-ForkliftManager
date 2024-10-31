using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using System.Windows.Input;
using Domain.Repositories;

namespace Application.Requests.Forklifts
{
    public class DeleteForkliftsRequestHandler(IForkliftRepository repository) : IRequestHandler<DeleteForkliftsRequest, bool>
    {
        public async Task<bool> Handle(DeleteForkliftsRequest request, CancellationToken cancellationToken)
        {
            await repository.DeleteAllAsync();
            return true;
        }
    }
}
