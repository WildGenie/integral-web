using System;
using Integral.Options;
using Integral.Senders;
using Microsoft.Extensions.DependencyInjection;

namespace Integral.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddEmailSender(this IServiceCollection serviceCollection, Action<EmailSenderOptions> configureOptions)
        {
            EmailSenderOptions emailSenderOptions = new EmailSenderOptions();
            configureOptions(emailSenderOptions);

            serviceCollection.AddTransient<EmailSender, SmtpEmailSender>(serviceProvider => new SmtpEmailSender(emailSenderOptions));
            return serviceCollection;
        }
    }
}
