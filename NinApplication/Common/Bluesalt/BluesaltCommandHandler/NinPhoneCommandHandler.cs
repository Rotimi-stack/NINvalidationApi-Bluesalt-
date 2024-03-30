using MediatR;
using Nin.Application.Common.Bluesalt.BluesaltCommand;
using Nin.Application.Common.Interface;
using Nin.Application.Common.Model.Bluesalt.Resources;
using Nin.Application.Common.Responses.BluesaltResponse;

namespace Nin.Application.Common.Bluesalt.BluesaltCommandHandler
{
    public class NinPhoneCommandHandler : IRequestHandler<NinPhoneCommand, NinPhResponse>
    {
        private readonly IBluesaltServiceInterface _bs;

        public NinPhoneCommandHandler(IBluesaltServiceInterface bs)
        {
            _bs = bs;
        }
        public async Task<NinPhResponse> Handle(NinPhoneCommand request, CancellationToken cancellationToken)
        {
            var data = new NinPhoneResource
            {
                phone_number = request.phone_number,
            };
            return await _bs.NinPhoneValidation(data);
        }
    }
}
