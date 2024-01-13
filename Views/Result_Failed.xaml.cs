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
using System.Diagnostics;



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

            //選択した選択肢を取得する
            // 表示する文字列格納用
            List<string> bad_choices = new List<string>();
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
            if (res.rScore == -1 && res.workAccident != null)
            {
                explanationStatementText.Text += "作業事故\n";
                for (int i = 0; i < res.workAccident.Length; i++)
                {
                    //Debug.WriteLine(rescue[Num].question[res.workAccident[i].sNumber - 1].choices[res.workAccident[i].sChoices].cAnswer);
                    // 作業事故の要素がnullでないことを確認
                    if (res.workAccident[i] != null)
                    {
                        int sNumberIndex = res.workAccident[i].sNumber - 1; // 配列のインデックスに合わせるために-1
                        int sChoicesIndex = res.workAccident[i].sChoices; // 既にインデックスとして適切

                        // 配列の範囲内かどうかをチェック
                        if (sNumberIndex >= 0 && sNumberIndex < rescue[Num].question.Length &&
                            sChoicesIndex >= 0 && sChoicesIndex < rescue[Num].question[sNumberIndex].choices.Length)
                        {
                            //作業事故の選択肢の文言と解説文をexplanationStatementText.Textと結合させる
                            explanationStatementText.Text += $"・{rescue[Num].question[sNumberIndex].choices[sChoicesIndex].cAnswer}\n{rescue[Num].question[sNumberIndex].choices[sChoicesIndex].cExplanation}\n";
                        }
                    }
                }
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