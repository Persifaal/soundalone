using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Application.Interfaces
{
    public interface IEmailTemplateRenderer
    {
        string Render(string templateId, Dictionary<string, string> parameters);
    }
}
