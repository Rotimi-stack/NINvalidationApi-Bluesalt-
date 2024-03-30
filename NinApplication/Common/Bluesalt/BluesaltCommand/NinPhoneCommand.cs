using MediatR;
using Nin.Application.Common.Responses.BluesaltResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nin.Application.Common.Bluesalt.BluesaltCommand
{
    public  class NinPhoneCommand:IRequest<NinPhResponse>
    {
        public string phone_number { get; set; } = string.Empty;
    }
}
