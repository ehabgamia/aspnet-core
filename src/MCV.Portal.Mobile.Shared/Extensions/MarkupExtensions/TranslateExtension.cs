using System;
using MCV.Portal.Core;
using MCV.Portal.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MCV.Portal.Extensions.MarkupExtensions
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (ApplicationBootstrapper.AbpBootstrapper == null || Text == null)
            {
                return Text;
            }

            return L.Localize(Text);
        }
    }
}