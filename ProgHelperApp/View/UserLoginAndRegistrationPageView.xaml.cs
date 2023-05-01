using ProgHelperApp.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ProgHelperApp.View
{
    /// <summary>
    /// Логика взаимодействия для UserLoginAndRegistrationPageView.xaml
    /// </summary>
    public partial class UserLoginAndRegistrationPageView : Page
    {
        public UserLoginAndRegistrationPageView(Frame MainFrame)
        {
            InitializeComponent();
            DataContext = new UserLoginAndRegistrationPageVM(MainFrame);
        }
    }
}
