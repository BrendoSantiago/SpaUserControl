using Microsoft.Practices.Unity;
using SpaUserControl.Domain.Contracts.Services;
using SpaUserControl.Startup;
using System;
using System.Globalization;
using System.Threading;

namespace SpaUserControl.Spammer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Idioma
            CultureInfo ci = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var container = new UnityContainer();
            DependencyResolver.Resolve(container);

            var service = container.Resolve<IUserService>();
            try
            {
                service.Register("Brendo Santiago", "brendosantiago@outlook.com.br", "12345678", "12345678");
                Console.WriteLine("Usuário cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                service.Dispose();
            }
            Console.ReadKey();
        }
    }
}