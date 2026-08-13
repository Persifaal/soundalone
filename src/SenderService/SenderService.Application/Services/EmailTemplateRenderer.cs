using SenderService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SenderService.Application.Services
{
    public class EmailTemplateRenderer : IEmailTemplateRenderer
    {
        public string Render(string templateId, Dictionary<string, string> parameters)
        {
            return templateId switch
            {
                "auth.email_confirmation" => RenderEmailConfirmation(parameters),
                _ => throw new InvalidOperationException($"Unknown template: {templateId}")
            };
        }

        private string RenderEmailConfirmation(Dictionary<string, string> parameters) 
        {
            var code = parameters.GetValueOrDefault("code", "000000");

            return $@"
            <html>
                <body style='font-family: Arial, sans-serif;'>
                    <h2>Подтверждение почты</h2>
                    <p>Здравствуйте!</p>
                    <p>Ваш код подтверждения:</p>
                    <h1 style='color: #007bff; font-size: 32px;'>{code}</h1>
                    <p>Код действителен 10 минут.</p>
                </body>
            </html>";
        }
    }
}
