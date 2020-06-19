using System;
using Integral.Options;
using Integral.Senders;
using Microsoft.AspNetCore.Authentication;
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

        public static IServiceCollection AddExternalAuth(this IServiceCollection serviceCollection, Action<ExternalAuthOptions> configureOptions)
        {
            ExternalAuthOptions externalAuthOptions = new ExternalAuthOptions();
            configureOptions(externalAuthOptions);

            AuthenticationBuilder authenticationBuilder = serviceCollection.AddAuthentication();
            if (!string.IsNullOrWhiteSpace(externalAuthOptions.MicrosoftKey))
            {
                authenticationBuilder.AddMicrosoftAccount(microsoftOptions =>
                {
                    microsoftOptions.ClientId = externalAuthOptions.MicrosoftKey;
                    microsoftOptions.ClientSecret = externalAuthOptions.MicrosoftSecret;
                });
            }

            if (!string.IsNullOrWhiteSpace(externalAuthOptions.FacebookKey))
            {
                authenticationBuilder.AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = externalAuthOptions.FacebookKey;
                    facebookOptions.AppSecret = externalAuthOptions.FacebookSecret;
                });
            }

            if (!string.IsNullOrWhiteSpace(externalAuthOptions.TwitterKey))
            {
                authenticationBuilder.AddTwitter(twitterOptions =>
                {
                    twitterOptions.ConsumerKey = externalAuthOptions.TwitterKey;
                    twitterOptions.ConsumerSecret = externalAuthOptions.TwitterSecret;
                });
            }

            if (!string.IsNullOrWhiteSpace(externalAuthOptions.GoogleKey))
            {
                authenticationBuilder.AddGoogle(googleOptions =>
                {
                    googleOptions.ClientId = externalAuthOptions.GoogleKey;
                    googleOptions.ClientSecret = externalAuthOptions.GoogleSecret;
                });
            }

            return serviceCollection;
        }
    }
}
