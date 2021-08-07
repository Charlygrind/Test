using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test.Services.Navigation
{
    public class NavigationService : INavigationService
    {

        public NavigationParameters Params = new NavigationParameters();

        public NavigationService()
        {
            Params = new NavigationParameters();
        }

        public virtual Page GetCurrentPage()
        {
            var MainPage = Application.Current.MainPage;

            return MainPage != null &&
                MainPage.Navigation != null &&
                MainPage.Navigation.NavigationStack != null &&
                MainPage.Navigation.NavigationStack.Count() > 0 ? MainPage.Navigation.NavigationStack.Last() : null;
        }
        public NavigationParameters GetParameters()
        {
            return Params;
        }
        public void ClearParameters()
        {
            Params = new NavigationParameters();
        }
        public virtual void SetMain(Page page, bool Navigation)
        {
            if (Navigation)
                Application.Current.MainPage = new NavigationPage(page);
            else
                Application.Current.MainPage = page;
        }
        public virtual bool CanGoBack()
        {
            var MainPage = Application.Current.MainPage;
            return MainPage != null &&
                MainPage.Navigation != null &&
                MainPage.Navigation.NavigationStack != null &&
                MainPage.Navigation.NavigationStack.Count > 1;
        }
        public virtual async Task<Page> GoBack()
        {
            var MainPage = Application.Current.MainPage;
            if (CanGoBack())
                return await MainPage.Navigation.PopAsync();
            return null;
        }
        public virtual async Task<Page> GoBack(bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (CanGoBack())
                return await MainPage.Navigation.PopAsync(animated);
            return null;
        }

        public virtual async Task NavigateTo(Page page)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushAsync(page);
        }
        public virtual async Task NavigateTo(Page page, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushAsync(page, animated);
        }
        public virtual async Task NavigateTo(Page page, NavigationParameters parameter)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushAsync(page);
            }
        }
        public virtual async Task NavigateTo(Page page, NavigationParameters parameter, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushAsync(page, animated);
            }
        }

        public virtual async Task PopToRootAsync()
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PopToRootAsync();
        }
        public virtual async Task PopToRootAsync(bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PopToRootAsync(animated);
        }

        public virtual void RemovePage(Page page)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                MainPage.Navigation.RemovePage(page);
        }
        public virtual void InsertPageBefore(Page page, Page before)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                MainPage.Navigation.InsertPageBefore(page, before);
        }

        public virtual async Task<Page> PopModalAsync()
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                return await MainPage.Navigation.PopModalAsync();
            return null;
        }
        public virtual async Task<Page> PopModalAsync(bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                return await MainPage.Navigation.PopModalAsync(animated);
            return null;
        }

        public virtual async Task PushModalAsync(Page page)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushModalAsync(page);
        }
        public virtual async Task PushModalAsync(Page page, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushModalAsync(page, animated);
        }
        public virtual async Task PushModalAsync(Page page, NavigationParameters parameter)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushModalAsync(page);
            }
        }
        public virtual async Task PushModalAsync(Page page, NavigationParameters parameter, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushModalAsync(page, animated);
            }
        }

        public virtual async Task<Page> PopAsync()
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                return await MainPage.Navigation.PopAsync();
            return null;
        }
        public virtual async Task<Page> PopAsync(bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                return await MainPage.Navigation.PopAsync(animated);
            return null;
        }
        public virtual async Task<Page> PopAsync(NavigationParameters parameter,bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                return await MainPage.Navigation.PopAsync(animated);
            }
                
            return null;
        }

        public virtual async Task PushAsync(Page page)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushAsync(page);
        }
        public virtual async Task PushAsync(Page page, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
                await MainPage.Navigation.PushAsync(page, animated);
        }
        public virtual async Task PushAsync(Page page, NavigationParameters parameter)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushAsync(page);
            }
        }
        public virtual async Task PushAsync(Page page, NavigationParameters parameter, bool animated)
        {
            var MainPage = Application.Current.MainPage;
            if (MainPage != null && MainPage.Navigation != null)
            {
                Params = parameter;
                await MainPage.Navigation.PushAsync(page, animated);
            }
        }

        public Page GetPageByName(string pageName)
        {
            var page =  new Page();
            
            Type type = (from asm in AppDomain.CurrentDomain.GetAssemblies()
                            from objectType in asm.GetTypes()
                            where objectType.IsClass && objectType.Name == pageName
                            select objectType).Single();
            if (type == null)
            {
                throw new ArgumentException(string.Format("No such page: {0}.", pageName), "pageName");
            }
            ConstructorInfo constructor = type.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => !c.GetParameters().Any());
            if (constructor == null)
            {
                throw new InvalidOperationException("No suitable constructor found for page " + pageName);
            }
            page = constructor.Invoke(null) as Page;
            
            return page;
        }

    }
}
