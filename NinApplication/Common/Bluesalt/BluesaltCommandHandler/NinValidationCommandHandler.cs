using Nin.Application.Common.Model.Bluesalt.Resources;
using MediatR;
using Nin.Application.Common.Bluesalt.BluesaltCommand;
using Nin.Application.Common.Interface;
using Nin.Application.Common.Responses.BluesaltResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Nin.Application.Common.Bluesalt.BluesaltCommandHandler
{
    public class NinValidationCommandHandler : IRequestHandler<NinValidationCommand, NinResponse>
    {
        private readonly IBluesaltServiceInterface _bs;

        public NinValidationCommandHandler(IBluesaltServiceInterface bs)
        {
            _bs = bs;
        }

        public async Task<NinResponse> Handle(NinValidationCommand request, CancellationToken cancellationToken)
        {
            var data = new NinValidationResource
            {
                nin_number = request.nin_number,
                phone_number = request.phone_number,
            };
            return await _bs.NinValidation(data);
        }
    }
}
