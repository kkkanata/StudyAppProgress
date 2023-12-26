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
using System.Text.Json;
using System.Text.Json.Serialization;
using studyApp.Common;
using JsonDataPack;



namespace studyApp.Views
{
    /// <summary>
    /// kekkagamen_sippai.xaml の相互作用ロジック
    /// </summary>
    public partial class Result_Failed : Page
    {
        public Result_Failed()
        {
            InitializeComponent();
            IList<JsonDataClass.RescueRequestData> rescue = (IList<JsonDataClass.RescueRequestData>)Application.Current.Properties["RescuRecest"];
            int Num = (int)Application.Current.Properties["next"];
            DataSearch dataSearch = new DataSearch();
            JsonDataClass.Grade.RescueRequestState res = (JsonDataClass.Grade.RescueRequestState)Application.Current.Properties["Rescue"];
            // 表示する文字列格納用
            string com = "";
            // ミスの表示
            if (res.miss[0].mChoices.Length > 0)
            {
                com += "ミス\n";
            }
            for (int i = 0; i < res.miss.Length; i++)
            {
                // 救援依頼データの問題番号にマッチする要素番号
                int qNum = dataSearch.QuestionSearch(Num, res.miss[i].mNumber);
                for (int j = 0; j < res.miss[i].mChoices.Length; j++)
                {   
                    com += "・";
                    // 救援依頼データのChoicesのcNumberにマッチする要素番号
                    int mChoice = dataSearch.ChoicesSearch(Num, qNum, res.miss[i].mChoices[j]);
                    // 解説を結合
                    com += rescue[Num].question[qNum].choices[mChoice].cAnswer + "\n" + rescue[Num].question[qNum].choices[mChoice].cExplanation + "\n";
                }
            }
            explanationStatementText.Text += com;
            if (res.workAccident.sNumber != -2)//点数が満たなかった場合はなにも行わないようにしている
            {
                explanationStatementText.Text += "作業事故\n・" + rescue[Num].question[res.workAccident.sNumber - 1].choices[res.workAccident.sChoices].cAnswer + "\n" + rescue[Num].question[res.workAccident.sNumber - 1].choices[res.workAccident.sChoices].cExplanation;//エラー個所(変数の値見れるようです)-1で配列の要素数と合わせている。これによって動くかなくなるJSONのエラーを別の方法で処理しようと思う。選択肢が三つの問題と四つの問題の違いでエラーが発生している模様(解決済み?)
            }
            resultDetailText.Content = "作業事故：あり　ミス：" + res.rMissCount;
        }   

        private void returnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            var MainpageTransition = new Mainpage();
            NavigationService.Navigate(MainpageTransition);
        }
    }
}