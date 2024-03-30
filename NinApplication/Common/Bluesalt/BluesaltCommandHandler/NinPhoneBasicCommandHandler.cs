using MediatR;
using Nin.Application.Common.Bluesalt.BluesaltCommand;
using Nin.Application.Common.Interface;
using Nin.Application.Common.Model.Bluesalt.Resources;
using Nin.Application.Common.Responses.BluesaltResponse;

namespace Nin.Application.Common.Bluesalt.BluesaltCommandHandler
{
    public class NinPhoneBasicCommandHandler : IRequestHandler<NinPhoneBasicCommand, NinPhoneBasicResponse>
    {

        private readonly IBluesaltServiceInterface _bs;

        public NinPhoneBasicCommandHandler(IBluesaltServiceInterface bs)
        {
            _bs = bs;
        }
        public async Task<NinPhoneBasicResponse> Handle(NinPhoneBasicCommand request, CancellationToken cancellationToken)
        {
            var data = new NinPhonebasicValidationResource
            {
                phone_number = request.phone_number,

            };
            return await _bs.NinPhoneValidationBasic(data);
        }
    }
}
