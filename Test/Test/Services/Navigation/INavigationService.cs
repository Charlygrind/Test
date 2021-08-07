using System.Threading.Tasks;
using Xamarin.Forms;

namespace Test.Services.Navigation
{
    public interface INavigationService
    {
        Page GetCurrentPage();
        NavigationParameters GetParameters();
        void ClearParameters();

        void SetMain(Page page, bool Navigation);

        Task<Page> GoBack();
        Task<Page> GoBack(bool animated);

        bool CanGoBack();

        Task PopToRootAsync();
        Task PopToRootAsync(bool animated);

        Task NavigateTo(Page page);
        Task NavigateTo(Page page, bool animated);
        Task NavigateTo(Page page, NavigationParameters parameter);
        Task NavigateTo(Page page, NavigationParameters parameter, bool animated);

        void RemovePage(Page page);
        void InsertPageBefore(Page page, Page before);

        
        Task<Page> PopModalAsync();
        Task<Page> PopModalAsync(bool animated);

        Task PushModalAsync(Page page);
        Task PushModalAsync(Page page, bool animated);
        Task PushModalAsync(Page page, NavigationParameters parameter);
        Task PushModalAsync(Page page, NavigationParameters parameter, bool animated);

        Task<Page> PopAsync();
        Task<Page> PopAsync(bool animated);
        Task<Page> PopAsync(NavigationParameters parameter, bool animated);

        Task PushAsync(Page page);
        Task PushAsync(Page page, bool animated);
        Task PushAsync(Page page, NavigationParameters parameter);
        Task PushAsync(Page page, NavigationParameters parameter, bool animated);

        Page GetPageByName(string pageName);
    }
}
