using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using studyApp.Common;

using studyApp.Views.SubView;
using JsonDataPack;
using System.Windows.Forms;
using System.Configuration;

namespace studyApp.Views
{
    /// <summary>
    /// Page1.xaml の相互作用ロジック
    /// </summary>
    public partial class LoginView : Page
    {
        public LoginView()
        {
            InitializeComponent();
        }


        private static Mainpage Screen = null;
        private void loginOKButton_Click(object sender, RoutedEventArgs e)
        {
            var jsonMember = new ReadJsonDataClass.ReadMember();
            var jsonGrade = new ReadJsonDataClass.ReadGrade();
            string enployeeNumber = enployeeNumberTextField.Text;
            string password = passwordTextField.Password;
            var member = jsonMember.ReadOwnMember(enployeeNumber, password);

           
               
            System.Windows.Application.Current.Properties["Menber"] = 111;
            //次のページへ
            if (Screen == null)
            {
                // 次に表示するページを生成、以後使いまわし
                Screen = new Mainpage();
            }
            System.Windows.Application.Current.Properties["Grade"] = jsonGrade.ReadFile("111");
            // Mainpageへ移動
            this.NavigationService.Navigate(Screen);
               
        }
    }
}

      
